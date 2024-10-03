using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Core.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(100, ErrorMessage = "First Name should not be more than 100 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Middle Name is required.")]
        [StringLength(100, ErrorMessage = "Middle Name should not be more than 100 characters.")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(100, ErrorMessage = "Last Name should not be more than 100 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address1 is required.")]
        [StringLength(200, ErrorMessage = "Address should not be more than 200 characters.")]
        public string Address1 { get; set; }

        [StringLength(200, ErrorMessage = "Address2 should not be more than 200 characters.")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(200, ErrorMessage = "City should not be more than 200 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [StringLength(50, ErrorMessage = "State should not be more than 50 characters.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip is required.")]
        [StringLength(10, ErrorMessage = "Zip should not be more than 10 characters.")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Joining Date is required.")]
        public DateTime JoiningDate { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        public int Salary { get; set; }

        public bool HasLeft { get; set; }
        public DateTime? LeavingDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
