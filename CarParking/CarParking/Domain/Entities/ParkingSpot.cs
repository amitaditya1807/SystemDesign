using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarParking.Domain.Enums;

namespace CarParking.Domain.Entities
{
    public class ParkingSpot
    {
        public int Id { get; set; }
        public SpotType Type { get; set; }
        public bool isFree { get; set; }

        public ParkingSpot(int id, SpotType type) 
        {
            this.Id = id;
            this.Type = type;
            this.isFree = true;
        }
    }
}
