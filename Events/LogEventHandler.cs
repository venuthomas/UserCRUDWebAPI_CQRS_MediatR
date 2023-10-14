using MediatR;

namespace UserCRUDWebAPI_CQRS_MediatR.Events
{
    public class LogEventHandler : INotificationHandler<ResponseEvent>, INotificationHandler<ErrorEvent>
    {
        public async Task Handle(ResponseEvent notification, CancellationToken cancellationToken) =>
            Console.WriteLine($"User ID {notification.response.UserID}:  Message {notification.response.ActionMessage}.");

        public async Task Handle(ErrorEvent notification, CancellationToken cancellationToken) => 
            Console.WriteLine($"Error: UserID: {notification.response.UserID} Error Message: {notification.response.ActionMessage}");
    }
}
