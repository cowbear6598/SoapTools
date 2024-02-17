using System;
using System.Text;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace SoapTools.Database
{
    public class GraphQLBuilder : IGraphQLBuilder
    {
        private Operation       operation = Operation.query;
        private UnityWebRequest req;

        public GraphQLBuilder()
        {
            req                 = new UnityWebRequest();
            req.method          = "POST";
            req.downloadHandler = new DownloadHandlerBuffer();

            req.SetRequestHeader("content-type", "application/json");
        }

        public IGraphQLBuilder SetRequestSO(GraphQLRequestScriptableObject requestScriptableObject)
        {
            req.url     = requestScriptableObject.url;
            req.timeout = requestScriptableObject.timeout;

            operation = requestScriptableObject.operation;

            var jsonRaw = MakeGraphQLContent(requestScriptableObject.content);

            req.uploadHandler = new UploadHandlerRaw(jsonRaw);

            return this;
        }

        public IGraphQLBuilder SetUrl(string url)
        {
            req.url = url;
            return this;
        }

        public IGraphQLBuilder SetOperation(Operation operation)
        {
            this.operation = operation;
            return this;
        }

        public IGraphQLBuilder SetTimeout(int timeout)
        {
            req.timeout = timeout;
            return this;
        }

        public IGraphQLBuilder AddHeader(string key, string value)
        {
            req.SetRequestHeader(key, value);
            return this;
        }

        public IGraphQLBuilder SetContent(string content)
        {
            var jsonRaw = MakeGraphQLContent(content);

            req.uploadHandler = new UploadHandlerRaw(jsonRaw);
            return this;
        }

        public UnityWebRequest Build()
        {
            return req;
        }

        /// <summary>
        /// 開始呼叫 API，若失敗則會拋出例外
        /// </summary>
        public async UniTask<TResponseData> StartRequest<TResponseData>()
        {
            await req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
                throw new Exception(req.error);

            return JsonUtility.FromJson<TResponseData>(req.downloadHandler.text);
        }

        private byte[] MakeGraphQLContent(string content)
        {
            var escapedContent           = content.Replace("\"", "\\\"");
            var withoutNewLines          = Regex.Replace(escapedContent, @"\r\n?|\n", " ");
            var withoutUnnecessarySpaces = Regex.Replace(withoutNewLines, @"\s+", " ");

            var jsonData = "";

            switch (operation)
            {
                case Operation.query:
                    jsonData = "{\"query\":\"" + withoutUnnecessarySpaces + "\"}";
                    break;
                case Operation.mutation:
                    jsonData = "{\"query\": \"mutation" + withoutUnnecessarySpaces + "\"}";
                    break;
            }

            return Encoding.UTF8.GetBytes(jsonData);
        }
    }
}