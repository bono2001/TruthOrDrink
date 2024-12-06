using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthOrDrink.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Difficulty { get; set; }
        public string Category { get; set; }
        public string DefaultQuestion { get; set; }
        public string PlayerQuestion { get; set; }
    }
}

