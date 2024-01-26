using System;

namespace Test.Editor.Database
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