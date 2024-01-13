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
        public Button sourceButton { get; set; } = new Button { Text = "Select Source" };
        [JsonIgnore]
        public Button destinationButton { get; set; } = new Button { Text = "Select Destination" };
        [JsonIgnore]
        public Label sourceLabel {  get; set; } = new Label();
        [JsonIgnore]
        public Label destinationLabel { get; set; } = new Label();
        [JsonIgnore]
        public Button copyButton { get; set; } = new Button { Text = "Copy" };
        [JsonIgnore]
        public Button StartScheduleButton { get; set; } = new Button { Text = "Start Task" };
        [JsonIgnore]
        public Button EditScheduleButton { get; set; } = new Button { Text = "Edit Schedule" };

        public bool IsRecurring { get; set; }
        public TimeSpan? RecurrenceInterval { get; set; }
        public DateTime? ScheduledTime { get; set; }

    }
}
