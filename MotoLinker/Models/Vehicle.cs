using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotoLinker.Models
{
    public class Vehicle
    {
        public int VehicleID { get; set; }
        
        public required string VehicleMake { get; set; }
        public required string VehicleModel { get; set; }
        public int VehicleYear { get; set; }
        public int VehicleMileage { get; set; }
        public required string VehicleDescription { get; set; }
        public bool VehicleStatus { get; set; } //available/sold

        public int SellerID { get; set; }
        public virtual required User Seller { get; set; }


    }
}