using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string DepartmenName { get; set; }


    }
}
