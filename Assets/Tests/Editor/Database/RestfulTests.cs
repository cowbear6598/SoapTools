using System.Threading.Tasks;
using DatabaseTools.Runtime;
using DatabaseTools.Tests.Editor.Types;
using NUnit.Framework;
using SoapTools.Database;
using UnityEngine;

namespace DatabaseTools.Tests.Editor
{
    [TestFixture]
    public class RestfulTests
    {
        [Test]
        public async Task _01_1_Should_Request_Simple_Get_Success_From_SO()
        {
            var requestSO = Resources.Load<RestfulRequestSO>("Restful/01_Test");
            var responseData = await new RestfulBuilder()
                                     .SetRequestSO(requestSO)
                                     .StartRequest<PostResponseData>();

            var expectedData = new PostResponseData {
                userId    = 1,
                id        = 1,
                title     = "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
                completed = false,
            };

            ResponseDataShouldBeEqual(expectedData, responseData);
        }

        [Test]
        public async Task _01_2_Should_Request_Simple_Get_Success_From_Builder()
        {
            var responseData = await new RestfulBuilder()
                                     .SetUrl("https://jsonplaceholder.typicode.com/posts/1")
                                     .SetMethod(Method.GET)
                                     .SetTimeout(10)
                                     .StartRequest<PostResponseData>();


            var expectedData = new PostResponseData {
                userId    = 1,
                id        = 1,
                title     = "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
                completed = false,
            };

            ResponseDataShouldBeEqual(expectedData, responseData);
        }

        [Test]
        public async Task _02_1_Should_Request_Simple_Post_Success_From_SO()
        {
            var requestSO   = Resources.Load<RestfulRequestSO>("Restful/02_Test");
            var requestData = new PostRequestData("foo", "bar", 1);

            var responseData = await new RestfulBuilder()
                                     .SetRequestSO(requestSO)
                                     .SetBody(requestData)
                                     .StartRequest<PostResponseData>();

            var expectedData = new PostResponseData(101, 1, "foo", false);

            ResponseDataShouldBeEqual(expectedData, responseData);
        }

        [Test]
        public async Task _02_2_Should_Request_Simple_Post_Success_From_Builder()
        {
            var requestData = new PostRequestData("foo", "bar", 1);

            var responseData = await new RestfulBuilder()
                                     .SetUrl("https://jsonplaceholder.typicode.com/posts")
                                     .AddHeader("Content-Type", "application/json")
                                     .SetMethod(Method.POST)
                                     .SetTimeout(10)
                                     .SetBody(requestData)
                                     .StartRequest<PostResponseData>();

            var expectedData = new PostResponseData(101, 1, "foo", false);

            ResponseDataShouldBeEqual(expectedData, responseData);
        }

        private void ResponseDataShouldBeEqual(PostResponseData expectedData, PostResponseData response)
        {
            Assert.AreEqual(JsonUtility.ToJson(expectedData), JsonUtility.ToJson(response));
        }
    }
}