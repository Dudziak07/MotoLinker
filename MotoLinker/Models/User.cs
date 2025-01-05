using System;
using System.Collections.Generic;

namespace MotoLinker.Models
{
    public class User
    {
        // Unikalny identyfikator użytkownika
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; } // Hasło w wersji podstawowej

        public bool IsAdmin { get; set; } = false; // Flaga określająca administratora

        // Powiązane transakcje i oferty (jeśli używane)
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
