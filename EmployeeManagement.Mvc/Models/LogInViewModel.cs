namespace EmployeeManagement.Mvc.Models
{
    public class LogInViewModel
    {
        public LogInApplicationUserDto LogInData { get; set; } = new LogInApplicationUserDto();
        public IEnumerable<RoleDto> RoleList { get; set; } = new List<RoleDto>();
    }
}
