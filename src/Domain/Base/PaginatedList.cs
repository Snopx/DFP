using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base
{
    public class PaginatedList<T> : List<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        private int _totalItemsCount { get; set; }
        public int TotalItemsCount
        {
            get => _totalItemsCount;
            set => _totalItemsCount = value >= 0 ? value : 0;
        }

        public int PageCount => TotalItemsCount / PageSize + (TotalItemsCount % PageSize > 0 ? 1 : 0);

        public bool HasPrevious => PageIndex > 0;
        public bool HasNext => PageIndex < PageCount - 1;

        public PaginatedList(int pageIndex,int pageSize,int totalItemsCount,IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = PageSize;
            TotalItemsCount = totalItemsCount;
            AddRange(data);
        }
    }
}
