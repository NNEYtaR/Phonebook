using System.ComponentModel.DataAnnotations;

namespace Phonebook.Models
{
    public class Account
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [Required]
        public string? Username { get; set; }

        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$")]
        [Required]
        public string? Email { get; set; }
        
        [Required]
        public string? Password { get; set; }
    }
}