using System;
namespace Helper
{
    /// <summary>
    /// 用于手动获取Di
    /// 
    /// </summary>
    public static class ServiceLocator
    {
        /// <summary>
        ///  在config 添加 ServiceLocator.Instance = app.ApplicationServices;
        /// </summary>
        /// <value></value>
        public static IServiceProvider Instance { get; set; }

        /// <summary>
        /// 获取指定的接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetClass<T>(){
            return (T)ServiceLocator.Instance.GetService(typeof(T));
        }
    }

}