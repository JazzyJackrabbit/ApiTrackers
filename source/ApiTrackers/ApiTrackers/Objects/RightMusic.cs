using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Objects
{
    public class RightMusic
    {
        [JsonProperty(PropertyName = "id")]
        public int id;
        [JsonProperty(PropertyName = "idTracker")]
        public int idTracker;
        [JsonProperty(PropertyName = "idUser")]
        public int idUser;
        [JsonIgnore]
        public RightForMusic right = RightForMusic.Read;

        [JsonProperty(PropertyName = "right")]
        public string rightString {
            get { return right.ToString(); }  
            set { rightString = right.ToString(); }
        }


        public enum RightForMusic
        {
            Nothing,
            Read,
            Edit,
        }

        public RightForMusic setRight(int _val)
        {
            right = (RightForMusic)Static.convertToInteger(_val);
            return right;
        }
        public RightForMusic setRight(string _val)
        {
            RightForMusic rfm = (RightForMusic)Enum.Parse(typeof(RightForMusic), _val, true);
            right = rfm;
            return rfm;
        }

    }

}
