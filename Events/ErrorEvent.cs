using MediatR;
using UserCRUDWebAPI_CQRS_MediatR.Models;

namespace UserCRUDWebAPI_CQRS_MediatR.Events
{
    public record ErrorEvent(ResponseDto response) : INotification;
}
