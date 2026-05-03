using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarParking.Domain.Entities;
using CarParking.Domain.Interfaces;
using CarParking.Infrastructure.Repositories;

namespace CarParking.Application.Services
{
    public class ParkingService
    {
        private List<ParkingSpot> _spots;
        private TicketRepository _repo;
        private IParkingStrategy _strategy;

        public ParkingService(List<ParkingSpot> spots, TicketRepository repo, IParkingStrategy strategy)
        {
            _spots = spots;
            _repo = repo;
            _strategy = strategy;
        }

        public Ticket Park(Vehicle vehicle)
        {
            var spot = _strategy.FindSpot(_spots, vehicle);
            
            if (spot == null){
                Console.WriteLine("No spot available");
                return null;
            }

            spot.isFree = false;
            var ticket = new Ticket
            {
                TicketId = Guid.NewGuid().ToString(),
                EntryTime = DateTime.Now,
                Vehicle = vehicle,
                Spot = spot
            };

            _repo.Add(ticket);
            return ticket;
        }

        public double Unpark(string ticketId)
        {
            var ticket = _repo.Get(ticketId);
            if (ticket == null){
                Console.WriteLine("Invalid ticket");
                return 0;
            }
            
            ticket.ExitTime = DateTime.Now;

            var hours = (ticket.ExitTime.Value - ticket.EntryTime).TotalHours;
            var fee = Math.Ceiling(hours) * 20;

            ticket.Spot.isFree = true;

            return fee;
        }
    }
}
