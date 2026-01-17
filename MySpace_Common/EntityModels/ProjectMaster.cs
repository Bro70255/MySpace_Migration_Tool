using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MySpace_Common.EntityModels
{
    public class ProjectMaster
    {
        [Key] // ✅ Explicit primary key
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectType { get; set; }
        public string ProjectFlow { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }

}
