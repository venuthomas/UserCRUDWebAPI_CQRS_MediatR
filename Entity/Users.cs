using System.ComponentModel.DataAnnotations;

namespace UserCRUDWebAPI_CQRS_MediatR.Entity
{
    public class Users
    {
        [Key]
        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
