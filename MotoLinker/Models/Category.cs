using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MotoLinker.Models;

namespace MotoLinker.Models
{


    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<Announcement> Announcements { get; set; }

        // Relacja wiele-do-wielu
        //  public List<Announcement> Announcements { get; set; } = new List<Announcement>();
    }
}