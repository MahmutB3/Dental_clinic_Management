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

namespace Dental_Management.Pages
{
    public partial class PagePatients : UserControl
    {
        public PagePatients()
        {
            if (this.IsInDesignMode()) return;
            InitializeComponent();
            LoadData();

            grid.OnDelete<Patient>((a, b) => Kimtoo.DbManager.Connections.GetConnection().Delete(a) >= 0);
        }

        private void LoadData()
        {
            var db = Kimtoo.DbManager.Connections.GetConnection();
            List<Patient> data = db.Select<Patient>();
            grid.Bind(data);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            pnlDrawwer.Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            pnlDrawwer.Visible = true;
            bindingProvider1.Bind(new Patient());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //check validation
            if (validationProvider1.Validate().Length > 0) return;
            //get database here - Using Kimtoo Toolkit
            var db = Kimtoo.DbManager.Connections.GetConnection();
            db.Save(bindingProvider1.Get<Patient>());
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
                bindingProvider1.Bind(grid.GetRecord<Patient>());//Kimtoo Toolkit Feature
            }
            if (e.ColumnIndex == ColDel.Index)
                grid.DeleteRow<Patient>(); //Kimtoo Toolkit Feature
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

        private void PagePatients_Load(object sender, EventArgs e)
        {

        }
    }
}