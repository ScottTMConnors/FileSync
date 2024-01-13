using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;
using System.Xml.Linq;
using DesktopApplication.Objects;
using DesktopApplication.Repository;

namespace DesktopApplication {
    public partial class DataCopyTool : Form {
        private Label emptyMessageLabel;

        private List<SyncObject> syncObjects;

        public DataCopyTool() {
            InitializeComponent();
            syncObjects = new List<SyncObject>();
            string saveFile = Properties.Settings.Default.LastSaveFile;
            if ((!string.IsNullOrEmpty(saveFile)) && (File.Exists(saveFile))) {
                var importSyncObjects = SyncObjectRepository.LoadFromFile(saveFile);
                InitializeObjectList(importSyncObjects);
            }
        }

        private void InitializeDynamicPanel() {
            emptyMessageLabel = new Label() { Text = "No rows added. Click 'Add Row' to start." };
            dynamicPanel.Controls.Add(emptyMessageLabel);
        }

        private void InitializeObjectList(List<SyncObject> syncObjects) {
            foreach (var syncObject in syncObjects) {
                InitializeSyncObject(syncObject);
            }
        }

        private void SelectDirectory(SyncObject syncObject, bool isSourcePath) {
            using (var fbd = new FolderBrowserDialog()) {

                if (fbd.ShowDialog() == DialogResult.OK) {

                    if (SyncObjectRepository.CheckPath(fbd.SelectedPath)) {
                        if (isSourcePath) {
                            syncObject.sourcePath = fbd.SelectedPath;
                            syncObject.sourceLabel.Text = fbd.SelectedPath;
                        } else {
                            syncObject.destinationPath = fbd.SelectedPath;
                            syncObject.destinationLabel.Text = fbd.SelectedPath;
                        }
                    }
                }
            }
        }


        private void addRow_Click(object sender, EventArgs e) {
            SyncObject syncObject = new SyncObject();
            InitializeSyncObject(syncObject);

        }

        // Overload to handler brand new object
        private void InitializeSyncObject(SyncObject syncObject) {

            if (syncObject.sourcePath != null) {
                syncObject.sourceLabel.Text = syncObject.sourcePath;
            }

            if (syncObject.destinationPath != null) {
                syncObject.destinationLabel.Text = syncObject.destinationPath;
            }


            // Add events to the objects controls
            syncObject.sourceButton.Click += (sender, e) => SelectDirectory(syncObject, true);
            syncObject.destinationButton.Click += (sender, e) => SelectDirectory(syncObject, false);
            syncObject.copyButton.Click += (sender, e) => SyncObjectRepository.CopyFiles(syncObject);
            syncObject.EditScheduleButton.Click += (sender, e) => new ObjectScheduleForm(syncObject).ShowDialog();

            AddObjectToPanel(syncObject);
        }

        private void AddObjectToPanel(SyncObject syncObject) {

            // Add the sync object to list
            syncObjects.Add(syncObject);

            // Add controls for each control field
            dynamicPanel.Controls.Add(syncObject.sourceButton);
            dynamicPanel.Controls.Add(syncObject.sourceLabel);
            dynamicPanel.Controls.Add(syncObject.destinationButton);
            dynamicPanel.Controls.Add(syncObject.destinationLabel);
            dynamicPanel.Controls.Add(syncObject.EditScheduleButton);
            dynamicPanel.Controls.Add(syncObject.copyButton);
        }


        private void clearPanel() {
            dynamicPanel.Controls.Clear();
            Properties.Settings.Default.LastSaveFile = "";
            syncObjects = new List<SyncObject>();
        }

        private void SaveButton_Click(object sender, EventArgs e) {
            try {
                using (var fbd = new SaveFileDialog()) {
                    fbd.CheckWriteAccess = true;
                    fbd.CheckPathExists = true;
                    fbd.Filter = "JSON files (*.json)|*.json";
                    fbd.RestoreDirectory = true;
                    if (fbd.ShowDialog() == DialogResult.OK) {
                        SyncObjectRepository.SaveToFile(syncObjects, fbd.FileName);
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void LoadButton_Click(object sender, EventArgs e) {
            try {
                string filePath = "";
                var openFileDialog = new OpenFileDialog() {
                    Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                    Title = "Open SyncObject File"
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    filePath = openFileDialog.FileName;
                    MessageBox.Show("File selected: " + filePath);
                    var importSyncObjects = SyncObjectRepository.LoadFromFile(filePath);
                    if (importSyncObjects != null) {
                        MessageBox.Show("Save loaded successfully");
                        clearPanel();
                        InitializeObjectList(importSyncObjects);
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ClearButton_Click(object sender, EventArgs e) {
            clearPanel();
        }

    }
}
