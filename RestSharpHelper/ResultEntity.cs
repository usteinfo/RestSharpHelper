namespace USTEInfo.RestSharpHelper
{
    /// <summary>
    /// Api传输定义
    /// </summary>
    public class ResultEntity
    {
        /// <summary>
        /// 用于序列化使用
        /// </summary>
        protected ResultEntity() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        protected ResultEntity(bool sucess, string errorMessage)
        {
            Sucess = sucess;
            ErrorMessage = errorMessage;
        }
        /// <summary>
        /// 访问是否成功，返回True表示成功，false表示出错，错误信息详细查看ErrorMessage
        /// </summary>
        public bool Sucess { get; set; }
        /// <summary>
        /// 出错时，返回错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns>返回创建对象</returns>
        public static ResultEntity Sucessed()
        {
            return new ResultEntity(true, "");
        }
        /// <summary>
        /// 创建出错对象
        /// </summary>
        /// <param name="message">出错信息</param>
        /// <returns>返回创建的错误对象</returns>
        public static ResultEntity Error(string message)
        {
            return new ResultEntity(false, message);
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="data">返回数据对象</param>
        /// <returns>返回创建对象</returns>
        public static ResultEntity<T> Sucessed<T>(T data)
        {
            return new ResultEntity<T>(true, data, "");
        }
        /// <summary>
        /// 创建出错对象
        /// </summary>
        /// <param name="message">出错信息</param>
        /// <returns>返回创建的错误对象</returns>
        public static ResultEntity<T> Error<T>(string message)
        {
            return new ResultEntity<T>(false, default(T), message);
        }
    }

    /// <summary>
    /// 结果集对象
    /// </summary>
    /// <typeparam name="T">泛型类型，数据</typeparam>
    public class ResultEntity<T> : ResultEntity
    {

        /// <summary>
        /// 用于序列化使用
        /// </summary>
        protected ResultEntity() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        protected internal ResultEntity(bool sucess, T data, string errorMessage) : base(sucess, errorMessage)
        {
            Data = data;
        }
        /// <summary>
        /// 数据对象
        /// </summary>
        public T Data { get; set; }
    }
}
