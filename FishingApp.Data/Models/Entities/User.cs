using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingApp.Data.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Score { get; set; }

        public ICollection<Fishing> Fishings { get; set; } = new List<Fishing>();
    }
}
