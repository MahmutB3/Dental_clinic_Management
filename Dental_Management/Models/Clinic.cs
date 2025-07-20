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
    [Alias("clinic")]
    public class Clinic
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";

        public byte[] Logo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}