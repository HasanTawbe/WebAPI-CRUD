using Microsoft.EntityFrameworkCore;
using WebAPI_CRUD_.Model;

namespace WebAPI_CRUD_.DB
{
    public class ContactsAPIDbContext : DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options) : base(options) { }

        public DbSet< Contact > Contacts
        {
            get;
            set;
        }
    }
}
