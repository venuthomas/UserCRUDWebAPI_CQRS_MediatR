using Microsoft.EntityFrameworkCore;
using UserCRUDWebAPI_CQRS_MediatR.Entity;

namespace UserCRUDWebAPI_CQRS_MediatR.Context
{
    public class demoDBContext : DbContext
    {
        public demoDBContext(DbContextOptions<demoDBContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
    }
}
