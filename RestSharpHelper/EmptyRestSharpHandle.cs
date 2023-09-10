using RestSharp;
using System;

namespace USTEInfo.RestSharpHelper
{
    public class EmptyRestSharpHandle : IRestSharpHandle
    {
        public void ExceptionMessage(RestResponse response, Exception ex)
        {
            
        }

        public void Executed(IRestClient client, RestRequest request, RestResponse response)
        {
            
        }

        public void Executing(IRestClient client, RestRequest request)
        {
            
        }

        public void RequestCreated(IRestClient client, RestRequest request)
        {
            
        }

        public void RequestCreating(IRestClient client)
        {
            
        }

        public void RestClientCreated(IRestClient client)
        {
            
        }

        public void RestClientCreating(string baseUrl)
        {
            
        }

    }
}
