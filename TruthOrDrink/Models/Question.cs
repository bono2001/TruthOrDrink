using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthOrDrink.Models
{
    [Table("Questions")]
    public class Question
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Difficulty")]
        public string Difficulty { get; set; }

        [Column("Category")]
        public string Category { get; set; }

        [Column("DefaultQuestion")]
        public string DefaultQuestion { get; set; }

        [Column("PlayerQuestion")]
        public string PlayerQuestion { get; set; }
    }
}

