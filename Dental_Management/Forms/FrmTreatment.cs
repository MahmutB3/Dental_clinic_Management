using Dental_Management.Models;
using Kimtoo.BindingProvider;
using Kimtoo.DbManager;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dental_Management.Forms
{
    public partial class FrmTreatment : Form
    {
        private Treatment _treatment = null;

        public FrmTreatment(Appointment appointment)
        {
            InitializeComponent();
            this._treatment = appointment.GetTreatment();
            if (_treatment == null)
            {
                // create appointment
                _treatment = new Treatment
                {
                    AppointmentID = appointment.Id,
                };
                Connections.GetConnection().Save(_treatment);
                //Add default Billing
                Connections.GetConnection().Save(
                    new Bill
                    {
                        TreatmentID = _treatment.Id,
                        Description = "Appointment Fee"
                    });
                Connections.GetConnection().Save(
                new Bill
                {
                    TreatmentID = _treatment.Id,
                    Description = "Consultation Fee"
                });
                Connections.GetConnection().Save(
                new Bill
                {
                    TreatmentID = _treatment.Id,
                    Description = "Prescriptions"
                });
                Connections.GetConnection().Save(
                new Bill
                {
                    TreatmentID = _treatment.Id,
                    Description = "Treatment Fee"
                });
            }

            bindingProvider1.Bind(_treatment);
            grid.Bind(_treatment.GetBilling());
            lblTot.Text = $"Total: {_treatment.GetBilling().Sum(r => r.Amount).ToString("C1")}";
            //kimtoo toolkit crud operations
            grid.OnDelete<Bill>((a, b) => Connections.GetConnection().Delete(a) >= 0);
            grid.OnEdit<Bill>((a, b) =>
            {
                Connections.GetConnection().Save(a);
                lblTot.Text = $"Total: {_treatment.GetBilling().Sum(r => r.Amount).ToString("C1")}";
                return true;
            });
            grid.OnError<Bill>((a, b) => { /**do nothing**/});

            Cursor.Current = Cursors.Default;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Connections.GetConnection().Save(_treatment);
            bunifuSnackbar1.Show(this.FindForm(), "Saved", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            grid.Bind(_treatment.GetBilling());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            grid.Bind(new Bill() { TreatmentID = _treatment.Id }, 1);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            grid.DeleteRow<Bill>();
            lblTot.Text = $"Total: {_treatment.GetBilling().Sum(r => r.Amount).ToString("C1")}";
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}