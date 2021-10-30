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
        public RightForMusic right = RightForMusic.canRead;

        public enum RightForMusic
        {
            nothing,
            canRead,
            canEdit,
        }
        public int setRight(RightForMusic _rightEnum)
        {
            return (int)(right = _rightEnum);
        }
        public RightForMusic setRight(int _val)
        {
            right = (RightForMusic)_val;
            return right;
        }
        public RightForMusic setRight(object _val)
        {
            right = (RightForMusic)Static.convertToInteger(_val);
            return right;
        }
      

    }

}
