using Microsoft.EntityFrameworkCore;
using OefenenMVC.Models;

namespace OefenenMVC.Db
{
    public class VoorbeeldDb : DbContext
    {
        public VoorbeeldDb(DbContextOptions<VoorbeeldDb> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.EventID);

                entity.Property(e => e.Name)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.Description)
                      .HasMaxLength(500);

                entity.Property(e => e.Location)
                      .HasMaxLength(200);

                entity.Property(e => e.Cost)
                      .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Date)
                      .IsRequired();

                entity.Property(e => e.MaxParticipants)
                      .IsRequired();

                entity.Property(e => e.SoldTickets)
                      .IsRequired();

                entity.Property(e => e.ImageData)
                      .HasColumnType("varbinary(max)"); 

                entity.Property(e => e.ImageMimeType)
                      .HasMaxLength(50);

                entity.Property(e => e.Latitude)
                      .HasMaxLength(50);

                entity.Property(e => e.Longitude)
                      .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).HasMaxLength(100);
                entity.Property(u => u.Email).HasMaxLength(100).IsRequired();
                entity.Property(u => u.PhoneNumber).HasMaxLength(15);
                entity.Property(u => u.Password).IsRequired();
            });


            // Voeg voorbeeld data toe voor evenementen
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    EventID = 1,
                    Name = "Test Event",
                    Description = "Dit is een test evenement",
                    Date = DateTime.Now,
                    Location = "Test Locatie",
                    Cost = 10.00m,
                    MaxParticipants = 100,
                    SoldTickets = 50,
                    ImageData = null,
                    ImageMimeType = null,
                    Latitude = "50.8792533",
                    Longitude = "5.9836698", 
                    EventType = "Overig" 
                }
            );
        }
    }
}
