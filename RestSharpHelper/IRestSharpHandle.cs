using RestSharp;
using System;

namespace USTEInfo.RestSharpHelper
{
    /// <summary>
    /// RestSharpHelper类 事件接口
    /// </summary>
    public interface IRestSharpHandle
    {
        /// <summary>
        /// RestClient开始创建事件
        /// </summary>
        /// <param name="baseUrl">基础地址</param>
        void RestClientCreating(string baseUrl);
        /// <summary>
        /// RestClient创建完成事件
        /// </summary>
        /// <param name="client">RestClinet对象</param>
        void RestClientCreated(IRestClient client);
        /// <summary>
        /// Request请求创建开始
        /// </summary>
        /// <param name="client">RestClinet对象</param>
        void RequestCreating(IRestClient client);
        /// <summary>
        /// Request请求创建完成
        /// </summary>
        /// <param name="client">RestClinet对象</param>
        /// <param name="request">RestRequest 请求对象</param>
        void RequestCreated(IRestClient client, RestRequest request);
        /// <summary>
        /// 远程请求开始执行
        /// </summary>
        /// <param name="client">RestClinet对象</param>
        /// <param name="request">RestRequest 请求对象</param>
        void Executing(IRestClient client, RestRequest request);
        /// <summary>
        /// 远程请求执行完成
        /// </summary>
        /// <param name="client">RestClinet对象</param>
        /// <param name="request">RestRequest 请求对象</param>
        /// <param name="response">RestResponse 请求对象</param>
        void Executed(IRestClient client, RestRequest request,RestResponse response);

        /// <summary>
        /// 异常通知消息
        /// </summary>
        /// <param name="response">服务响应内容</param>
        /// <param name="ex">异常信息</param>
        void ExceptionMessage(RestResponse response, Exception ex);
    }
}
