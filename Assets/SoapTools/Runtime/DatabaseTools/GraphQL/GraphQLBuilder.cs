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
        private UnityWebRequest req;

        public GraphQLBuilder()
        {
            req                 = new UnityWebRequest();
            req.downloadHandler = new DownloadHandlerBuffer();

            req.SetRequestHeader("content-type", "application/json");
        }

        public IGraphQLBuilder SetRequestSO(GraphQLRequestSO requestSo)
        {
            req.url     = requestSo.url;
            req.method  = "POST";
            req.timeout = requestSo.timeout;

            var escapedContent           = requestSo.content.Replace("\"", "\\\"");
            var withoutNewLines          = Regex.Replace(escapedContent, @"\r\n?|\n", " ");
            var withoutUnnecessarySpaces = Regex.Replace(withoutNewLines, @"\s+", " ");

            var jsonData = "{\"query\":\"" + withoutUnnecessarySpaces + "\"}";

            Debug.Log(jsonData);

            req.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonData));

            return this;
        }

        public IGraphQLBuilder SetUrl(string url)
        {
            req.url = url;
            return this;
        }

        public IGraphQLBuilder SetMethod(Method method)
        {
            req.method = method.ToString();
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
            req.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(content));
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
    }
}