using DesktopApplication.Objects;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text.Json;

namespace DesktopApplication.Repository {
    internal static class SyncObjectRepository {
        internal static List<SyncObject> LoadFromFile(string filePath) {
            try {
                string jsonString = File.ReadAllText(filePath);
                // Save the path to app settings
                Properties.Settings.Default.LastSaveFile = filePath;
                Properties.Settings.Default.Save();
                return JsonSerializer.Deserialize<List<SyncObject>>(jsonString);
            } catch (Exception fileEx) {
                MessageBox.Show($"Error loading file from {filePath}: {fileEx.Message}");
                return new List<SyncObject>();
            }
        }

        internal static void SaveToFile(List<SyncObject> syncObjects, string fileName) {
            try {

                string jsonString = JsonSerializer.Serialize(syncObjects);
                File.WriteAllText(fileName, jsonString);

                // Save the path to app settings
                Properties.Settings.Default.LastSaveFile = fileName;
                Properties.Settings.Default.Save();
                MessageBox.Show("File successfully saved");
            } catch (Exception fileEx) {
                // Handle exceptions for individual files here if needed
                MessageBox.Show($"Error saving file to {fileName}: {fileEx.Message}");
                // Optionally, rethrow the exception to be caught by AggregateException
                throw;
            }
        }
        internal static bool CheckPath(string filePath) {
            try {
                if (!Directory.Exists(filePath)) {
                    MessageBox.Show($"Path does not exist: {filePath}");
                    return false;
                }

                // Check if the destination path exists. If not, create it.
                //if (!Directory.Exists(savePath)) {
                //    Directory.CreateDirectory(savePath);
                //}


                if (!HasFolderPermission(filePath, FileSystemRights.Read)) {
                    MessageBox.Show("Insufficient read permissions on the source folder. Please update the folder permissions.");
                    return false; // Exit the method
                }

                // Check write permission on the destination folder
                if (!HasFolderPermission(filePath, FileSystemRights.Write)) {
                    MessageBox.Show("Insufficient write permissions on the destination folder. Please update the folder permissions.");
                    return false; // Exit the method
                }
            } catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
            return true;
        }

        internal static bool CheckPath(string sourcePath, string DestinationPath) {
            if (!CheckPath(sourcePath)) {
                return false;
            }
            if (!CheckPath(DestinationPath)) {
                return false;
            }

            if (Path.GetFullPath(DestinationPath).StartsWith(Path.GetFullPath(sourcePath), StringComparison.OrdinalIgnoreCase)) {
                MessageBox.Show("Destination path cannot be a subdirectory of the source path.");
                return false;
            }
            return true;
        }

        internal static bool HasFolderPermission(string folderPath, FileSystemRights accessRight) {
            var isInRoleWithAccess = false;
            try {
                var directoryInfo = new DirectoryInfo(folderPath);
                var directorySecurity = directoryInfo.GetAccessControl();
                var acl = directorySecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));

                var currentUser = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(currentUser);

                foreach (FileSystemAccessRule rule in acl) {
                    if ((accessRight & rule.FileSystemRights) != 0) {
                        if (principal.IsInRole(rule.IdentityReference as SecurityIdentifier)) {
                            if (rule.AccessControlType == AccessControlType.Allow) {
                                isInRoleWithAccess = true;
                            } else if (rule.AccessControlType == AccessControlType.Deny) {
                                return false;
                            }
                        }
                    }
                }
            } catch (UnauthorizedAccessException) {
                isInRoleWithAccess = false;
            }

            return isInRoleWithAccess;
        }

        internal static void CopyFiles(SyncObject syncObject) {
            string sourcePath = syncObject.sourcePath;
            string destinationPath = syncObject.destinationPath;
            CopyFiles(sourcePath, destinationPath);
        }

        internal static void CopyFiles(string sourcePath, string destinationPath) {
            try {
                if (CheckPath(sourcePath, destinationPath)) {
                    string[] files = Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories);


                    // Multi threaded copy process
                    Parallel.ForEach(files, (file) => {
                        try {
                            string relativePath = file.Substring(sourcePath.Length + 1);
                            string destFile = Path.Combine(destinationPath, relativePath);

                            string destDirectory = Path.GetDirectoryName(destFile);
                            if (!Directory.Exists(destDirectory)) {
                                Directory.CreateDirectory(destDirectory);
                            }

                            if (!File.Exists(destFile) || File.GetLastWriteTime(file) > File.GetLastWriteTime(destFile)) {
                                File.Copy(file, destFile, true);
                            }
                        } catch (Exception fileEx) {
                            Console.WriteLine($"Error copying file {file}: {fileEx.Message}");
                            throw;
                        }
                    });
                    MessageBox.Show("Files copied successfully!");
                };
            } catch (AggregateException aggEx) {
                foreach (var ex in aggEx.InnerExceptions) {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            } catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }



    }
}
