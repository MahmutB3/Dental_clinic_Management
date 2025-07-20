using Kimtoo.DbManager;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dental_Management.Models
{
    [AutoGenerateTable(2)]
    [Alias("appointments")]
    public class Appointment
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        [ForeignKey(typeof(Patient), OnDelete = "CASCADE")]
        public int PatientId { get; set; }

        [ForeignKey(typeof(Dentist), OnDelete = "CASCADE")]
        public int DentistId { get; set; }

        public string Notes { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
        public string Time { get; set; } = "";
        public bool Cancelled { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Patient GetPatient()
          => Connections.GetConnection().SingleById<Patient>(this.PatientId);

        public Dentist GetDentist()
          => Connections.GetConnection().SingleById<Dentist>(this.DentistId);

        public Treatment GetTreatment()
            => Connections.GetConnection().Single<Treatment>(r => r.AppointmentID == this.Id);

        public bool HasSessions()
         => GetTreatment() != null;
    }
}