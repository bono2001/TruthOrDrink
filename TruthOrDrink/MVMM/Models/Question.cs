using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruthOrDrink.MVMM.Models;

namespace TruthOrDrink.Models
{
    [Table("Questions")]
    public class Question
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }
        [Column("DefaultQuestion")]
        public string DefaultQuestion { get; set; }
        [Column("PlayerQuestion")]
        public string PlayerQuestion { get; set; }
        [Column("Difficulty")]
        public string Difficulty { get; set; }
        [Column("CategoryId")]
        public int CategoryId { get; set; } // Foreign Key naar Category

        [Ignore] // Voorkomt dat dit veld in de database wordt opgeslagen
        public Category Category { get; set; }
    }
}

