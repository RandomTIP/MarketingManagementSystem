using MMS.Core.Domain;
using MMS.Service.Bonuses.Commands;
using MMS.Service.Common.POCO;

namespace MMS.Service.Bonuses
{
    public partial class Bonus : AggregateRoot
    {
        public Bonus() { }

        public void Calculate(CalculateBonusesCommand command, DistributorSaleModel distributorSaleModel)
        {
            DistributorId = distributorSaleModel.DistributorId;
            StartDate = command.StartDate;
            EndDate = command.EndDate;
            CalculationDate = DateTime.Now;

            DistributorTotalSales = distributorSaleModel.TotalSales;
            FirstLevelTotalSales = distributorSaleModel.FirstLevelSales;
            SecondLevelTotalSales = distributorSaleModel.SecondLevelSales;

            DistributorBonusShare = DistributorTotalSales * new decimal(0.1);
            FirstLevelBonusShare = FirstLevelTotalSales * new decimal(0.05);
            SecondLevelBonusShare = SecondLevelTotalSales * new decimal(0.01);
            TotalBonus = DistributorBonusShare + FirstLevelBonusShare + SecondLevelBonusShare;
        }
    }
}
