using System;
using System.IO;
using System.Linq;
using Castle.DynamicProxy;

namespace Infrastructure.aop
{
    public class BaseAop : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            //记录被拦截方法信息的日志信息
            var dataIntercept = $"{DateTime.Now.ToString("HH:mm:ss")} " +
                $"当前执行方法：{ invocation.Method.Name} " +
                $"参数： {string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())} \r\n";

            //在被拦截的方法执行完毕后 继续执行当前方法
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                dataIntercept += $"Exception：''{e.InnerException}''\r\n";
            }
            dataIntercept += ($"Return Value：{invocation.ReturnValue}\r\n");


            #region 输出到当前项目日志
            var path = Directory.GetCurrentDirectory() + @"\Log";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = path + $@"\InterceptLog-{DateTime.Now.ToString("yyyy-MM-dd")}.log";
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine(dataIntercept);
                sw.Flush();
                sw.Close();
            }
            #endregion
        }
    }
}
