using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotoLinker.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int VehicleID { get; set; }
        public virtual required Vehicle Vehicle { get; set; }
        public int SellerID { get; set; }
        public virtual required User Seller { get; set; }
        public int BuyerID { get; set; }
        public virtual required User Buyer { get; set; }
        public float FinalPrice { get; set; }
        public DateTime Date { get; set; }

    }
}