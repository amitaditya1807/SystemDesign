using CarParking.Application.Services;
using CarParking.Domain.Entities;
using CarParking.Domain.Enums;
using CarParking.Infrastructure.Repositories;
using CarParking.Infrastructure.Strategies;

namespace CarParking
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to My Car Parking Systemm !!!");

            // 1. Create spots
            var spots = new List<ParkingSpot>
            {
                new ParkingSpot(1, SpotType.Small),
                new ParkingSpot(2, SpotType.Medium),
                new ParkingSpot(3, SpotType.Large),
                new ParkingSpot(4, SpotType.Large)
            };

            // 2. Create dependencies
            var repo = new TicketRepository();
            var strategy = new FirstAvailableStrategy();

            var service = new ParkingService(spots, repo, strategy);

            // 3. Park vehicle
            var vehicle = new Vehicle("KA01AB1234", VehicleType.Car);
            var ticket = service.Park(vehicle);

            if (ticket != null)
            {
                Console.WriteLine($"Parked. TicketId: {ticket.TicketId}");
            }

            var vehicle1 = new Vehicle("KA01AB1235", VehicleType.Car);
            var ticket1 = service.Park(vehicle1);

            if (ticket1 != null)
            {
                Console.WriteLine($"Parked. TicketId: {ticket1.TicketId}");
            }

            var vehicle2 = new Vehicle("KA01AB1236", VehicleType.Truck);
            var ticket2 = service.Park(vehicle2);

            if (ticket2 != null)
            {
                Console.WriteLine($"Parked. TicketId: {ticket2.TicketId}");
            }

            var vehicle3 = new Vehicle("KA01AB1237", VehicleType.Truck);
            var ticket3 = service.Park(vehicle3);

            if (ticket3 != null)
            {
                Console.WriteLine($"Parked. TicketId: {ticket3.TicketId}");
            }

            // simulate time
            Thread.Sleep(2000);

            // 4. Unpark
            var fee = service.Unpark(ticket.TicketId);
            Console.WriteLine($"Fee: {fee}");

            /*fee = service.Unpark(ticket1.TicketId);
            Console.WriteLine($"Fee: {fee}");
            */
            fee = service.Unpark(ticket2.TicketId);
            Console.WriteLine($"Fee: {fee}");

            fee = service.Unpark(ticket3.TicketId);
            Console.WriteLine($"Fee: {fee}");
        }
    }
}