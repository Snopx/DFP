using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.QueryParameterFolder
{
    public class UserParameter: PaginationQueryParameters
    {
        public string Name { get; set; }
    }
}
