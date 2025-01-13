using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.ContactDetails.UpdateContactDetails
{
    public class UpdateContactDetailsCommand : IRequest<OperationResult<ContactDetail>>
    {
        public UpdateContactDetailsCommand(Guid id, ContactDetailsDto updatedContactDetail)
        {
            Id = id;
            UpdatedContactDetail = updatedContactDetail;
        }
        public Guid Id { get; set; }
        public ContactDetailsDto UpdatedContactDetail { get; set; }
    }
}
