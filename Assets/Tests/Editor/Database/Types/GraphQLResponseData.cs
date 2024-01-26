using System;

namespace SoapTools.Database.Tests.Editor.Types
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