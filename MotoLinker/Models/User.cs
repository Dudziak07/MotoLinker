using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotoLinker.Models
{
    public class User
    {
        private static int NextUserId = 1; // Zmienna statyczna do generowania kolejnych ID

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; } // ID jako liczba całkowita
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;

        // Konstruktor automatycznie przypisujący nowe ID
        public User()
        {
           // UserId = NextUserId++;
        }
    }
}