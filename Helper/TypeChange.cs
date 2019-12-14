using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
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
using System.Xml;

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

        public static JObject JsonToObject(this string str)
        {
            return (JObject)JsonConvert.DeserializeObject(str);
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ObjectToDic<T>(T inEnt)
        {
            var reObj = new Dictionary<string, string>();

            Type type = typeof(T);
            PropertyInfo[] PropertyList = type.GetProperties();//得到该类的所有公共属性
            foreach (PropertyInfo proInfo in PropertyList)
            {
                var v = proInfo.GetValue(inEnt, null);
                reObj.Add(proInfo.Name, v == null ? "" : v.ToString());
            }
            return reObj;
        }


        public static string ObjectToStr<T>(T entity)
        {
            try
            {
                if (entity is String )
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


        /// <summary>
        /// Url地址转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public static T UrlToEntities<T>(string request) where T : new()
        {

            T entity = Activator.CreateInstance<T>();
            if (string.IsNullOrEmpty(request)) return entity;

            request = request.Trim('?');
            PropertyInfo[] attrs = entity.GetType().GetProperties();
            foreach (PropertyInfo p in attrs)
            {
                foreach (string key in request.Split('&'))
                {
                    var _key = key.Split('=')[0];
                    var _value = key.Split('=')[1];
                    if (string.Compare(p.Name, _key, true) == 0)
                    {
                        try
                        {
                            Type type = p.PropertyType;
                            //判断type类型是否为泛型，因为nullable是泛型类
                            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))//判断convertsionType是否为nullable泛型类  
                            {
                                //如果type为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换  
                                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(type);
                                //将type转换为nullable对的基础基元类型  
                                type = nullableConverter.UnderlyingType;
                            }

                            p.SetValue(entity, Convert.ChangeType(_value, type), null);
                        }
                        catch (Exception e)
                        {
                            continue;
                        }
                    }
                }
            }
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlStr"></param>
        /// <returns></returns>
        public static Dictionary<string,string> XmlToDict(string xmlStr)
        {
            var reObj = new Dictionary<string, string>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlStr);
            XmlNode root = xmlDoc.SelectSingleNode("xml");
            XmlNodeList xnl = root.ChildNodes;

            foreach (XmlNode xnf in xnl)
            {
                reObj.Add(xnf.Name, xnf.InnerText);
            }
            return reObj;
        }

        public static Int64 DateToInt64(DateTime dateTime)
        {
            return dateTime.Ticks - Convert.ToDateTime("1970-1-1").Ticks;
        }

        public static DateTime Int64ToDate(Int64 inInt)
        {
            return Convert.ToDateTime("1970-1-1").AddMilliseconds(inInt);
        }
    }
}
