using System.ComponentModel.DataAnnotations;

namespace VoziMe.Models
{
    public class MyAccountViewModel
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Add additional properties for other user details such as address, phone number, etc.
    }

}
