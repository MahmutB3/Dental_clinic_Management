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

namespace Dental_Management.Pages
{
    public partial class PageDashboard : UserControl
    {
        public PageDashboard()
        {
            if (this.IsInDesignMode()) return;
            InitializeComponent();
        }

        private void LoadData()
        {
            var db = Connections.GetConnection();

            var appointments = db.Select<Appointment>();
            var patients = db.Select<Patient>();
            var dentists = db.Select<Dentist>();
            var billing = db.Select<Bill>();

            //update lables
            lblAppointments.Text = appointments.Count.ToString("N0");
            lblPatients.Text = patients.Count.ToString("N0");
            lblDentists.Text = dentists.Count.ToString("N0");
            lblBillling.Text = billing.Sum(r => r.Amount).ToString("C1");

            lblActive.Text = appointments.Where(r => !r.HasSessions() && !r.Cancelled).Count().ToString("N0");
            lblComplete.Text = appointments.Where(r => r.HasSessions()).Count().ToString("N0");
            lblCancelled.Text = appointments.Where(r => r.Cancelled).Count().ToString("N0");

            grid1.Bind(patients.OrderByDescending(r => r.CreatedAt).Take(20));
            grid2.Bind(billing.OrderByDescending(r => r.CreatedAt).Take(20));
        }

        private void PageDashboard_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}