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
        [JsonIgnore]
        public CheckBox RecurringCheckbox { get; set; } = new CheckBox { Text = "Sync", Width = 139, Height = 49, Checked = false };

        public new string sourcePath {
            get { return base.sourcePath; }
            set {
                base.sourcePath = value;
                sourceLabel.Text = value;
            }
        }

        public new string destinationPath {
            get { return base.destinationPath; }
            set {
                base.destinationPath = value;
                destinationLabel.Text = value;
            }
        }
        public new bool IsRecurring {
            get { return base.IsRecurring; }
            set {
                base.IsRecurring = value;
                RecurringCheckbox.Checked = value;
            }
        }

        // Constructor
        public SyncViewModel() {
            // Initialize sourceLabel.Text with sourcePath if it's not null
            sourceLabel.Text = base.sourcePath ?? string.Empty;
            destinationLabel.Text = base.destinationPath ?? string.Empty;
            RecurringCheckbox.Checked = base.IsRecurring;
        }

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
