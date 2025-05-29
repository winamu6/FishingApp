using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingApp.Data.Models.Entities
{
    public class Discription
    {
        public int Id { get; set; }

        public int FishId { get; set; }
        public Fish Fish { get; set; } = null!;

        public string Text { get; set; } = null!;
        public string Resirvoir { get; set; } = null!;
        public double Weight { get; set; }
    }
}
