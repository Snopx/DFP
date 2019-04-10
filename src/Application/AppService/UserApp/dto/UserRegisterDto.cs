using Application.ServiceBaseInterface;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AppService.UserApp.dto
{
    public class UserRegisterDto : IEntityDto
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
    }
}
