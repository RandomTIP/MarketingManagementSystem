using MediatR;
using MMS.Core.Repositories;
using MMS.Core.UnitOfWork;
using MMS.Service.Bonuses.Commands;
using MMS.Service.Common.POCO;
using MMS.Service.Distributors;
using MMS.Service.Sales;

namespace MMS.Service.Bonuses;

internal class BonusesCommandHandler : IRequestHandler<CalculateBonusesCommand, int>
{
    private readonly IRepository<Bonus> _bonusRepository;
    private readonly IRepository<Sale> _saleRepository;
    private readonly IRepository<Distributor> _distributorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BonusesCommandHandler(IRepository<Bonus> bonusRepository, IRepository<Sale> saleRepository,
        IRepository<Distributor> distributorRepository, IUnitOfWork unitOfWork)
    {
        _bonusRepository = bonusRepository;
        _saleRepository = saleRepository;
        _distributorRepository = distributorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CalculateBonusesCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        request.Validate();

        int calculatedCount = 0;

        using IUnitOfWorkScope scope = await _unitOfWork.CreteScopeAsync(cancellationToken);

        List<Sale> salesInGivenPeriod = await _saleRepository.GetListForUpdateAsync(
            x => x.SaleDate > request.StartDate && x.SaleDate < request.EndDate && !x.IsCalculated,
            q => q.OrderBy(s => s.DistributorId),
            new[] { "Product" },
            cancellationToken);

        if (salesInGivenPeriod.Count > 0)
        {
            List<DistributorSaleModel>? distributorSales = salesInGivenPeriod
                .GroupBy(
                    x => x.DistributorId,
                    x => x.TotalPrice,
                    (id, totalSales) => new DistributorSaleModel
                    {
                        DistributorId = id,
                        TotalSales = totalSales.Sum()
                    })
                .ToList();

            if (distributorSales?.Count > 0)
            {
                foreach (DistributorSaleModel distributorSaleModel in distributorSales)
                {
                    List<IdModel>? firstLevelDistributors = await _distributorRepository.GetMappedListAsync<IdModel>(
                        IdModel.DistributorIdConfig,
                        x => x.RecommendationAuthorDistributorId == distributorSaleModel.DistributorId,
                        cancellationToken: cancellationToken);

                    if (firstLevelDistributors?.Count is null or 0)
                    {
                        continue;
                    }

                    IEnumerable<int> firstLevelDistributorIds = firstLevelDistributors.Select(x => x.Id);

                    distributorSaleModel.FirstLevelSales = salesInGivenPeriod
                        .Where(x => firstLevelDistributorIds.Contains(x.DistributorId)).Sum(x => x.TotalPrice);

                    foreach (int firstLevelDistributorId in firstLevelDistributorIds)
                    {
                        List<IdModel>? secondLevelDistributors =
                            await _distributorRepository.GetMappedListAsync<IdModel>(
                                IdModel.DistributorIdConfig,
                                x => x.RecommendationAuthorDistributorId == firstLevelDistributorId,
                                cancellationToken: cancellationToken);

                        if (secondLevelDistributors?.Count is null or 0)
                        {
                            continue;
                        }

                        IEnumerable<int> secondLevelDistributorIds = secondLevelDistributors.Select(x => x.Id);

                        distributorSaleModel.SecondLevelSales = salesInGivenPeriod
                            .Where(x => secondLevelDistributorIds.Contains(x.DistributorId)).Sum(x => x.TotalPrice);
                    }

                    var bonus = new Bonus();
                    bonus.Calculate(request, distributorSaleModel);
                    _ = await _bonusRepository.InsertAsync(bonus, cancellationToken);

                    foreach (Sale sale in salesInGivenPeriod.Where(x => x.DistributorId == bonus.DistributorId))
                    {
                        sale.IncludeInCalculation();
                        calculatedCount++;
                        _ = await _saleRepository.UpdateAsync(sale, cancellationToken);
                    }
                }
            }
        }

        await scope.CompleteAsync(cancellationToken);

        return calculatedCount;
    }
}