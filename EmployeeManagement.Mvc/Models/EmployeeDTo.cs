namespace EmployeeManagement.Mvc.Models
{
    public class EmployeeDTo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime JoiningDate { get; set; }
        public int DepartmentId { get; set; }
        public int Salary { get; set; }
        public bool HasLeft { get; set; }
        public DateTime? LeavingDate { get; set; }
    }
}
