using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TripsMicroservice.Data;
using TripsMicroservice.Entities;

namespace TripsMicroservice.Tests.Fixtures
{
    public static class TestingDbContextFactory
    {
        public static TripsDbContext Create()
        {
            var options = new DbContextOptionsBuilder<TripsDbContext>()
                // Crea una BD temporal y única para esta prueba
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new TripsDbContext(options);

            // Dato esencial: Insertamos el estado "Pending" (ID 1)
            context.TripStates.Add(new TripState { IdTripStates = 1, Name = "Pending" });
            context.SaveChanges();

            return context;
        }

        public static void Destroy(TripsDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}