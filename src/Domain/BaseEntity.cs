using Domain.Interface;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    /// 基础实体类
    /// </summary>
    /// <typeparam name="Primary">主键类型</typeparam>
    public abstract class BaseEntity<TPrimary> : IEntity
    {
        [Key]
        [MaxLength(36)]
        public virtual TPrimary ID { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<long>, IEntity
    {
    }
}
