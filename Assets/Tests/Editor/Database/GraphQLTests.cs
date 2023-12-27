using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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

            Debug.Log(response.downloadHandler.text);
        }
    }
}