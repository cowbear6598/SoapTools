using Cysharp.Threading.Tasks;
using SoapTools.Database;
using UnityEngine.Networking;

namespace SoapTools.Database
{
    public interface IGraphQLBuilder
    {
        IGraphQLBuilder SetRequestSO(GraphQLRequestSO requestSo);
        IGraphQLBuilder SetUrl(string url);
        IGraphQLBuilder SetMethod(Method method);
        IGraphQLBuilder SetTimeout(int timeout);
        IGraphQLBuilder AddHeader(string key, string value);
        IGraphQLBuilder SetContent(string content);
        UnityWebRequest Build();
        UniTask<TResponseData> StartRequest<TResponseData>();
    }
}