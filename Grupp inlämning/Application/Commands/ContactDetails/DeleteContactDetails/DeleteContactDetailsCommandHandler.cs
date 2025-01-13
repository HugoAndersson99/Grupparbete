using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.ContactDetails.DeleteContactDetails
{
    public class DeleteContactDetailsCommandHandler : IRequestHandler<DeleteContactDetailsCommand, OperationResult<ContactDetail>>
    {
        private readonly IRepository<ContactDetail> _contactDetailsRepository;
        private readonly ILogger<DeleteContactDetailsCommandHandler> _logger;

        public DeleteContactDetailsCommandHandler(IRepository<ContactDetail> contactDetailsRepository, ILogger<DeleteContactDetailsCommandHandler> logger)
        {
            _contactDetailsRepository = contactDetailsRepository;
            _logger = logger;
        }
        public async Task<OperationResult<ContactDetail>> Handle(DeleteContactDetailsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var contactDetailToDelete = _contactDetailsRepository.GetById(request.Id);

                if (contactDetailToDelete != null)
                {
                    _contactDetailsRepository.Delete(contactDetailToDelete);

                    _logger.LogInformation("Contact detail with ID: {ContactDetailId} was successfully deleted.", request.Id);

                    return OperationResult<ContactDetail>.Success(contactDetailToDelete, "Contact detail deleted successfully.");
                }

                _logger.LogWarning("Attempted to delete contact detail with ID: {ContactDetailId}, but no about was found.", request.Id);

                return OperationResult<ContactDetail>.Failure($"Contact detail with ID: {request.Id} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while attempting to delete contact detail with ID: {ContactDetailId}", request.Id);
                return OperationResult<ContactDetail>.Failure("An unexpected error occurred while deleting the contact detail.");
            }
        }
    }
}
