using Azure;
using MediatR;
using UserCRUDWebAPI_CQRS_MediatR.Context;
using UserCRUDWebAPI_CQRS_MediatR.Entity;
using UserCRUDWebAPI_CQRS_MediatR.Events;
using UserCRUDWebAPI_CQRS_MediatR.Models;

namespace UserCRUDWebAPI_CQRS_MediatR.Features.Commands
{
    public record SaveUserDetailsCommand(string FirstName, string LastName, string Department, string Email, string Password) : IRequest<ResponseDto>;

    public class SaveUserDetailsCommandHandler : IRequestHandler<SaveUserDetailsCommand, ResponseDto>
    {
        private readonly demoDBContext demoDBContext;
        private readonly IMediator mediator;
        public SaveUserDetailsCommandHandler(demoDBContext _demoDBContext, IMediator _mediator) { demoDBContext = _demoDBContext; mediator = _mediator; }

        public async Task<ResponseDto> Handle(SaveUserDetailsCommand request, CancellationToken cancellationToken)
        {

            ResponseDto response;
            try
            {
                if (request is not null)
                {
                    var userID = Guid.NewGuid();
                    await demoDBContext.Users.AddAsync(new Users
                    {
                        UserID = userID,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        Department = request.Department,
                        Password = request.Password,
                    });
                    await demoDBContext.SaveChangesAsync();
                    response = new ResponseDto(userID, "Saved Successfully!");
                    mediator.Publish(new ResponseEvent(response));
                    return response;
                }

                response = new ResponseDto(default, "Request is not found!");
                mediator.Publish(new ResponseEvent(response));
                return response;
            }
            catch
            {
                response = new ResponseDto(default, "Failed!");
                mediator.Publish(new ErrorEvent(response));
                return response;
            }
        }
    }
}
