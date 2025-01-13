using Application.Dtos;
using MediatR;
using Domain.Models;

namespace Application.Commands.ContactDetails.AddContactDetails
{
    public class AddContactDetailsCommand : IRequest<OperationResult<ContactDetail>>
    {
        public AddContactDetailsCommand(ContactDetailsDto contactDetailsToAdd)
        {
            ContactDetailsToAdd = contactDetailsToAdd;
        }

        public ContactDetailsDto ContactDetailsToAdd { get; }
    }
}
