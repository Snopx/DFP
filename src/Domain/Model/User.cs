using Domain.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace Domain.Model
{
    public class User : BaseEntity<string>, IIdentity
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [MaxLength(20)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [NotMapped]
        public virtual IEnumerable<Role> Roles { get; set; }
        [NotMapped]
        public string AuthenticationType { get; set; }
        [NotMapped]
        public bool IsAuthenticated { get; set; }
    }
}
