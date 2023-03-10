using BookstoreAppWorkerService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAppWorkerService.Data
{
    public class QueryDatabaseContext : DbContext
    {
        public QueryDatabaseContext(DbContextOptions<QueryDatabaseContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set;}
        public DbSet<User> Users { get; set;}
        public DbSet<Booking> Bookings { get; set;} 
        
    }
}
