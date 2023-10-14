using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserCRUDWebAPI_CQRS_MediatR.Features.Commands;
using UserCRUDWebAPI_CQRS_MediatR.Features.Queries;
using UserCRUDWebAPI_CQRS_MediatR.Models;

namespace UserCRUDWebAPI_CQRS_MediatR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        public UserController( IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpGet("GetUserLists")]
        public async Task<List<UserDto>> GetUserLists() => await this.mediator.Send(new GetUserListsQuery());

        [HttpGet("GetUserDetailsByUserID")]
        public async Task<UserDto> GetUserDetailsByUserID(Guid userID) => await this.mediator.Send(new GetUserDetailsByUserIDQuery(userID));

        [HttpPost("SaveUserDetails")]
        public async Task<ResponseDto> SaveUserDetails(SaveUserDetailsCommand userDto) => await this.mediator.Send(userDto);

        [HttpPut("UpdateUserDetails")]
        public async Task<ResponseDto> UpdateUserDetails(UpdateUserDetailsCommand userDto) => await this.mediator.Send(userDto);

        [HttpGet("DeleteUserDetails")]
        public async Task<ResponseDto> DeleteUserDetails(Guid userID) => await this.mediator.Send(new DeleteUserDetailsCommand(userID));

    }
}
