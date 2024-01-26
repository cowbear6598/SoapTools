using System;

namespace Test.Editor.Database.Database
{
    [Serializable]
    public struct GraphQLRequest
    {
        public string query;
        public string variables;
    }
}