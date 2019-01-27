using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AutoMapper
{
    public interface IEntityDto<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
