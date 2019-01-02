using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace WebApi.Unit
{
    /// <summary>
    /// 获取所有Dll
    /// </summary>
    [ApiExplorerSettings(IgnoreApi=true)]
    public class RuntimeHelper
    {
        /// <summary>
        /// 获取项目程序集，排除所有的系统程序集(Microsoft.***、System.***等)、Nuget下载包
        /// </summary>
        /// <returns></returns>
        public static IList<Assembly> GetAllAssemblies()
        {
            List<Assembly> list = new List<Assembly>();
            var deps = DependencyContext.Default;
            var pro=deps.CompileLibraries.Where(x=>x.Type=="project").ToList();
            //排除所有的系统程序集、Nuget下载包
            var libs = deps.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package");
            foreach (var lib in libs)
            {
                try
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                    list.Add(assembly);
                }
                catch (Exception ex)
                {
                    Helper.LogHelper.WriteErrorLog<RuntimeHelper>(ex.ToString());
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static Assembly GetAssembly(string assemblyName)
        {
            return GetAllAssemblies().FirstOrDefault(f => f.FullName.Contains(assemblyName));
        }

        /// <summary>
        /// 获取所有类型
        /// </summary>
        /// <returns></returns>
        public static IList<Type> GetAllTypes()
        {
            List<Type> list = new List<Type>();
            foreach (var assembly in GetAllAssemblies())
            {
                var typeinfos = assembly.DefinedTypes;
                foreach (var typeinfo in typeinfos)
                {
                    list.Add(typeinfo.AsType());
                }
            }
            return list;
        }

        /// <summary>
        /// 根据AssemblyName获取所有的类
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static IList<Type> GetTypesByAssembly(string assemblyName)
        {
            List<Type> list = new List<Type>();
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
            var typeinfos = assembly.DefinedTypes;
            foreach (var typeinfo in typeinfos)
            {
                list.Add(typeinfo.AsType());
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="baseInterfaceType"></param>
        /// <returns></returns>
        public static Type GetImplementType(string typeName, Type baseInterfaceType)
        {
            return GetAllTypes().FirstOrDefault(t =>
            {
                if (t.Name == typeName && t.GetTypeInfo().GetInterfaces().Any(b => b.Name == baseInterfaceType.Name))
                {
                    var typeinfo = t.GetTypeInfo();
                    return typeinfo.IsClass && !typeinfo.IsAbstract && !typeinfo.IsGenericType;
                }
                return false;
            });
        }
    }
}