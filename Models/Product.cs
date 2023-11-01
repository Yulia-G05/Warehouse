using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string ProductName { get; set; } = "";

        [ForeignKey("DepartmentID")]
        public int DepartmentID { get; set; }

    }
}
