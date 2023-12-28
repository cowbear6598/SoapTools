using System;

namespace DatabaseTools.Tests.Editor.Types
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