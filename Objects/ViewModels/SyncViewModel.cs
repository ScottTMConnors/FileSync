using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DesktopApplication.Objects.ViewModels {
    internal class SyncViewModel : SyncObject {
        [JsonIgnore]
        public Button sourceButton { get; set; } = new Button { Text = "Select Source", Width = 139, Height = 49 };
        [JsonIgnore]
        public Button destinationButton { get; set; } = new Button { Text = "Select Destination", Width = 139, Height = 49 };
        [JsonIgnore]
        public Label sourceLabel { get; set; } = new Label { Width = 200, Height = 49 };
        [JsonIgnore]
        public Label destinationLabel { get; set; } = new Label { Width = 200, Height = 49 };
        [JsonIgnore]
        public Button copyButton { get; set; } = new Button { Text = "Copy", Width = 139, Height = 49 };
        [JsonIgnore]
        public Button StartScheduleButton { get; set; } = new Button { Text = "Start Task", Width = 139, Height = 49 };
        [JsonIgnore]
        public Button EditScheduleButton { get; set; } = new Button { Text = "Edit Schedule", Width = 139, Height = 49 };

        public new string sourcePath {
            get { return base.sourcePath; }
            set {
                base.sourcePath = value;
                sourceLabel.Text = value; // Update the label whenever sourcePath is set
            }
        }

        // Constructor
        public SyncViewModel() {
            // Initialize sourceLabel.Text with sourcePath if it's not null
            sourceLabel.Text = base.sourcePath ?? string.Empty;
            destinationLabel.Text = base.destinationPath ?? string.Empty;
        }

        // Optionally, add a method or constructor to facilitate conversion from SyncObject
        public SyncViewModel(SyncObject syncObject) : this() { // Call default constructor first
            // Manually copy properties from SyncObject to this SyncViewModel
            this.Id = syncObject.Id;
            this.Name = syncObject.Name;
            this.sourcePath = syncObject.sourcePath; // This will also update the sourceLabel.Text
            this.destinationPath = syncObject.destinationPath;
            this.IsRecurring = syncObject.IsRecurring;
        }

        public SyncObject ToSyncObject() {
            return new SyncObject {
                Id = this.Id,
                Name = this.Name,
                sourcePath = this.sourcePath,
                destinationPath = this.destinationPath,
                IsRecurring = this.IsRecurring
            };
        }
    }
}
