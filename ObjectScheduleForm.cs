using DesktopApplication.Objects;
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
    internal partial class ObjectScheduleForm : Form {
        private SyncObject syncObject;

        internal ObjectScheduleForm() {
            InitializeComponent();
        }

        internal ObjectScheduleForm(SyncObject syncObject) {
            this.syncObject = syncObject;
        }
    }
}
