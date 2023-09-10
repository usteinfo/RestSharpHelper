using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace USTEInfo.RestSharpHelper
{
    /// <summary>
    /// 
    /// Api调用 辅助类
    /// </summary>
    public sealed class RestSharpHelper
    {
        /// <summary>
        /// 处理拦截事件
        /// </summary>
        private readonly IRestSharpHandle _restSharpHandle;


        /// <summary>
        /// 基础访问地址，以"/"结尾
        /// </summary>
        public string BaseUrl { get; private set; }

        /// <summary>
        /// 超时时间，单位秒
        /// </summary>
        public int Timeout { get; private set; }

        /// <summary>
        /// 构造函数，超时时间，默认：30s
        /// </summary>
        /// <param name="baseUrl">基础地址</param>
        /// <param name="eventHandle">通知事件处理器</param>
        public RestSharpHelper(string baseUrl, IRestSharpHandle eventHandle = null) : this(baseUrl, 30, eventHandle)
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="baseUrl">基础地址</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="eventHandle">通知事件处理器</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RestSharpHelper(string baseUrl, int timeout, IRestSharpHandle eventHandle = null)
        {
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentNullException(nameof(baseUrl));
            }
            Timeout = timeout <= 0 ? 30 : timeout;
            BaseUrl = baseUrl;
            if (!BaseUrl.EndsWith("/"))
            {
                BaseUrl += "/";
            }
            if (eventHandle == null)
            {
                _restSharpHandle = new EmptyRestSharpHandle();
            }
            else
            {
                _restSharpHandle = eventHandle;
            }
        }
        /// <summary>
        /// Get方式调用服务
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="resource">资源相对地址</param>
        /// <param name="headers">注入的header数据</param>
        /// <returns>返回类型为T的数据</returns>
        public async Task<T> GetAsync<T>(string resource, IDictionary<string, string> headers = null)
        {
            return await ExecuteAsync<T>(resource, Method.Get, null, headers);
        }
        /// <summary>
        /// Post方式调用服务
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="resource">资源相对地址</param>
        /// <param name="headers">注入的header数据</param>
        /// <returns>返回类型为T的数据</returns>
        public async Task<T> PostAsync<T>(string resource, IDictionary<string, string> headers = null)
        {
            return await PostAsync<T>(resource, default(object), headers);
        }
        /// <summary>
        /// Post方式调用服务
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="resource">资源相对地址</param>
        /// <param name="body">form对象</param>
        /// <param name="headers">注入的header数据</param>
        /// <returns>返回类型为T的数据</returns>
        public  async Task<T> PostAsync<T>(string resource, object body, IDictionary<string, string> headers = null)
        {
            return await ExecuteAsync<T>(resource, Method.Post, body, headers);
        }
        /// <summary>
        /// Delete方式调用服务
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="resource">资源相对地址</param>
        /// <param name="headers">注入的header数据</param>
        /// <returns>返回类型为T的数据</returns>
        public  async Task<T> DeleteAsync<T>(string resource, IDictionary<string, string> headers = null)
        {
            return await DeleteAsync<T>(resource, default(object), headers);
        }
        /// <summary>
        /// Delete方式调用服务
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="resource">资源相对地址</param>
        /// <param name="body">form对象</param>
        /// <param name="headers">注入的header数据</param>
        /// <returns>返回类型为T的数据</returns>
        public  async Task<T> DeleteAsync<T>(string resource, object body, IDictionary<string, string> headers = null)
        {
            return await ExecuteAsync<T>(resource, Method.Delete, body, headers);
        }
        /// <summary>
        /// 以指定的Method方式调用服务
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="resource">资源相对地址</param>
        /// <param name="method">请求方式</param>
        /// <param name="body">form对象</param>
        /// <param name="headers">注入的header数据</param>
        /// <returns>返回类型为T的数据</returns>
        public async Task<T> ExecuteAsync<T>(string resource, Method method, object body, IDictionary<string, string> headers)
        {
            return await Execute(resource, method, body, headers, async (client, request) =>
            {
                var response = await client.ExecuteAsync<T>(request);
                _restSharpHandle.Executed(client, request, response);
                var data = CheckResponse(response);
                if (data.Sucess)
                {
                    return data.Data;
                }
                Exception ex = data.ErrorException ?? new ApiException(data.ErrorMessage);
                WriteExceptionLog(response, ex);
                throw ex;
            });
        }
        /// <summary>
        /// 异常通知
        /// </summary>
        /// <param name="response">Api响应对象</param>
        /// <param name="ex">异常对象</param>
        private void WriteExceptionLog(RestResponse response, Exception ex)
        {
            if(_restSharpHandle != null)
            {
                _restSharpHandle.ExceptionMessage(response, ex);
            }
        }
        /// <summary>
        /// 以指定的Method方式调用服务
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="resource">资源相对地址</param>
        /// <param name="method">请求方式</param>
        /// <param name="body">form对象</param>
        /// <param name="headers">注入的header数据</param>
        /// <param name="action">自定义数据处理Action</param>
        /// <returns>返回类型为T的数据</returns>
        public Task<T> ExecuteAsync<T>(string resource, Method method, object body
            , IDictionary<string, string> headers, Func<CallBackData<T>,Task<T>> action)
        {
          return  Execute(resource, method, body, headers, async (client, request) =>
            {
                var response = await client.ExecuteAsync<T>(request);
                
                _restSharpHandle.Executed(client, request, response);
                var data = CheckResponse(response);
                if (!data.Sucess)
                {
                    Exception ex = data.ErrorException ?? new ApiException(data.ErrorMessage);
                    WriteExceptionLog(response, ex);
                    data = CallBackData<T>.Error<T>(ex);
                }

                return await action(data);
            });
        }
        /// <summary>
        /// 检查响应对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        private CallBackData<T> CheckResponse<T>(RestResponse<T> response)
        {
            if (response.Data != null)
            {
                return CallBackData<T>.Successed(response.Data);
            }
            int code = (int)response.StatusCode;

            //if (code < 300)
            //{
            //    return CallBackData<T>.Successed<T>(Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content));
            //}

            return CallBackData<T>.Error<T>(new ApiException("调用Api出现错误，HttpCode:" + code, response.ErrorException));
        }
        /// <summary>
        /// 执行远程调用
        /// </summary>
        /// <param name="resource">资源地址</param>
        /// <param name="method">调用方法</param>
        /// <param name="body">表单数据</param>
        /// <param name="headers">注入表头数据</param>
        /// <param name="action">响应数据处理</param>
        private async Task<T> Execute<T>(string resource, Method method, object body, IDictionary<string, string> headers, Func<IRestClient, RestRequest,Task<T>> action)
        {
            _restSharpHandle.RestClientCreating(BaseUrl);
            var client = CreateRestClient(BaseUrl);
            _restSharpHandle.RestClientCreated(client);
            _restSharpHandle.RequestCreating(client);
            var request = CreateRestRequest(method, resource);

            if (headers != null)
            {
                foreach (var param in headers)
                {
                    request.AddHeader(param.Key, param.Value);
                }
            }

            switch (method)
            {
                case Method.Get:
                    break;
                default:
                    if (body != null)
                    {
                        request.AddJsonBody(body);
                    }
                    break;
            }
            _restSharpHandle.RequestCreated(client, request);
            _restSharpHandle.Executing(client, request);
            return await action(client, request);
        }

        /// <summary>
        /// 创建远程调用对象
        /// </summary>
        /// <param name="baseUrl">基础地址</param>
        /// <returns></returns>
        private IRestClient CreateRestClient(string baseUrl)
        {
            var options = new RestClientOptions(baseUrl)
            {
                MaxTimeout = (Timeout <= 0 ? 100000 : Timeout * 1000),
            };
            var client = new RestClient(options);
            return client;
        }
        /// <summary>
        /// 创建远程请求对象
        /// </summary>
        /// <param name="method">请求方式</param>
        /// <param name="resource">资源地址</param>
        /// <returns></returns>
        private RestRequest CreateRestRequest(Method method, string resource)
        {
            return new RestRequest(resource, method);
        }
    }
}
