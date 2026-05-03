using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParking.Domain.Entities
{
    public class Ticket
    {
        public string TicketId { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }

        public ParkingSpot Spot { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
