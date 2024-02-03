using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApplication {
    public partial class progressForm : Form {
        public progressForm() {
            InitializeComponent();
        }

        public void UpdateProgress(int progress) {
            if (InvokeRequired) {
                Invoke(new Action<int>(UpdateProgress), progress);
            } else {
                progressBar.Value = progress;
                progressLabel.Text = $"{progress}%";
            }
        }


    }
}
