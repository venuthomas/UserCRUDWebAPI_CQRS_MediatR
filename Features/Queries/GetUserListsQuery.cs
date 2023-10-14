using MediatR;
using Microsoft.EntityFrameworkCore;
using UserCRUDWebAPI_CQRS_MediatR.Context;
using UserCRUDWebAPI_CQRS_MediatR.Models;

namespace UserCRUDWebAPI_CQRS_MediatR.Features.Queries
{
    public record GetUserListsQuery() : IRequest<List<UserDto>>;
 
    public class GetUserListsQueryHandler : IRequestHandler<GetUserListsQuery, List<UserDto>>
    {
        private readonly demoDBContext demoDBContext;

        public GetUserListsQueryHandler(demoDBContext _demoDBContext) => demoDBContext = _demoDBContext;
        

        public async Task<List<UserDto>> Handle(GetUserListsQuery request, CancellationToken cancellationToken) =>
            await demoDBContext.Users
                .AsNoTracking()
                .Select(u => new UserDto
                {
                    UserID = u.UserID,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Department = u.Department,
                    Email = u.Email,
                    Password = u.Password
                })
                .ToListAsync();
    }
}
