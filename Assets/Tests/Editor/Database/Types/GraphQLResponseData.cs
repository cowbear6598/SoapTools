using System;

namespace Test.Editor.Database
{
    [Serializable]
    public class GraphQLResponseData
    {
        public UserResponseData data;
    }

    [Serializable]
    public class GraphQLMutationResponseData
    {
        public TodoResponseData data;
    }
}