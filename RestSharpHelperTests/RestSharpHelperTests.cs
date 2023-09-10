using Microsoft.VisualStudio.TestTools.UnitTesting;
using USTEInfo.RestSharpHelper;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace USTEInfo.RestSharpHelper.Tests
{
    [TestClass()]
    public class RestSharpHelperTests
    {

        private RestSharpHelper GetRestSharpHelper()
        {
            return new RestSharpHelper("https://jsonplaceholder.typicode.com");
        }
        [TestMethod()]
        public void GetTest()
        {
            var restSharpHelper = GetRestSharpHelper();
            var result = restSharpHelper.GetAsync<Todo>("/todos/1");
            result.ContinueWith(p =>
            {
                Assert.AreEqual(1, p.Result.id);
            });
            result.Wait();
        }
        [TestMethod()]
        public void PostTest()
        {
            var restSharpHelper = GetRestSharpHelper();
            var result = restSharpHelper.GetAsync<List<PostData>>("posts");
            result.ContinueWith(p =>
            {
                Assert.AreEqual(100, p.Result.Count);
            });
            result.Wait();
        }
        [TestMethod()]
        public void CallBackDataTest()
        {
            var restSharpHelper = GetRestSharpHelper();
            var result = restSharpHelper.ExecuteAsync<List<PostData>>("posts",Method.Get,null,null);
            result.ContinueWith(p =>
            {
                Assert.AreEqual(100, p.Result.Count);
            });
            result.Wait();
        }
    }
}