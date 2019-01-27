using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Infrastructure.Cache;

namespace Infrastructure.aop
{
    public class CacheAop : IInterceptor
    {
        private ICache _cache;
        public CacheAop(ICache cache)
        {
            _cache = cache;
        }

        public void Intercept(IInvocation invocation)
        {
            //获取自定义缓存键
            var cacheKey = CustomCacheKey(invocation);
            //根据key获取相应的缓存值
            var cacheValue = _cache.Get(cacheKey);
            if (cacheValue != null)
            {
                //将当前获取到的缓存值，赋值给当前执行方法
                invocation.ReturnValue = cacheValue;
                return;
            }
            //去执行当前的方法
            invocation.Proceed();
            //存入缓存
            if (!string.IsNullOrWhiteSpace(cacheKey))
            {
                _cache.Set(cacheKey, invocation.ReturnValue);
            }
        }

        //自定义缓存键
        private string CustomCacheKey(IInvocation invocation)
        {
            var typeName = invocation.TargetType.Name;
            var methodName = invocation.Method.Name;
            var methodArguments = invocation.Arguments.Select(GetArgumentValue).Take(3).ToList();//获取参数列表，最多三个

            string key = $"{typeName}:{methodName}:";
            foreach (var param in methodArguments)
            {
                key += $"{param}:";
            }

            return key.TrimEnd(':');
        }
        //object 转 string
        private string GetArgumentValue(object arg)
        {
            if (arg is int || arg is long || arg is string)
                return arg.ToString();

            if (arg is DateTime)
                return ((DateTime)arg).ToString("yyyyMMddHHmmss");

            return "";
        }
    }
}
