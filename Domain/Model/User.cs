using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public Gender Gender { get; set; }
        [Required]
        public string Account { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
