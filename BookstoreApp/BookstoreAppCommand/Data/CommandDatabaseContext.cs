using BookstoreAppCommand.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAppCommand.Data
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
