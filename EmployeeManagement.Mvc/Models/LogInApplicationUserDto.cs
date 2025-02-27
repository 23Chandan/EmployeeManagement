using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Mvc.Models
{
    public class LogInApplicationUserDto
    {
        [Required(ErrorMessage ="User Name is reuqired")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is reuqired")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Role is reuqired")]
        public string Role { get; set; }

        public bool RememberMe { get; set; }
    }


    public class SignInApplicationUserDto
    {
        [Required(ErrorMessage = "User Name is reuqired")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is reuqired")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Role is reuqired")]
        public string Role { get; set; }
    }
}
