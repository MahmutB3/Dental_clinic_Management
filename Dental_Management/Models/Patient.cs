using Kimtoo.DbManager;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dental_Management.Models
{
    [AutoGenerateTable(1)]
    [Alias("patients")]
    public class Patient
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public string Email { get; set; } = "";
        public DateTime DOB { get; set; } = DateTime.Now;
        public string Gender { get; set; } = "";
        public string Allergies { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}