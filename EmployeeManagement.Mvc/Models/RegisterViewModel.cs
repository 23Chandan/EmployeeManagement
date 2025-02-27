namespace EmployeeManagement.Mvc.Models
{
    public class RegisterViewModel
    {
        public SignInApplicationUserDto RegisterModel { get; set; } = new SignInApplicationUserDto();
        public IEnumerable<RoleDto> RoleList { get; set; } = new List<RoleDto>();
    }
}
