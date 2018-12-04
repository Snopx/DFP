using Domain.Enum;

namespace Domain.Model
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
    }
}
