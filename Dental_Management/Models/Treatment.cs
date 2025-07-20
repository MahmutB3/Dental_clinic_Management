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
    [Alias("treatments")]
    public class Treatment
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        [ForeignKey(typeof(Appointment), OnDelete = "CASCADE")]
        public int AppointmentID { get; set; }

        public string CosultationNote { get; set; } = "";
        public string Prescriptions { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Appointment GetAppointment()
            => Connections.GetConnection().SingleById<Appointment>(this.AppointmentID);

        public Dentist GetDentist()
            => GetAppointment().GetDentist();

        public Patient GetPatient()
        => GetAppointment().GetPatient();

        public List<Bill> GetBilling()
            => Connections.GetConnection().Select<Bill>(r => r.TreatmentID == this.Id);
    }
}