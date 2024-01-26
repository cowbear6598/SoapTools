using System;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Test.Editor.Database.Database
{
    public class RestfulBuilder : IRestfulBuilder
    {
        private UnityWebRequest req;

        public RestfulBuilder()
        {
            req                 = new UnityWebRequest();
            req.downloadHandler = new DownloadHandlerBuffer();
        }

        public IRestfulBuilder SetRequestSO(RestfulRequestSO requestSo)
        {
            req.url     = requestSo.url;
            req.method  = requestSo.method.ToString();
            req.timeout = requestSo.timeout;
            requestSo.SetRequestHeaders(req);

            return this;
        }

        public IRestfulBuilder SetUrl(string url)
        {
            req.url = url;
            return this;
        }

        public IRestfulBuilder SetMethod(Method method)
        {
            req.method = method.ToString();
            return this;
        }

        public IRestfulBuilder SetTimeout(int timeout)
        {
            req.timeout = timeout;
            return this;
        }

        public IRestfulBuilder AddHeader(string key, string value)
        {
            req.SetRequestHeader(key, value);
            return this;
        }

        public IRestfulBuilder AddQuery(string key, string value)
        {
            if (req.url.Contains("?"))
                req.url += "&";
            else
                req.url += "?";

            req.url += $"{key}={value}";

            return this;
        }

        public IRestfulBuilder SetBody<TRequestData>(TRequestData data)
        {
            var jsonRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
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
    }
}