using System.Text.Json.Serialization;

namespace UserCRUDWebAPI_CQRS_MediatR.Models
{
    public class UserDto
    {
        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
