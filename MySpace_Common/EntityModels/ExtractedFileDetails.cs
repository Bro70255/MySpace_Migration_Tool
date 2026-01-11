using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MySpace_Common
{
    public class ExtractedFileDetails
    {
        [Key] // ✅ REQUIRED
        public int ExtractedId { get; set; }

        public int? ParentFileId { get; set; }

        public string ExtractedName { get; set; }
        public string ExtractedPath { get; set; }
        public string ExtractedType { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
