using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingApp.Data.Models.Entities
{
    public class Fish
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Discription Discription { get; set; } = null!;
    }
}
