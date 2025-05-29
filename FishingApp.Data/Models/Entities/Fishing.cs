using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingApp.Data.Models.Entities
{
    public class Fishing
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<Fish> Fishes { get; set; } = new List<Fish>();
    }
}
