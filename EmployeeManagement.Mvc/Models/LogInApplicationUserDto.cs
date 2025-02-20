namespace EmployeeManagement.Mvc.Models
{
    public class LogInApplicationUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
    public class SignInApplicationUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
