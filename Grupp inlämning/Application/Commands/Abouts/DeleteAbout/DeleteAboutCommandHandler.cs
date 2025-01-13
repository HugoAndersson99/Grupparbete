using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Abouts.DeleteAbout
{
    public class DeleteAboutCommandHandler : IRequestHandler<DeleteAboutCommand, OperationResult<About>>
    {
        private readonly IRepository<About> _aboutRepository;
        private readonly ILogger<DeleteAboutCommandHandler> _logger;

        public DeleteAboutCommandHandler(IRepository<About> authorRepository, ILogger<DeleteAboutCommandHandler> logger)
        {
            _aboutRepository = authorRepository;
            _logger = logger;
        }
        public async Task<OperationResult<About>> Handle(DeleteAboutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var aboutToDelete = _aboutRepository.GetById(request.Id);

                if (aboutToDelete != null)
                {
                    _aboutRepository.Delete(aboutToDelete);

                    _logger.LogInformation("About with ID: {AboutId} was successfully deleted.", request.Id);

                    return OperationResult<About>.Success(aboutToDelete, "About deleted successfully.");
                }

                _logger.LogWarning("Attempted to delete about with ID: {AboutId}, but no about was found.", request.Id);

                return OperationResult<About>.Failure($"About with ID: {request.Id} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while attempting to delete about with ID: {AboutId}", request.Id);
                return OperationResult<About>.Failure("An unexpected error occurred while deleting the about.");
            }
        }
    }
}
