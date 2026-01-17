using System;
using System.Collections.Generic;
using System.Text;

namespace MySpace_Common.ControllerModels
{
    public class ProjectCreateDto
    {
        public string ProjectName { get; set; }
        public string ProjectType { get; set; }
        public List<string> ProjectFlow { get; set; }
    }
}
