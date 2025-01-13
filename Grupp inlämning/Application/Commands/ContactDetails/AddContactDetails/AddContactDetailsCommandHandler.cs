using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Commands.ContactDetails.AddContactDetails
{
    public class AddContactDetailsCommandHandler : IRequestHandler<AddContactDetailsCommand, OperationResult<ContactDetail>>
    {
        private readonly IRepository<ContactDetail> _contactDetailRepository;
        private readonly ILogger<AddContactDetailsCommandHandler> _logger;

        public AddContactDetailsCommandHandler(IRepository<ContactDetail> contactDetailRepository, ILogger<AddContactDetailsCommandHandler> logger)
        {
            _contactDetailRepository = contactDetailRepository;
            _logger = logger;
        }

        public async Task<OperationResult<ContactDetail>> Handle(AddContactDetailsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling AddContactDetailsCommand for contact with name: {ContactName}", request.ContactDetailsToAdd.Name);

                var contactDetailToAdd = new ContactDetail
                {
                    Id = Guid.NewGuid(),
                    Name = request.ContactDetailsToAdd.Name,
                    Email = request.ContactDetailsToAdd.Email,
                    Phone = request.ContactDetailsToAdd.Phone,
                    Address = request.ContactDetailsToAdd.Address,
                    ZipCode = request.ContactDetailsToAdd.ZipCode
                };

                _logger.LogInformation("Attempting to add a new contact detail with ID: {ContactId}", contactDetailToAdd.Id);

                await _contactDetailRepository.Add(contactDetailToAdd);

                _logger.LogInformation("Successfully added new contact detail with ID: {ContactId}", contactDetailToAdd.Id);

                return OperationResult<ContactDetail>.Success(contactDetailToAdd);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding contact details.");

                return OperationResult<ContactDetail>.Failure($"An error occurred while adding contact details: {ex.Message}");
            }
        }
    }
}
