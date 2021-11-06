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
        [JsonProperty(PropertyName = "right")]
        public RightForMusic right = RightForMusic.Read;

        public enum RightForMusic
        {
            Nothing,
            Read,
            Edit,
        }

        public int setRight(RightForMusic _rightEnum)
        {
            return (int)(right = _rightEnum);
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

        public bool isEqual(RightForMusic _right)
        {
            if (_right == right) return true;
            return false;
        }


    }

}
