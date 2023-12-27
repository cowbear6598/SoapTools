﻿using Cysharp.Threading.Tasks;
using DatabaseTools.Runtime;
using UnityEngine.Networking;

namespace SoapTools.Database
{
    public interface IRestfulBuilder
    {
        IRestfulBuilder SetRequestSO(RestfulRequestSO requestSo);
        IRestfulBuilder SetUrl(string url);
        IRestfulBuilder SetMethod(Method method);
        IRestfulBuilder SetTimeout(int timeout);
        IRestfulBuilder SetBody<TRequestData>(TRequestData body);
        IRestfulBuilder AddHeader(string header, string value);
        IRestfulBuilder AddQuery(string key, string value);
        UnityWebRequest Build();
        UniTask<TResponseData> StartRequest<TResponseData>();
    }
}