using BookstoreAppWorkerService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAppWorkerService.Data
{
    public class CommandDatabaseContext : DbContext
    {
        public CommandDatabaseContext(DbContextOptions<CommandDatabaseContext> options) : base(options)
        {

        }
        //public DbSet<Book> Books { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }

    }
}
