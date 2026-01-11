using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MySpace_Common
{
    [Table("BankTransferStatusResponse")]
    public class BankTransferStatusResponse
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }   // ✅ new column
        public string Status { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ResponseData { get; set; }
        public DateTime CreatedOn { get; set; }
    }

}
