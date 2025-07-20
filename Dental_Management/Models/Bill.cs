using Kimtoo.DbManager;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dental_Management.Models
{
    [AutoGenerateTable(3)]
    [Alias("billing")]
    public class Bill
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        [ForeignKey(typeof(Treatment), OnDelete = "CASCADE")]
        public int TreatmentID { get; set; }

        public string Description { get; set; } = "";
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}