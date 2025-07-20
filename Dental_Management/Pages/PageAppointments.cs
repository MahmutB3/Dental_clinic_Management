using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dental_Management.Models;
using Kimtoo.BindingProvider;
using ServiceStack.OrmLite;
using Kimtoo.Preloader;

namespace Dental_Management.Pages
{
    public partial class PageAppointments : UserControl
    {
        public bool IsAppointments { get; set; }

        public PageAppointments()
        {
            if (this.IsInDesignMode()) return;
            InitializeComponent();

            grid.OnDelete<Appointment>((a, b) => Kimtoo.DbManager.Connections.GetConnection().Delete(a) >= 0);
        }

        private void LoadData()
        {
            tabCat.Visible = this.IsAppointments;
            Cursor.Current = Cursors.WaitCursor;
            var db = Kimtoo.DbManager.Connections.GetConnection();
            //bind dropdowns
            cDentists.DataSource = db.Select<Dentist>();
            cPatients.DataSource = db.Select<Patient>();

            List<Appointment> data = db.Select<Appointment>();

            data = data.Where(r => r.Date.Date == datePick.Value.Date).ToList();
            if (this.IsAppointments)
            {
                if (tabCat.CurrentSelection.Trim() == "Active")
                {
                    data = data.Where(r => !r.HasSessions() && !r.Cancelled).ToList();
                }
                else if (tabCat.CurrentSelection.Trim() == "Cancelled")
                {
                    data = data.Where(r => r.Cancelled).ToList();
                }
            }
            else
            {
                data = data.Where(r => r.HasSessions()).ToList();
            }

            grid.Bind(data);
            Cursor.Current = Cursors.Default;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            pnlDrawwer.Visible = false;
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            pnlDrawwer.Visible = true;
            bindingProvider1.Bind(new Appointment());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //check validation
            if (validationProvider1.Validate().Length > 0) return;
            //get database here - Using Kimtoo Toolkit
            var db = Kimtoo.DbManager.Connections.GetConnection();
            db.Save(bindingProvider1.Get<Appointment>());
            LoadData();
            pnlDrawwer.Visible = false;
            bunifuSnackbar1.Show(this.FindForm(), "Success", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success);
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0) return;

            if (e.ColumnIndex == colEdit.Index)
            {
                pnlDrawwer.Visible = true;

                bindingProvider1.Bind(grid.GetRecord<Appointment>());//Kimtoo Toolkit Feature
                btnSave.Visible = chk.Enabled = !grid.GetRecord<Appointment>().HasSessions();
            }
            if (e.ColumnIndex == ColDel.Index)
            {
                grid.DeleteRow<Appointment>(); //Kimtoo Toolkit Feature
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim().Length > 0)
                grid.SearchRows(txtSearch.Text);
            else
                LoadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadData();
        }

        private void tabCat_OnSelectionChange(object sender, EventArgs e)
        {
            LoadData();
        }

        private void datePick_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnOpen.Text = grid.GetRecord<Appointment>().HasSessions() ? "OPEN TREATMENT SESSION" : "CREATE TREATMENT SESSION";
            btnOpen.Visible = !grid.GetRecord<Appointment>().Cancelled;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var record = grid.GetRecord<Appointment>();
            new Forms.FrmTreatment(record).ShowDialog();
        }

        private void PageAppointments_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}