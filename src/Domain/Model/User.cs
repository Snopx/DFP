using Domain.Base;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace Domain.Model
{
    [Table("User")]
    public class UserModel : BaseEntity, IIdentity
    {
        [MinLength(5),MaxLength(10)]
        public string Account { get; set; }

        [MaxLength(30), Required]
        public string Name { get; set; }

        public Gender Gender { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }
        [MaxLength(128), Required]
        public string Password { get; set; }

        /// <summary>
        /// 个性签名
        /// </summary>
        [MaxLength(80)]
        public string Personality { get; set; }
        [NotMapped]
        public virtual IEnumerable<Role> Roles { get; set; }
        [NotMapped]
        public string AuthenticationType { get; set; }
        [NotMapped]
        public bool IsAuthenticated { get; set; }
    }
}
