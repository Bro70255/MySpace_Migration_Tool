using System;
using System.Collections.Generic;
using System.Text;

namespace MySpace_Common.ControllerModels
{
    public class FileDiffViewModel
    {
        public string OldVersion { get; set; }
        public string NewVersion { get; set; }

        public List<string> AddedFiles { get; set; } = new();
        public List<string> DeletedFiles { get; set; } = new();
        public List<string> ModifiedFiles { get; set; } = new();
    }
}
