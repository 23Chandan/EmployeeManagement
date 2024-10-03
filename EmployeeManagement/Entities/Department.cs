using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Entities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required"),MaxLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage ="IsActive is required")]
        public bool ISActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreadedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
