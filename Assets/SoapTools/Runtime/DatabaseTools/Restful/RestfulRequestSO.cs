﻿using System;
using SoapTools.Database;
using UnityEngine;
using UnityEngine.Networking;

namespace DatabaseTools.Runtime
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

    [CreateAssetMenu(fileName = "DB_Request", menuName = "DB/Request")]
    public class RestfulRequestSO : ScriptableObject
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