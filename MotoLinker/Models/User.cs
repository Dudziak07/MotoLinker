using System;

namespace MotoLinker.Models
{
    public class User
    {
        private static int NextUserId = 1; // Zmienna statyczna do generowania kolejnych ID

        public int Id { get; set; } // ID jako liczba całkowita
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;

        // Konstruktor automatycznie przypisujący nowe ID
        public User()
        {
            Id = NextUserId++;
        }
    }
}