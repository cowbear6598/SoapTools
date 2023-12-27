using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DatabaseTools.Tests.Editor.Types;
using NUnit.Framework;
using SoapTools.Database;
using UnityEngine;

namespace DatabaseTools.Tests.Editor
{
    [TestFixture]
    public class GraphQLTests
    {
        [Test]
        public async Task _01_1_Should_Query_Success_From_SO()
        {
            var requestSO = Resources.Load<GraphQLRequestSO>("GraphQL/01_Test");
            var req = new GraphQLBuilder()
                      .SetRequestSO(requestSO)
                      .Build();

            var response = await req.SendWebRequest();

            var data = JsonUtility.FromJson<GraphQLResponseData>(response.downloadHandler.text).data;

            ResponseDataShouldCorrect(data);
        }

        [Test]
        public async Task _01_2_Should_Query_Success_From_Builder()
        {
            var req = new GraphQLBuilder()
                      .SetUrl("https://api.mocki.io/v2/c4d7a195/graphql")
                      .SetTimeout(30)
                      .SetOperation(Operation.query)
                      .SetContent("{users{id email name}}")
                      .Build();

            var response = await req.SendWebRequest();

            var data = JsonUtility.FromJson<GraphQLResponseData>(response.downloadHandler.text).data;

            ResponseDataShouldCorrect(data);
        }

        private static void ResponseDataShouldCorrect(UserResponseData data)
        {
            Assert.AreEqual(2, data.users.Count);
            Assert.AreEqual("Hello World", data.users[0].name);
        }
    }
}