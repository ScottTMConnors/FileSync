using DesktopApplication.Objects;
using DesktopApplication.Objects.ViewModels;
using System.ComponentModel;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text.Json;

namespace DesktopApplication.DataAccess {
    internal static class SyncObjectDataAccess {
        internal static List<SyncObject> LoadFromFile(string filePath) {
            try {
                string jsonString = File.ReadAllText(filePath);
                var syncObjects = JsonSerializer.Deserialize<List<SyncObject>>(jsonString);
                if (syncObjects == null) return new List<SyncObject>();
                Properties.Settings.Default.LastSaveFile = filePath;
                Properties.Settings.Default.Save();
                return syncObjects;
            } catch (Exception fileEx) {
                MessageBox.Show($"Error loading file from {filePath}: {fileEx.Message}");
                return new List<SyncObject>();
            }
        }
        internal static string SaveToFile(IEnumerable<SyncObject> syncObjects, string fileName) {
            try {
                string jsonString = JsonSerializer.Serialize(syncObjects);
                File.WriteAllText(fileName, jsonString);
                Properties.Settings.Default.LastSaveFile = fileName;
                Properties.Settings.Default.Save();
                return "File successfully saved";
            } catch (Exception fileEx) {
                return $"Error saving file to {fileName}: {fileEx.Message}";
            }
        }

    }
}
