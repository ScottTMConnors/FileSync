using DesktopApplication.Objects;
using DesktopApplication.Objects.ViewModels;
using System.ComponentModel;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text.Json;

namespace DesktopApplication.DataAccess
{
    internal static class SyncObjectDataAccess
    {
        internal static List<SyncObject> LoadFromFile(string filePath)
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                // Deserialize JSON to List<SyncObject>
                var syncObjects = JsonSerializer.Deserialize<List<SyncObject>>(jsonString);
                if (syncObjects == null) return new List<SyncObject>(); // Return an empty list if deserialization fails


                // Save the path to app settings
                Properties.Settings.Default.LastSaveFile = filePath;
                Properties.Settings.Default.Save();

                return syncObjects;
            }
            catch (Exception fileEx)
            {
                MessageBox.Show($"Error loading file from {filePath}: {fileEx.Message}");
                return new List<SyncObject>();
            }
        }

        internal static List<SyncViewModel> LoadViewModelFromFile(string filePath)
        {
            var syncObjects = LoadFromFile(filePath);
            var syncViewModels = ListSyncObjectToViewModel(syncObjects);
            return syncViewModels;
        }

        internal static List<SyncViewModel> ListSyncObjectToViewModel(IEnumerable<SyncObject> syncObjects)
        {
            return syncObjects.Select(syncObject => new SyncViewModel(syncObject)).ToList();
        }

        internal static List<SyncObject> ListViewModelToSyncObject(IEnumerable<SyncViewModel> viewModels)
        {
            return viewModels.Select(viewModel => viewModel.ToSyncObject()).ToList();
        }

        internal static void SaveToFile(IEnumerable<SyncObject> syncObjects, string fileName)
        {
            try
            {

                string jsonString = JsonSerializer.Serialize(syncObjects);
                File.WriteAllText(fileName, jsonString);

                // Save the path to app settings
                Properties.Settings.Default.LastSaveFile = fileName;
                Properties.Settings.Default.Save();
                MessageBox.Show("File successfully saved");
            }
            catch (Exception fileEx)
            {
                // Handle exceptions for individual files here if needed
                MessageBox.Show($"Error saving file to {fileName}: {fileEx.Message}");
                // Optionally, rethrow the exception to be caught by AggregateException
                throw;
            }
        }
        internal static void SaveSyncVMToFile(IEnumerable<SyncViewModel> viewModels, string fileName) {
            var syncObjects = ListViewModelToSyncObject(viewModels);
            SaveToFile(syncObjects, fileName);
        }
        






    }
}
