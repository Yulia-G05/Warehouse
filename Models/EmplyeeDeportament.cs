using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models
{
    public class EmployeeDeportament
    {
        [Key]
        public int DepartmentEmployeeID { get; set; }
        [ForeignKey("DepartmentID")]
        public int DepartmentID { get; set; }
        [ForeignKey("EmployeeID")]
        public int EmployeeID { get; set; } 
        
        


    }
}
