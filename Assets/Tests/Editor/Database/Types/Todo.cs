using System;

namespace DatabaseTools.Tests.Editor.Types
{
    [Serializable]
    public class TodoResponseData
    {
        public Todo updateTodo;
    }

    [Serializable]
    public class Todo
    {
        public string id;
        public bool   done;
    }
}