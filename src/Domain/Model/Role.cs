using Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model
{
    public class Role:BaseEntity
    {
        [MaxLength(20)]
        public string RoleName { get; set; }
    }
}
