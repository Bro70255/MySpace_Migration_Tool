using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MySpace_Common
{
    [Table("FileChildDetail")]
    public class FileChildDetail
    {
        [Key] // ✅ REQUIRED
        public int Id { get; set; }

        public int ParentFileId { get; set; }

        public string Name { get; set; }      // function / method name

        public string Type { get; set; }      // cshtml-function, js-function, etc.

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }

}
