using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarParking.Domain.Enums;

namespace CarParking.Domain.Entities
{
    public class Vehicle
    {
        public String Number { get; set; }
        public VehicleType Type { get; set; }

        public Vehicle(string number, VehicleType type) 
        {
            this.Number = number;
            this.Type = type;
        }
    }
}
