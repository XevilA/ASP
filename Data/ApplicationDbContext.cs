using Microsoft.EntityFrameworkCore;
using RestaurantBooking.Models;

namespace RestaurantBooking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Reservation> Reservations => Set<Reservation>();
    }
}