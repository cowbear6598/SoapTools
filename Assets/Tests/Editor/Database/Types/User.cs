using System;
using System.Collections.Generic;

namespace Test.Editor.Database
{
    [Serializable]
    public class UserResponseData
    {
        public List<User> users;
    }

    [Serializable]
    public class User
    {
        public string id;
        public string name;
        public string email;
    }
}