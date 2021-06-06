using System.ComponentModel.DataAnnotations;

namespace TaskTrackingSystem.ViewModels
{
    /// <summary>
    /// A model, which is used during registration
    /// </summary>
    public class RegisterView
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
