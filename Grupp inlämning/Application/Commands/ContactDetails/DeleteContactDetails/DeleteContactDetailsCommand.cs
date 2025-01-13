using Domain.Models;
using MediatR;

namespace Application.Commands.ContactDetails.DeleteContactDetails
{
    public class DeleteContactDetailsCommand : IRequest<OperationResult<ContactDetail>>
    {
        public DeleteContactDetailsCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
