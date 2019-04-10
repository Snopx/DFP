using System.ComponentModel.DataAnnotations;
using Application.ServiceBaseInterface;
using Domain.Enum;

namespace Application.UserApp
{
    public class UserDto : IEntityDto
    {
        [Required]
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
