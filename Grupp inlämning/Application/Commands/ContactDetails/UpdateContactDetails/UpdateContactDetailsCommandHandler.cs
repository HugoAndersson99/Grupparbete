using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.ContactDetails.UpdateContactDetails
{
    public class UpdateContactDetailsCommandHandler : IRequestHandler<UpdateContactDetailsCommand, OperationResult<ContactDetail>>
    {
        private readonly IRepository<ContactDetail> _contactDetailsRepository;
        private readonly ILogger<UpdateContactDetailsCommandHandler> _logger;

        public UpdateContactDetailsCommandHandler(IRepository<ContactDetail> contactDetailsRepository, ILogger<UpdateContactDetailsCommandHandler> logger)
        {
            _contactDetailsRepository = contactDetailsRepository;
            _logger = logger;
        }

        public async Task<OperationResult<ContactDetail>> Handle(UpdateContactDetailsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var contactDetailToUpdate = _contactDetailsRepository.GetById(request.Id);
                if (contactDetailToUpdate == null)
                {
                    _logger.LogWarning("Attempted to update contact detail with ID: {ContactDetailId}, but no contact detail was found.", request.Id);
                    return OperationResult<ContactDetail>.Failure($"Contact detail with ID {request.Id} not found.");
                }

                UpdateContactDetails(contactDetailToUpdate, request.UpdatedContactDetail);
                _contactDetailsRepository.Update(contactDetailToUpdate);

                _logger.LogInformation("Contact detail with ID: {ContactDetailId} was successfully updated.", request.Id);

                return OperationResult<ContactDetail>.Success(contactDetailToUpdate, "Contact details updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating contact detail with ID: {ContactDetailId}", request.Id);
                return OperationResult<ContactDetail>.Failure("An unexpected error occurred while updating the contact detail.");
            }
        }

        private void UpdateContactDetails(ContactDetail contactDetailToUpdate, ContactDetailsDto updatedContactDetail)
        {
            contactDetailToUpdate.Name = updatedContactDetail.Name;
            contactDetailToUpdate.Email = updatedContactDetail.Email;
            contactDetailToUpdate.Phone = updatedContactDetail.Phone;
            contactDetailToUpdate.Address = updatedContactDetail.Address;
            contactDetailToUpdate.ZipCode = updatedContactDetail.ZipCode;
        }

    }
}
