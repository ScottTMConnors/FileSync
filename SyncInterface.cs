using DesktopApplication.InterfaceLogic;
using DesktopApplication.Objects.ViewModels;

namespace DesktopApplication
{
    public partial class DataCopyTool : Form {
        private SyncInterfaceLogic InterfaceLogic;

        public DataCopyTool() {
            InitializeComponent();
            InitializeDynamicPanel();
            InterfaceLogic = new SyncInterfaceLogic(this);
        }

        private void InitializeDynamicPanel() {
            dynamicPanel.Controls.Add(new Label() { Text = "No rows added. Click 'Add Row' to start." });
        }

        private void addRow_Click(object sender, EventArgs e) {
            InterfaceLogic.CreateSyncObject();

        }
        private void SaveButton_Click(object sender, EventArgs e) {
            InterfaceLogic.SaveSettings();
        }

        private void LoadButton_Click(object sender, EventArgs e) {
            InterfaceLogic.LoadSettings();
        }

        private void ClearButton_Click(object sender, EventArgs e) {
            InterfaceLogic.ResetSettings();
        }
        internal void clearPanel() {
            dynamicPanel.Controls.Clear();
        }

        internal void AddObjectToPanel(SyncViewModel syncViewModel) {
            dynamicPanel.Controls.Add(syncViewModel.sourceButton);
            dynamicPanel.Controls.Add(syncViewModel.sourceLabel);
            dynamicPanel.Controls.Add(syncViewModel.destinationButton);
            dynamicPanel.Controls.Add(syncViewModel.destinationLabel);
            dynamicPanel.Controls.Add(syncViewModel.EditScheduleButton);
            dynamicPanel.Controls.Add(syncViewModel.copyButton);
        }

    }
}
