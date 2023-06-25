using MediatR;
using MMS.Core.FileManagers;
using MMS.Core.Repositories;
using MMS.Core.UnitOfWork;
using MMS.Service.Common.Exceptions;
using MMS.Service.Common.POCO;
using MMS.Service.Distributors.Commands;

namespace MMS.Service.Distributors
{
    internal class DistributorCommandHandler : IRequestHandler<CreateDistributorCommand, int>, IRequestHandler<UpdateDistributorCommand>, IRequestHandler<DeleteDistributorCommand>
    {
        private readonly IRepository<Distributor> _distributorRepository;
        private readonly IFileManager _fileManager;
        private readonly IUnitOfWork _unitOfWork;

        public DistributorCommandHandler(IRepository<Distributor> distributorRepository, IFileManager fileManager, IUnitOfWork unitOfWork)
        {
            _distributorRepository = distributorRepository;
            _fileManager = fileManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateDistributorCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(
                    $"{nameof(request)} must not be null! {typeof(CreateDistributorCommand)}");
            }

            var codes = await 
                _distributorRepository.GetMappedListAsync<PreviousCodeModel>(
                    PreviousCodeModel.DistributorConfiguration,
                    cancellationToken: cancellationToken);

            var distributor = new Distributor(request, codes.LastOrDefault()?.Code);

            if (request.Picture != null)
            {
                var pictureLocation = await _fileManager.SaveFileAsync(request.Picture, cancellationToken);
                distributor.AddPicture(pictureLocation);
            }

            if (request.RecommendationAuthorDistributorId.HasValue)
            {
                await SetRecommendation(distributor, request.RecommendationAuthorDistributorId.Value, cancellationToken);
            }

            // თუ SetRecommendation() მეთოდის დროს დისტრიბუტორის რეკომენდატორი
            // არ აისახა და ააფდეითდა ბაზაში ესეიგი ეს დისტრიბუტორიც არ შექმნილა.
            if (!distributor.RecommendationAuthorDistributorId.HasValue)
            {
                distributor = await _distributorRepository.InsertAsync(distributor, cancellationToken);
            }

            return distributor.Id;
        }

        public async Task<Unit> Handle(UpdateDistributorCommand request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request), $"{ nameof(request) } must not be null! { typeof(UpdateDistributorCommand)}");
            }

            request.Validate();

            var distributor = await _distributorRepository.GetForUpdateAsync(request.Id,
                new[] { "AddressInformation", "ContactInformation", "IdentityDocument" },
                cancellationToken);

            if (distributor == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Distributor));
            }

            distributor.Update(request);

            if (request.Picture != null)
            {
                var pictureLocation = await _fileManager.SaveFileAsync(request.Picture, cancellationToken);
                distributor.AddPicture(pictureLocation);
            }

            if (request.RecommendationAuthorDistributorId.HasValue &&
                request.RecommendationAuthorDistributorId != distributor.RecommendationAuthorDistributorId)
            {
                await SetRecommendation(distributor, request.RecommendationAuthorDistributorId.Value,
                    cancellationToken);
            }

            _ = await _distributorRepository.UpdateAsync(distributor, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteDistributorCommand request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                throw new ArgumentNullException(
                $"{nameof(request)} must not be null! {typeof(DeleteDistributorCommand)}");
            }

            request.Validate();

            using var scope = await _unitOfWork.CreteScopeAsync(cancellationToken);

            await RemoveRecommendation(request.Id, cancellationToken);

            await _distributorRepository.DeleteAsync(request.Id, cancellationToken: cancellationToken);

            await scope.CompleteAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task SetRecommendation(Distributor distributor, int recommendationAuthorId, CancellationToken cancellationToken = default)
        {
            var recommendationAuthor =
                await _distributorRepository.GetForUpdateAsync(recommendationAuthorId,
                    cancellationToken: cancellationToken);

            if (recommendationAuthor == null)
            {
                throw new RecommendationException(
                    $"Recommendation author distributor with provided id ({recommendationAuthorId}) was not fount!");
            }

            recommendationAuthor.AddRecommendedDistributor(distributor);

            await _distributorRepository.UpdateAsync(recommendationAuthor, cancellationToken);
        }

        private async Task RemoveRecommendation(int distributorId, CancellationToken cancellationToken = default)
        {
            var authorId =
                await _distributorRepository.GetMappedAsync<IdModel>(distributorId,
                    IdModel.RecommendationAuthorConfig, cancellationToken: cancellationToken);

            if (authorId == null)
            {
                return;
            }

            var recommendationAuthor =
                await _distributorRepository.GetForUpdateAsync(authorId.Id,
                    cancellationToken: cancellationToken);

            if (recommendationAuthor == null)
            {
                return;
            }

            recommendationAuthor.RemoveRecommendedDistributor();

            await _distributorRepository.UpdateAsync(recommendationAuthor, cancellationToken);
        }
    }
}
