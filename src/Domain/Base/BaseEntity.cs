using Domain.Interface;
using System.ComponentModel.DataAnnotations;

namespace Domain.Base
{
    /// <summary>
    /// 基础实体类
    /// </summary>
    /// <typeparam name="Primary">主键类型</typeparam>
    public abstract class BaseEntity<TPrimary> : IEntity
    {
        [Key]
        public virtual TPrimary ID { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>, IEntity
    {
    }
}
