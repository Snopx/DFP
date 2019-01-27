using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Application.AutoMapper;
using Domain.Enum;

namespace Application.UserApp
{
    public class UserDto: EntityDto<long>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Gender Gender { get; set; }
    }
}
