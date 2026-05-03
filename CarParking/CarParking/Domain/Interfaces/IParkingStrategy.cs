using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarParking.Domain.Entities;

namespace CarParking.Domain.Interfaces
{
    public interface IParkingStrategy
    {
        ParkingSpot FindSpot(List<ParkingSpot> spots, Vehicle vehicle);
    }
}
