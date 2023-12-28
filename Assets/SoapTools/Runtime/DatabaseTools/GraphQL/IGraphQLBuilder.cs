using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace SoapTools.Database
{
    public interface IGraphQLBuilder
    {
        IGraphQLBuilder SetRequestSO(GraphQLRequestSO requestSo);
        IGraphQLBuilder SetUrl(string url);
        IGraphQLBuilder SetOperation(Operation operation);
        IGraphQLBuilder SetTimeout(int timeout);
        IGraphQLBuilder AddHeader(string key, string value);
        IGraphQLBuilder SetContent(string content);
        UnityWebRequest Build();
        UniTask<TResponseData> StartRequest<TResponseData>();
    }
}