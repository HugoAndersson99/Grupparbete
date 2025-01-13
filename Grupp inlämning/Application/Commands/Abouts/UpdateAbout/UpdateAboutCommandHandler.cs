using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Abouts.UpdateAbout
{
    public class UpdateAboutCommandHandler : IRequestHandler<UpdateAboutCommand, OperationResult<About>>
    {
        private readonly IRepository<About> _aboutRepository;
        private readonly ILogger<UpdateAboutCommandHandler> _logger;

        public UpdateAboutCommandHandler(IRepository<About> aboutRepository, ILogger<UpdateAboutCommandHandler> logger)
        {
            _aboutRepository = aboutRepository;
            _logger = logger;
        }

        public async Task<OperationResult<About>> Handle(UpdateAboutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var aboutToUpdate = _aboutRepository.GetById(request.Id);
                if (aboutToUpdate == null)
                {
                    _logger.LogWarning("Attempted to update about with ID: {AboutId}, but no about was found.", request.Id);
                    return OperationResult<About>.Failure($"About with ID {request.Id} not found.");
                }

                UpdateAboutDetails(aboutToUpdate, request.UpdatedAbout);
                _aboutRepository.Update(aboutToUpdate);

                _logger.LogInformation("About with ID: {AboutId} was successfully updated.", request.Id);

                return OperationResult<About>.Success(aboutToUpdate, "About updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating about with ID: {AboutId}", request.Id);
                return OperationResult<About>.Failure("An unexpected error occurred while updating the about.");
            }
        }

        private void UpdateAboutDetails(About aboutToUpdate, AboutDto updatedAbout)
        {
            aboutToUpdate.Description = updatedAbout.Description;
        }

    }
}
