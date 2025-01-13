using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Abouts.AddAbout
{
    public class AddAboutCommandHandler : IRequestHandler<AddAboutCommand, OperationResult<About>>
    {
        private readonly IRepository<About> _aboutRepository;
        private readonly ILogger<AddAboutCommandHandler> _logger;

        public AddAboutCommandHandler(IRepository<About> aboutRepository, ILogger<AddAboutCommandHandler> logger)
        {
            _aboutRepository = aboutRepository;
            _logger = logger;
        }

        public async Task<OperationResult<About>> Handle(AddAboutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var aboutToAdd = new About
                {
                    Id = Guid.NewGuid(), 
                    Description = request.AboutToAdd.Description
                };

                _logger.LogInformation("Attempting to add a new About entity with ID: {Id}.", aboutToAdd.Id);

                await _aboutRepository.Add(aboutToAdd);

                _logger.LogInformation("Successfully added About entity with ID: {Id}.", aboutToAdd.Id);

                return OperationResult<About>.Success(aboutToAdd);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding About entity.");

                return OperationResult<About>.Failure($"An error occurred while adding About: {ex.Message}");
            }
        }
    }
}
