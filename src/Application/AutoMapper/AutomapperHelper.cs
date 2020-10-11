using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;

namespace Application.AutoMapper
{
    /// <summary>
    /// 旧版本使用的 profile 注册方式
    /// </summary>
    public class AutomapperHelper
    {
        [Obsolete("10.x之后的版本 不需要使用这种方式注册profile DI中检测某一程序集中继承“Profile”类的所映射项")]
        public void RegisterMappings()
        {
            //获取所有IProfile实现类
            var assemblies =
            Assembly
               .GetEntryAssembly()//获取默认程序集
               .GetReferencedAssemblies()//获取所有引用程序集
               .Select(Assembly.Load);
            //.SelectMany(y => y.DefinedTypes)
            //.Where(type => typeof(IProfile).GetTypeInfo().IsAssignableFrom(type.AsType()));

            var config = new MapperConfiguration(cfg => cfg.AddMaps(assemblies)); //这里扫描所有程序集 理论上只有 application 层

            var mapper = new Mapper(config);

            //foreach (var typeInfo in allType)
            //{
            //    var type = typeInfo.AsType();
            //    if (type.Equals(typeof(IProfile)))
            //    {
            //        //注册映射
            //        new Mapper()
            //    }
            //}
        }
    }
}
