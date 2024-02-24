using System.Text.Json.Serialization;

namespace DesktopApplication.Objects {
    internal class SyncObject {
        public string? Id { get; set; } = new Guid().ToString();
        public string? Name { get; set; }
        public string sourcePath {  get; set; }
        public string destinationPath { get; set; }
        public bool IsRecurring { get; set; }

    }
}
