using DesktopApplication.DataAccess;
using DesktopApplication.Objects;
using DesktopApplication.Objects.ViewModels;
using System.ComponentModel;

namespace DesktopApplication.InterfaceLogic {
    internal class SyncInterfaceLogic {
        private List<SyncViewModel> syncObjects;

        private DataCopyTool formInstance;

        private string saveFile;

        public SyncInterfaceLogic(DataCopyTool form) {
            formInstance = form;
            syncObjects = new List<SyncViewModel>();
            saveFile = Properties.Settings.Default.LastSaveFile;
            if ((!string.IsNullOrEmpty(saveFile)) && (File.Exists(saveFile))) {
                var importSyncObjects = ListSyncObjectToViewModel(SyncObjectDataAccess.LoadFromFile(saveFile));
                InitializeObjectList(importSyncObjects);
            }
        }

        private void SaveData() {
            if ((!string.IsNullOrEmpty(saveFile)) && (File.Exists(saveFile))) {
                SyncObjectDataAccess.SaveToFile(ListViewModelToSyncObject(syncObjects), saveFile);
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

        internal void ToggleSync(SyncViewModel syncViewModel) {
            syncViewModel.IsRecurring = syncViewModel.RecurringCheckbox.Checked;
            SaveData();
            //activate deactivate service
        }

        internal void InitializeSyncObject(SyncViewModel syncViewModel) {
            syncObjects.Add(syncViewModel);

            if (syncViewModel.sourcePath != null) {
                syncViewModel.sourceLabel.Text = syncViewModel.sourcePath;
            }

            if (syncViewModel.destinationPath != null) {
                syncViewModel.destinationLabel.Text = syncViewModel.destinationPath;
            }

            syncViewModel.sourceButton.Click += (sender, e) => SelectDirectory(syncViewModel, true);
            syncViewModel.destinationButton.Click += (sender, e) => SelectDirectory(syncViewModel, false);
            syncViewModel.copyButton.Click += (sender, e) => CopyFilesInterface(syncViewModel);
            //syncObject.EditScheduleButton.Click += (sender, e) => new ObjectScheduleForm(syncObject).ShowDialog();
            syncViewModel.RecurringCheckbox.CheckedChanged += (sender, e) => ToggleSync(syncViewModel);

            formInstance.AddObjectToPanel(syncViewModel);
            SaveData();
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
                        SaveData();
                    }
                }
            }
        }

        internal static async void CopyFilesInterface(SyncViewModel syncViewModel) {
            string sourcePath = syncViewModel.sourcePath;
            string destinationPath = syncViewModel.destinationPath;

            var progressForm = new progressForm();
            progressForm.Show();

            var backgroundWorker = new BackgroundWorker {
                WorkerReportsProgress = true
            };

            backgroundWorker.DoWork += (sender, e) => {
                FileTransferService.CopyFiles(sourcePath, destinationPath, backgroundWorker);
            };

            backgroundWorker.ProgressChanged += (sender, e) => {
                progressForm.UpdateProgress(e.ProgressPercentage);
            };

            backgroundWorker.RunWorkerCompleted += (sender, e) => {
                progressForm.Close();
                MessageBox.Show("Copy operation completed.");
            };

            backgroundWorker.RunWorkerAsync();
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
                        MessageBox.Show(SyncObjectDataAccess.SaveToFile(ListViewModelToSyncObject(syncObjects), fbd.FileName));
                        saveFile = fbd.FileName;
                        Properties.Settings.Default.LastSaveFile = fbd.FileName;
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
                    var importSyncObjects = ListSyncObjectToViewModel(SyncObjectDataAccess.LoadFromFile(filePath));
                    if (importSyncObjects != null) {
                        MessageBox.Show("Save loaded successfully");
                        ResetSettings();
                        InitializeObjectList(importSyncObjects);
                        saveFile = filePath;
                        Properties.Settings.Default.LastSaveFile = filePath;
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

        internal static List<SyncViewModel> ListSyncObjectToViewModel(IEnumerable<SyncObject> syncObjects) {
            return syncObjects.Select(syncObject => new SyncViewModel(syncObject)).ToList();
        }

        internal static List<SyncObject> ListViewModelToSyncObject(IEnumerable<SyncViewModel> viewModels) {
            return viewModels.Select(viewModel => viewModel.ToSyncObject()).ToList();
        }


    }
}
