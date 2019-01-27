using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Cache
{
    public class MemoryCache : ICache
    {
        //引用Microsoft.Extensions.Caching.Memory;这个和.net 还是不一样，没有了Httpruntime了
        private IMemoryCache _cache;
        //还是通过构造函数的方法，获取
        public MemoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public void Set(string key, object value)
        {
            _cache.Set(key, value, TimeSpan.FromSeconds(7200));
        }
    }
}
