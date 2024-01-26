using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Test.Editor.Database;
using Test.Editor.Database.Database;
using UnityEngine;

namespace Test.Editor.Database
{
    [TestFixture]
    public class GraphQLTests
    {
        [Test]
        public async Task Should_Query_Success_From_SO()
        {
            var requestSO = Resources.Load<GraphQLRequestSO>("GraphQL/Query");
            var req = new GraphQLBuilder()
                      .SetRequestSO(requestSO)
                      .Build();

            var response = await req.SendWebRequest();

            var data = JsonUtility.FromJson<GraphQLResponseData>(response.downloadHandler.text).data;

            QueryResponseDataShouldCorrect(data);
        }

        [Test]
        public async Task Should_Query_Success_From_Builder()
        {
            var req = new GraphQLBuilder()
                      .SetUrl("https://api.mocki.io/v2/c4d7a195/graphql")
                      .SetTimeout(30)
                      .SetOperation(Operation.query)
                      .SetContent("{users{id email name}}")
                      .Build();

            var response = await req.SendWebRequest();

            var data = JsonUtility.FromJson<GraphQLResponseData>(response.downloadHandler.text).data;

            QueryResponseDataShouldCorrect(data);
        }

        [Test]
        public async Task Should_Mutation_Success_From_SO()
        {
            var requestSO = Resources.Load<GraphQLRequestSO>("GraphQL/Mutation");
            var req = new GraphQLBuilder()
                      .SetRequestSO(requestSO)
                      .Build();

            var response = await req.SendWebRequest();

            var data = JsonUtility.FromJson<GraphQLMutationResponseData>(response.downloadHandler.text).data;

            MutationResponseShouldCorrect(data);
        }

        [Test]
        public async Task Should_Mutation_Success_From_Build()
        {
            var req = new GraphQLBuilder()
                      .SetUrl("https://api.mocki.io/v2/c4d7a195/graphql")
                      .SetTimeout(30)
                      .SetOperation(Operation.mutation)
                      .SetContent("{ updateTodo(input: { id: \"Hello World\", done: false }) {id done}}")
                      .Build();

            var response = await req.SendWebRequest();

            var data = JsonUtility.FromJson<GraphQLMutationResponseData>(response.downloadHandler.text).data;

            MutationResponseShouldCorrect(data);
        }

        private static void MutationResponseShouldCorrect(TodoResponseData data)
        {
            Assert.AreEqual("Hello World", data.updateTodo.id);
        }

        private static void QueryResponseDataShouldCorrect(UserResponseData data)
        {
            Assert.AreEqual(2, data.users.Count);
            Assert.AreEqual("Hello World", data.users[0].name);
        }
    }
}