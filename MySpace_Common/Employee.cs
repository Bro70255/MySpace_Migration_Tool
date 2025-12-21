using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySpace_Common
{
    [Table("EmployeeMaster")]   // <-- Maps to your SQL table
    public class Employee
    {
        [Key]
        public int Emp_ID { get; set; }

        public string Emp_Name { get; set; }
        public int Emp_Code { get; set; }
        public string Password { get; set; }
        public int Branch_ID { get; set; }
        public bool IsActive { get; set; }
    }

}
