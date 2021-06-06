using System.ComponentModel.DataAnnotations;

namespace TaskTrackingSystem.ViewModels
{
    /// <summary>
    /// A model, which are used during log in
    /// </summary>
    public class LoginView
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
