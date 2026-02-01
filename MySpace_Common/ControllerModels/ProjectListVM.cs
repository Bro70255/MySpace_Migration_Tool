using System;
using System.Collections.Generic;
using System.Text;

namespace MySpace_Common.ControllerModels
{
    public class ProjectListVM
    {
        public string ProjectName { get; set; }
        public List<ProjectVersionVM> Versions { get; set; } = new();
    }
}
