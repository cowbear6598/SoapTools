using UnityEngine;

namespace SoapTools.Database
{
    [CreateAssetMenu(fileName = "DB_GraphQLRequest", menuName = "DB/GraphQLRequest")]
    public class GraphQLRequestSO : ScriptableObject
    {
        public string    url;
        public Operation operation;
        public int       timeout = 30;

        [TextArea(50, 100)] public string content;
    }
}