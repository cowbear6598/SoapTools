using System;

namespace SoapTools.Database
{
    [Serializable]
    public struct GraphQLRequest
    {
        public string query;
        public string variables;
    }
}