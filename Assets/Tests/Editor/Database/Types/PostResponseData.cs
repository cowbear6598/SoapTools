using System;

namespace Test.Editor.Database
{
    [Serializable]
    public struct PostResponseData
    {
        public int    id;
        public int    userId;
        public string title;
        public bool   completed;

        public PostResponseData(int id, int userId, string title, bool completed)
        {
            this.id        = id;
            this.userId    = userId;
            this.title     = title;
            this.completed = completed;
        }
    }
}