using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace MotoLinker.Models
{
    public class Vehicle
    {
        public int VehicleID { get; set; }
        
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public int VehicleYear { get; set; }
        public int VehicleMileage { get; set; }
        public string VehicleDescription { get; set; }
        public bool VehicleStatus { get; set; } //available/sold

        public int SellerID { get; set; }
        public virtual User Seller { get; set; }


    }
}