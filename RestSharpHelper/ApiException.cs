using System;

namespace USTEInfo.RestSharpHelper
{
    /// <summary>
    /// Api调用异常错误
    /// </summary>
    [Serializable]
    public class ApiException : Exception
    {
        public ApiException()
        {
        }

        public ApiException(string message) : base(message)
        {
        }

        public ApiException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
