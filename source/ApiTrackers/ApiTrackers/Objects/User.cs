using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ApiTrackers.Objects
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "isEnable")]
        public int isEnable { get; set; }

        [JsonProperty(PropertyName = "mail")]
        public string mail { get; set; }

        [JsonProperty(PropertyName = "pseudo")]
        public string pseudo { get; set; }

        [JsonProperty(PropertyName = "passwordHash")]
        public string passwordHash { get; set; }

        [JsonProperty(PropertyName = "recoverMails")]
        public int recoverMails { get; set; }  = 1; // 0=false  1=true


        public User(string _pseudo, string _mail, string _passwordHash)
        {
            setUser(_pseudo, _mail, _passwordHash);
        }
        public User()
        {}

        public void setUser(string _pseudo, string _mail, string _passwordHash)
        {
            pseudo = _pseudo;
            mail = _mail;
            passwordHash = _passwordHash;
        }
        public void setUser(string _pseudo, string _mail, string _passwordHash, int _wantRecoverMails)
        {
            pseudo = _pseudo;
            mail = _mail;
            passwordHash = _passwordHash;
            recoverMails = _wantRecoverMails;
        }
    }
}
