using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarParking.Domain.Entities;

namespace CarParking.Infrastructure.Repositories
{
    public class TicketRepository
    {
        private Dictionary<string, Ticket> _tickets = new();

        public void Add(Ticket ticket)
        {
            _tickets[ticket.TicketId] = ticket;
        }

        public Ticket Get(string ticketId)
        {
            return _tickets.ContainsKey(ticketId) ? _tickets[ticketId] : null;
        }
    }
}
