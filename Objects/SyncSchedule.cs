using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.Objects {
    internal class SyncSchedule {
        internal string Id { get; set; }
        internal SyncObject? syncObject {  get; set; } 
        internal TimeOnly? StartTime { get; set; }
        internal TimeOnly? EndTime { get; set; }






    }
}
