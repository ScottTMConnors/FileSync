using DesktopApplication.DataAccess;
using DesktopApplication.Objects.ViewModels;

namespace DesktopApplication.InterfaceLogic {
    internal class SyncInterfaceLogic {
        internal List<SyncViewModel> syncObjects;

        private DataCopyTool formInstance;

        public SyncInterfaceLogic(DataCopyTool form) {
            formInstance = form;
            syncObjects = new List<SyncViewModel>();
            string saveFile = Properties.Settings.Default.LastSaveFile;
            if ((!string.IsNullOrEmpty(saveFile)) && (File.Exists(saveFile))) {
                var importSyncObjects = SyncObjectDataAccess.LoadViewModelFromFile(saveFile);
                InitializeObjectList(importSyncObjects);
            }
        }

        //Logic methods
        internal void InitializeObjectList(List<SyncViewModel> syncObjects) {
            foreach (var syncObject in syncObjects) {
                InitializeSyncObject(syncObject);
            }
        }

        internal void CreateSyncObject() {
            SyncViewModel syncViewModel = new SyncViewModel();
            InitializeSyncObject(syncViewModel);
        }

        internal void InitializeSyncObject(SyncViewModel syncViewModel) {
            // Add the sync object to list
            syncObjects.Add(syncViewModel);

            if (syncViewModel.sourcePath != null) {
                syncViewModel.sourceLabel.Text = syncViewModel.sourcePath;
            }

            if (syncViewModel.destinationPath != null) {
                syncViewModel.destinationLabel.Text = syncViewModel.destinationPath;
            }


            // Add events to the objects controls
            syncViewModel.sourceButton.Click += (sender, e) => SelectDirectory(syncViewModel, true);
            syncViewModel.destinationButton.Click += (sender, e) => SelectDirectory(syncViewModel, false);
            syncViewModel.copyButton.Click += (sender, e) => FileTransferService.CopyFiles(syncViewModel);
            //syncObject.EditScheduleButton.Click += (sender, e) => new ObjectScheduleForm(syncObject).ShowDialog();

            formInstance.AddObjectToPanel(syncViewModel);
        }

        
        // File transfer methods
        private void SelectDirectory(SyncViewModel syncObject, bool isSourcePath) {
            using (var fbd = new FolderBrowserDialog()) {

                if (fbd.ShowDialog() == DialogResult.OK) {

                    if (FileTransferService.CheckPath(fbd.SelectedPath)) {
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

        //Sync Object Data Access methods

        internal void SaveSettings() {
            try {
                using (var fbd = new SaveFileDialog()) {
                    fbd.CheckWriteAccess = true;
                    fbd.CheckPathExists = true;
                    fbd.Filter = "JSON files (*.json)|*.json";
                    fbd.RestoreDirectory = true;
                    if (fbd.ShowDialog() == DialogResult.OK) {
                        SyncObjectDataAccess.SaveSyncVMToFile(syncObjects, fbd.FileName);
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        internal void LoadSettings() {
            try {
                string filePath = "";
                var openFileDialog = new OpenFileDialog() {
                    Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                    Title = "Open SyncObject File"
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    filePath = openFileDialog.FileName;
                    MessageBox.Show("File selected: " + filePath);
                    var importSyncObjects = SyncObjectDataAccess.LoadViewModelFromFile(filePath);
                    if (importSyncObjects != null) {
                        MessageBox.Show("Save loaded successfully");
                        ResetSettings();
                        InitializeObjectList(importSyncObjects);
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        internal void ResetSettings() {
            syncObjects = new List<SyncViewModel>();
            formInstance.clearPanel();
            Properties.Settings.Default.LastSaveFile = "";
        }


    }
}
