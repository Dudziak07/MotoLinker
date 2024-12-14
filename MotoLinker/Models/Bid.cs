using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotoLinker.Models
{
    public class Bid
    {
        public int BidID { get; set; }
        public int VehicleID { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public int SellerID { get; set; }
        public virtual User Seller { get; set; }
        public float OfferPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Validity { get; set; }
    }
}