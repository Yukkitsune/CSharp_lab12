using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TourClass;
public class TourContext:DbContext
{
    public TourContext(DbContextOptions<TourContext> options) : base(options) { }
    public TourContext() { }
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Tour> Tours => Set<Tour>();
    public DbSet<Booking> Bookings => Set<Booking>();
}
namespace TourClass
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Description {  get; set; } = string.Empty;
    }
    public class Tour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TourId { get; set; }
        public string Destination { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;

    }
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int TourId { get; set; }
        [Required]
        [Range(1, 100)]
        public int NumberOfPeople { get; set; }
        public string Description { get; set; } = string.Empty;

        public Client? Client { get; set; }
        public Tour? Tour { get; set; }
    }
}