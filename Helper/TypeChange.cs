using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Helper
{
    public static class TypeChange
    {
        #region 类型转换
        /// <summary>
        /// 将JSON 字符转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T ToJsonObject<T>(this string str)
        {
            try
            {
                T obj = Activator.CreateInstance<T>();
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(str)))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                    return (T)serializer.ReadObject(ms);
                }
            }
            catch (Exception)
            {
                throw new Exception("数据验证失败");
            }
        }

        public static T JsonToObject<T>(this string str)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(str)))
            {
                JsonSerializerSettings jsonConfig = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };
                return JsonConvert.DeserializeObject<T>(str, jsonConfig);
            }
        }


        /// <summary>
        /// 将DataTable转变为实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static T GetEntity<T>(DataTable table) where T : new()
        {
            T entity = new T();
            foreach (DataRow row in table.Rows)
            {
                foreach (var item in entity.GetType().GetProperties())
                {
                    if (row.Table.Columns.Contains(item.Name))
                    {
                        if (DBNull.Value != row[item.Name])
                        {
                            var piType = item.PropertyType;
                            if (!piType.IsGenericType)
                            {
                                item.SetValue(entity, Convert.ChangeType(row[item.Name], piType), null);
                            }
                            else
                            {
                                Type genericTypeDefinition = piType.GetGenericTypeDefinition();
                                item.SetValue(entity, Convert.ChangeType(row[item.Name], Nullable.GetUnderlyingType(piType)));
                            }
                        }
                    }
                }
            }
            return entity;
        }
        /// <summary>
        /// 将DataTable转换为实体对象集合List(如果DataTable列中缺少实体的属性，则实体集合中的属性为NULL，不会报错)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> GetEntities<T>(DataTable table) where T : new()
        {
            // 定义集合  
            List<T> ts = new List<T>();
            if (table == null) return ts;
            // 获得此模型的类型  
            Type type = typeof(T);
            //定义一个临时变量  
            string tempName = string.Empty;
            //遍历DataTable中所有的数据行   
            foreach (DataRow dr in table.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性  
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性  
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;//将属性名称赋值给临时变量    
                    //检查DataTable是否包含此列（列名==对象的属性名）      
                    if (table.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter    
                        if (!pi.CanWrite) continue;//该属性不可写，直接跳出    
                        //取值    
                        object value = dr[tempName];
                        //如果非空，则赋给对象的属性    
                        if (value != DBNull.Value)
                        {
                            var piType = pi.PropertyType;
                            if (!piType.IsGenericType)
                            {
                                pi.SetValue(t, Convert.ChangeType(value, piType), null);
                            }
                            else
                            {
                                Type genericTypeDefinition = piType.GetGenericTypeDefinition();
                                pi.SetValue(t, Convert.ChangeType(value, Nullable.GetUnderlyingType(piType)));
                            }
                        }
                    }
                }
                //对象添加到泛型集合中  
                ts.Add(t);
            }
            return ts;
        }

        /// <summary>
        /// 将Dictionary转成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static T DicToObject<T>(Dictionary<string, object> dic) where T : new()
        {
            var md = new T();
            foreach (var d in dic)
            {
                var filed = d.Key;
                try
                {
                    var value = d.Value;
                    if (value != null && value != DBNull.Value && !string.IsNullOrEmpty(value.ToString()))
                    {
                        md.GetType().GetProperty(filed).SetValue(md, value);
                    }
                }
                catch
                {
                }
            }
            return md;
        }


        public static string ObjectToStr<T>(T entity)
        {
            if (entity == null) return null;
            try
            {
                if (entity == null) return null;
                if ((entity.GetType() == typeof(String) || entity.GetType() == typeof(string)))
                {
                    return entity.ToString();
                }
                string DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
                IsoDateTimeConverter dt = new IsoDateTimeConverter();
                dt.DateTimeFormat = DateTimeFormat;
                var jSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                jSetting.Converters.Add(dt);
                return JsonConvert.SerializeObject(entity, jSetting);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 将数据表转到CSV数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="exportHeader"></param>
        /// <returns></returns>
        public static string DataTableToString(DataTable data, bool exportHeader = true)
        {
            var buffer = new StringBuilder();
            if (exportHeader)
            {
                for (var i = 0; i < data.Columns.Count; i++)
                {
                    buffer.AppendFormat("\"{0}\"", data.Columns[i].ColumnName);
                    if (i < data.Columns.Count - 1)
                        buffer.Append(",");
                }
                buffer.AppendLine();
            }
            for (var i = 0; i < data.Rows.Count; i++)
            {
                for (var j = 0; j < data.Columns.Count; j++)
                {
                    buffer.AppendFormat("\"{0}\"", (data.Rows[i][j] != null && data.Rows[i][j] != DBNull.Value) ? data.Rows[i][j].ToString() : "");
                    if (j < data.Columns.Count - 1)
                        buffer.Append(",");
                }
                buffer.AppendLine();
            }
            return buffer.ToString();
        }

        /// <summary>
        /// 动态对象转Kv数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, object>> DynamicToKeyValueList(object obj)
        {
            List<KeyValuePair<string, object>> reList = new List<KeyValuePair<string, object>>();
            Type type = obj.GetType();
            PropertyInfo[] PropertyList = type.GetProperties();//得到该类的所有公共属性
            reList = PropertyList.Select(x =>
             new KeyValuePair<string, object>(x.Name, x.GetValue(obj))
            ).ToList();
            return reList;
        }

        /// <summary>
        /// 动态对象转Kv数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<string> DynamicToKeyList(object obj)
        {
            List<string> reList = new List<string>();
            reList = obj.GetType().GetProperties().Select(x =>x.Name).ToList();
            return reList;
        }
        #endregion
    }
}
