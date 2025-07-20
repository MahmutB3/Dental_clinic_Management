using Dental_Management.Lib;
using Dental_Management.Models;
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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            try
            {
                if (Connections.GetConnection().Select<Clinic>().Count() == 0)
                    Connections.GetConnection().Save(
                        new Clinic
                        {
                            Name = "GoldenTooth",
                            Email = "Dr_Kerim@gmail.com",
                            Password = "M1234M",
                            Address = "Mersin, Mezitli",
                            Logo = Image.FromFile("logo.png").ToBytes(),
                            Phone = "5556235666"
                        });
            }
            catch (Exception)
            {
                Connections.Show();
            }
            InitializeComponent();
        }

        private void menu_OnItemSelected(object sender, string path, EventArgs e)
        {
            if (path == "Settings.Database")
            {
                Kimtoo.DbManager.Config.Theme = Color.FromArgb(25, 144, 234);
                Connections.Show();
                return;
            }
            if (path == "Settings.Clinic")
            {
                Cursor.Current = Cursors.WaitCursor;
                new FrmClinic().ShowDialog();
                return;
            }

            bunifuAppBar1.Title = path;
            pages.SetPage(path);
        }
            
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            bunifuFormDock1.WindowState = Bunifu.UI.WinForms.BunifuFormDock.FormWindowStates.Normal;
            picLogo.Image = Connections.GetConnection().Select<Clinic>().FirstOrDefault().Logo.ToImage();

#if DEBUG
            //to speed up debuging process
            txtEmail.Text = "Dr_Kerim@gmail.com";
            txtPassword.Text = "M1234M";
#endif
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (validationProvider1.Validate().Length > 0) return;

            var record = Connections.GetConnection().Select<Clinic>().FirstOrDefault();
            if (record.Email.ToLower().Trim() != txtEmail.Text.ToLower().Trim() || record.Password.Trim() != txtPassword.Text.Trim())
            {
                bunifuSnackbar1.Show(this, "Incorrect username or password ", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }

            pages.SetPage("Dashboard");
            bunifuAppBar1.Title = "Dashboard";
            menu.Enabled = true;
            bunifuSnackbar1.Show(this, "Success", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success);
            label1.Text = record.Email.Split('@')[0].ToLower().Trim();
        }

        private void pageDashboard1_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuAppBar1_IconClick(object sender, EventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }
    }
}