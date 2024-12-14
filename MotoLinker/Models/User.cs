using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotoLinker.Models
{
    public class User
    {
        public int UserID { get; set; }
      
        public string Username { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }

    }
}