using Dental_Management.Lib;
using Dental_Management.Models;
using Kimtoo.DbManager;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dental_Management
{
    internal static class Program
    {
        private static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6)
               SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.FrmMain());
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        public static bool IsInDesignMode(this UserControl container)
        {
            if (Application.ExecutablePath.IndexOf("devenv.exe",
                StringComparison.OrdinalIgnoreCase) > -1)
            {
                container.Controls.Add(new Label()
                {
                    Text = container.GetType().Name,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                });

                return true;
            }
            return false;
            /**********************************/
        }
    }
}