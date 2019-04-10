using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Application.AutoMapper;
using Application.ServiceBaseInterface;
using Domain.Enum;

namespace Application.AppService.UserApp.dto
{
    public class UserDto : IEntityDto
    {
        public int ID { get; set; }

        [MinLength(5), MaxLength(10)]
        public string Account { get; set; }

        [MaxLength(30), Required]
        public string Name { get; set; }

        public Gender Gender { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }
        /// <summary>
        /// 个性签名
        /// </summary>
        [MaxLength(80)]
        public string Personality { get; set; }
    }
}
