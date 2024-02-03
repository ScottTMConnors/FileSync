using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DesktopApplication.Objects {
    internal class SyncObject {
        public string? Id { get; set; } = new Guid().ToString();
        public string? Name { get; set; }
        public string sourcePath {  get; set; }
        public string destinationPath { get; set; }
        [JsonIgnore]
        public Button sourceButton { get; set; } = new Button { Text = "Select Source", Width=139, Height=49 };
        [JsonIgnore]
        public Button destinationButton { get; set; } = new Button { Text = "Select Destination", Width = 139, Height = 49 };
        [JsonIgnore]
        public Label sourceLabel {  get; set; } = new Label { Width = 200, Height = 49 };
        [JsonIgnore]
        public Label destinationLabel { get; set; } = new Label { Width = 200, Height = 49 };
        [JsonIgnore]
        public Button copyButton { get; set; } = new Button {Text = "Copy", Width = 139, Height = 49 };
        [JsonIgnore]
        public Button StartScheduleButton { get; set; } = new Button { Text = "Start Task", Width = 139, Height = 49 };
        [JsonIgnore]
        public Button EditScheduleButton { get; set; } = new Button { Text = "Edit Schedule", Width = 139, Height = 49 };

        public bool IsRecurring { get; set; }
        public TimeSpan? RecurrenceInterval { get; set; }
        public DateTime? ScheduledTime { get; set; }

    }
}
