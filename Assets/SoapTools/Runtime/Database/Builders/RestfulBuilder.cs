using System;
using System.Text;
using Cysharp.Threading.Tasks;
using SoapTools.Database.ScriptableObjects;
using UnityEngine;
using UnityEngine.Networking;

namespace SoapTools.Database.Builders
{
	public class RestfulBuilder
	{
		private readonly UnityWebRequest req;

		public RestfulBuilder()
		{
			req                 = new UnityWebRequest();
			req.downloadHandler = new DownloadHandlerBuffer();
		}

		public RestfulBuilder SetRequestSO(RestfulRequestScriptableObject apiSO)
		{
			req.url     = apiSO.url;
			req.method  = apiSO.method.ToString();
			req.timeout = apiSO.timeout;

			apiSO.SetRequestHeaders(req);

			return this;
		}

		public RestfulBuilder SetUrl(string url)
		{
			req.url = url;
			return this;
		}

		public RestfulBuilder SetMethod(Method method)
		{
			req.method = method.ToString();
			return this;
		}

		public RestfulBuilder SetTimeout(int timeout)
		{
			req.timeout = timeout;
			return this;
		}

		public RestfulBuilder AddHeader(string key, string value)
		{
			req.SetRequestHeader(key, value);
			return this;
		}

		public RestfulBuilder AddQuery(string key, string value)
		{
			if (req.url.Contains("?"))
				req.url += "&";
			else
				req.url += "?";

			req.url += $"{key}={value}";

			return this;
		}

		public RestfulBuilder SetBody<TRequestData>(TRequestData data)
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

		/// <summary>
		/// 簡易的呼叫 API，只需傳入 API ScriptableObject
		/// </summary>
		public async UniTask<TResponseData> SimpleRequest<TResponseData>(RestfulRequestScriptableObject apiSO)
		{
			SetRequestSO(apiSO);

			return await StartRequest<TResponseData>();
		}
	}
}