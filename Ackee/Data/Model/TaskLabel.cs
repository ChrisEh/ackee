using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ackee.Data.Model
{
    public class TaskLabel
    {
        public string TaskID { get; set; }
        public AspNetTasks Task { get; set; }
        public string LabelID { get; set; }
        public AspNetLabels Label { get; set; }
    }
}
