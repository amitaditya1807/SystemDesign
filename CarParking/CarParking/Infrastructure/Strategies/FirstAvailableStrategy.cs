using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarParking.Domain.Entities;
using CarParking.Domain.Enums;
using CarParking.Domain.Interfaces;

namespace CarParking.Infrastructure.Strategies
{
    public class FirstAvailableStrategy : IParkingStrategy
    {
        public ParkingSpot FindSpot(List<ParkingSpot> spots, Vehicle vehicle)
        {
            foreach (var spot in spots)
            {
                if(spot.isFree && IsCompatible(spot, vehicle))
                {
                    return spot;
                }
            }

            return null;
        }

        private bool IsCompatible(ParkingSpot spot, Vehicle vehicle)
        {
            if (vehicle.Type == VehicleType.Bike && spot.Type == SpotType.Small) return true;
            if (vehicle.Type == VehicleType.Car && spot.Type == SpotType.Medium) return true;
            if (vehicle.Type == VehicleType.Truck && spot.Type == SpotType.Large) return true;
            
            return false;
        }
    }
}
