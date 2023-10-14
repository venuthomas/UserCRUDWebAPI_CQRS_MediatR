using Azure;
using MediatR;
using UserCRUDWebAPI_CQRS_MediatR.Context;
using UserCRUDWebAPI_CQRS_MediatR.Events;
using UserCRUDWebAPI_CQRS_MediatR.Models;

namespace UserCRUDWebAPI_CQRS_MediatR.Features.Queries
{
    public record GetUserDetailsByUserIDQuery(Guid userID) : IRequest<UserDto>;


    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsByUserIDQuery, UserDto>
    {
        private readonly demoDBContext demoDBContext;
        private readonly IMediator mediator;

        public GetUserDetailsQueryHandler(demoDBContext _demoDBContext, IMediator _mediator) { demoDBContext = _demoDBContext; mediator = _mediator; }


        public async Task<UserDto> Handle(GetUserDetailsByUserIDQuery request, CancellationToken cancellationToken)
        {

            ResponseDto response;
            var _userDetails = await demoDBContext.Users.FindAsync(request.userID);
            if (_userDetails is not null)
            {
                return new UserDto
                {
                    UserID = _userDetails.UserID,
                    FirstName = _userDetails.FirstName,
                    LastName = _userDetails.LastName,
                    Department = _userDetails.Department,
                    Email = _userDetails.Email,
                    Password = _userDetails.Password
                };
            }
            else
            {
                response = new ResponseDto(request.userID, "User details not found!");
                mediator.Publish(new ErrorEvent(response));

            }

            return null;
        }
    }
}
