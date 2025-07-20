using Dental_Management.Models;
using Kimtoo.DbManager;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dental_Management.Lib;

namespace Dental_Management.Forms
{
    public partial class FrmClinic : Form
    {
        private Clinic record;

        public FrmClinic()
        {
            InitializeComponent();
            this.record = Connections.GetConnection().Select<Clinic>().FirstOrDefault();
            bindingProvider1.Bind(record);
            picLogo.Image = record.Logo.ToImage();
            Cursor.Current = Cursors.Default;
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            picLogo.Image = Image.FromFile(openFileDialog1.FileName);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            record.Logo = picLogo.Image.ToBytes();
            Connections.GetConnection().Save(record);
            bunifuSnackbar1.Show(this.FindForm(), "Saved", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success);
        }

        private void FrmClinic_Load(object sender, EventArgs e)
        {

        }
    }
}