using DesktopApplication.Objects;
using DesktopApplication.Objects.ViewModels;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Security.AccessControl;
using System.Security.Principal;

namespace DesktopApplication.DataAccess {
    internal static class FileTransferService {

        public delegate void ProgressReportDelegate(int percentage);
        internal static async void CopyFiles(SyncObject syncObject) {
            string sourcePath = syncObject.sourcePath;
            string destinationPath = syncObject.destinationPath;
            CopyFiles(sourcePath, destinationPath);
        }

        internal static void CopyFiles(string sourcePath, string destinationPath, ProgressReportDelegate progressCallback = null, ConcurrentBag<Exception> exceptionsBag = null) {
            try {
                if (CheckPath(sourcePath, destinationPath)) {
                    IEnumerable<string> files = SafeEnumerateFiles(sourcePath, "*", SearchOption.AllDirectories);
                    int totalFiles = files.Count();
                    int filesCopied = 0;

                    Parallel.ForEach(files, (file) => {
                        try {
                            string relativePath = file.Substring(sourcePath.Length).TrimStart(Path.DirectorySeparatorChar);
                            string destFile = Path.Combine(destinationPath, relativePath);

                            string destDirectory = Path.GetDirectoryName(destFile);
                            if (!Directory.Exists(destDirectory)) {
                                Directory.CreateDirectory(destDirectory);
                            }

                            if (!File.Exists(destFile) || File.GetLastWriteTime(file) > File.GetLastWriteTime(destFile)) {
                                File.Copy(file, destFile, true);
                            }

                            Interlocked.Increment(ref filesCopied);
                            int progressPercentage = (int)((double)filesCopied / totalFiles * 100);
                            progressCallback?.Invoke(progressPercentage);
                        } catch (Exception fileEx) {
                            exceptionsBag?.Add(fileEx);
                        }
                    });
                }
            } catch (Exception ex) {
                exceptionsBag?.Add(ex);
            }
        }

        internal static bool CheckPath(string filePath) {
            try {
                if (!Directory.Exists(filePath)) {
                    MessageBox.Show($"Path does not exist: {filePath}");
                    return false;
                }
                if (!HasFolderPermission(filePath, FileSystemRights.Read)) {
                    MessageBox.Show("Insufficient read permissions on the source folder. Please update the folder permissions.");
                    return false;
                }
                if (!HasFolderPermission(filePath, FileSystemRights.Write)) {
                    MessageBox.Show("Insufficient write permissions on the destination folder. Please update the folder permissions.");
                    return false;
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

        private static bool HasFolderPermission(string folderPath, FileSystemRights accessRight) {
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
        private static IEnumerable<string> SafeEnumerateFiles(string path, string searchPattern, SearchOption searchOption) {
            var dirs = new Stack<string>();
            dirs.Push(path);

            while (dirs.Count > 0) {
                string currentDirPath = dirs.Pop();
                if (!Directory.Exists(currentDirPath)) {
                    continue;
                }

                string[] subDirs;
                string[] files;

                try {
                    subDirs = Directory.GetDirectories(currentDirPath);
                    files = Directory.GetFiles(currentDirPath, searchPattern);
                } catch (UnauthorizedAccessException e) {
                    Console.WriteLine($"Access denied for directory {currentDirPath}: {e.Message}");
                    continue;
                } catch (DirectoryNotFoundException e) {
                    Console.WriteLine($"{currentDirPath} not found: {e.Message}");
                    continue;
                }

                foreach (var file in files) {
                    yield return file;
                }

                if (searchOption == SearchOption.AllDirectories) {
                    foreach (string subDir in subDirs) {
                        dirs.Push(subDir);
                    }
                }
            }
        }
    }
}
