using System;
using System.Collections.Generic;
using System.Text;

namespace MySpace_Common
{
    public class FileNode
    {
        public string Name { get; set; }
        public bool IsDirectory { get; set; }
        public List<FileNode> Children { get; set; } = new();
    }
}
