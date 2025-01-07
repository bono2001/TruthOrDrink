using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthOrDrink.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public  int Score { get; set; }
        public List<string> Friends { get; set; }
    }
}
