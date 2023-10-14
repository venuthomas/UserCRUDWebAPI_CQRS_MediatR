using Azure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserCRUDWebAPI_CQRS_MediatR.Context;
using UserCRUDWebAPI_CQRS_MediatR.Entity;
using UserCRUDWebAPI_CQRS_MediatR.Events;
using UserCRUDWebAPI_CQRS_MediatR.Models;

namespace UserCRUDWebAPI_CQRS_MediatR.Features.Commands
{
    public record DeleteUserDetailsCommand(Guid userID) : IRequest<ResponseDto>;

    public class DeleteUserDetailsCommandHandler : IRequestHandler<DeleteUserDetailsCommand, ResponseDto>
    {
        private readonly demoDBContext demoDBContext;
        private readonly IMediator mediator;

        public DeleteUserDetailsCommandHandler(demoDBContext _demoDBContext, IMediator _mediator) { demoDBContext = _demoDBContext; mediator = _mediator; }

        public async Task<ResponseDto> Handle(DeleteUserDetailsCommand request, CancellationToken cancellationToken)
        {
            ResponseDto response;
            try
            {
                if (request is not null)
                {
                    var userDetails = await demoDBContext.Users.FirstOrDefaultAsync(x => x.UserID == request.userID);
                    if (userDetails != null)
                    {
                        demoDBContext.Users.Remove(userDetails);
                        await demoDBContext.SaveChangesAsync();
                        response = new ResponseDto(request.userID, "Deleted Successfully!");
                        mediator.Publish(new ResponseEvent(response));
                        return response;
                    }
                    else
                    {
                        response = new ResponseDto(request.userID, "User ID not found in the Database!");
                        mediator.Publish(new ErrorEvent(response));
                        return response;
                    }
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
