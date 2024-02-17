using System;
using UnityEngine;
using UnityEngine.Networking;

namespace SoapTools.Database
{
    [Serializable]
    public class RequestHeaders
    {
        public Header header;
        public string value;

        public string GetHeaderKey()
        {
            return header switch {
                Header.Accept        => "accept",
                Header.Authorization => "authorization",
                Header.ContentType   => "content-type",
                Header.ContentLength => "content-length",
                _                    => "",
            };
        }
    }

    [CreateAssetMenu(fileName = "RestfulRequest", menuName = "Soap/Database/RestfulRequest")]
    public class RestfulRequestScriptableObject : ScriptableObject
    {
        public Method           method;
        public string           domain;
        public string           api;
        public int              timeout = 30;
        public RequestHeaders[] headers;

        public string url => domain + api;

        public void SetRequestHeaders(UnityWebRequest req)
        {
            for (int i = 0; i < headers.Length; i++)
            {
                req.SetRequestHeader(headers[i].GetHeaderKey(), headers[i].value);
            }
        }
    }
}