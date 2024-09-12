using System.ComponentModel.DataAnnotations;

namespace BestBrightnesss.ViewLogic
{
    public class RegisterView
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(10, ErrorMessage = "Title cannot be longer than 10 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact Number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact Number must be 10 digits")]
        public string ContactNumber { get; set; } = string.Empty;
    }

}
