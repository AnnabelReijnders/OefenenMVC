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
                      .IsRequired(); // Naam is verplicht

                entity.Property(e => e.Description)
                      .HasMaxLength(500);

                entity.Property(e => e.Location)
                      .HasMaxLength(200);

                entity.Property(e => e.Cost)
                      .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Date)
                      .IsRequired(); // Datum is verplicht

                entity.Property(e => e.MaxParticipants)
                      .IsRequired(); // Max deelnemers is verplicht

                entity.Property(e => e.SoldTickets)
                      .IsRequired(); // Verkochte tickets zijn verplicht

                entity.Property(e => e.ImageData)
                      .HasColumnType("varbinary(max)"); // Voor de afbeelding

                entity.Property(e => e.ImageMimeType)
                      .HasMaxLength(50); // MimeType van de afbeelding

                // Definieer latitude en longitude als string
                entity.Property(e => e.Latitude)
                      .HasMaxLength(50); // Maximale lengte voor latitude string

                entity.Property(e => e.Longitude)
                      .HasMaxLength(50); // Maximale lengte voor longitude string
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id); // Zorg ervoor dat hier Id wordt gebruikt
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
                    ImageData = null, // Voeg hier een afbeelding toe als dat nodig is
                    ImageMimeType = null,
                    Latitude = "50.8792533", // Voorbeeld latitude als string
                    Longitude = "5.9836698",  // Voorbeeld longitude als string
                }
            );
        }
    }
}
