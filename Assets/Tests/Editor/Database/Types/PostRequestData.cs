using System;

namespace SoapTools.Database.Tests.Editor.Types
{
    [Serializable]
    public struct PostRequestData
    {
        public string title;
        public string body;
        public int    userId;

        public PostRequestData(string title, string body, int userId)
        {
            this.title  = title;
            this.body   = body;
            this.userId = userId;
        }
    }
}