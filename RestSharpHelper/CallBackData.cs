using System;

namespace USTEInfo.RestSharpHelper
{
    public class CallBackData<T> : ResultEntity<T>
    {
        protected internal CallBackData(bool sucess, T data, Exception exception) : base(sucess, data, exception == null ? "" : exception.Message)
        {
            ErrorException = exception;
        }

        public static CallBackData<TData> Successed<TData>(TData data)
        {
            return new CallBackData<TData>(true, data, null);
        }
        public static CallBackData<TData> Error<TData>(Exception exception)
        {
            return new CallBackData<TData>(false, default(TData), exception);
        }

        public Exception ErrorException { get; private set; }
    }
} 
