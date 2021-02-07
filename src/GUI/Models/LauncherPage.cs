using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mefino.GUI.Models
{
    public partial class LauncherPage : MetroFramework.Controls.MetroUserControl
    {
        public LauncherPage()
        {
            InitializeComponent();
        }

        private void _launchOutwardButton_Click(object sender, EventArgs e)
        {
            MefinoApp.TryLaunchOutward();
        }
    }
}
