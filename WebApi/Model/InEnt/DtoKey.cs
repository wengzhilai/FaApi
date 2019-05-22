namespace WebApi.Model.InEnt
{
    /// <summary>
    /// 操作对象
    /// </summary>
    public class DtoKey
    {
        /// <summary>
        /// 键
        /// </summary>
        /// <value></value>
        public string Key { get; set; }
    }

    /// <summary>
    /// 操作对象
    /// </summary>
    public class DtoKey<T>
    {
        /// <summary>
        /// 键
        /// </summary>
        /// <value></value>
        public T Key { get; set; }
    }
}