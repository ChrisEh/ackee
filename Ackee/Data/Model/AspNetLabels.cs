using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ackee.Data.Model
{
    public class AspNetLabels
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public String LabelID { get; set; }

        [StringLength(128)]
        public String LabelName { get; set; }

        [StringLength(128)]
        public String LabelColor { get; set; }

        public IEnumerable<TaskLabel> TaskLabels { get; set; }
    }
}
