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
        public int id { get; set; }
        [JsonProperty(PropertyName = "idTracker")]
        public int idTracker { get; set; }
        [JsonProperty(PropertyName = "idUser")]
        public int idUser { get; set; }
        [JsonProperty(PropertyName = "right")]
        public RightForMusic right { get; set; } = RightForMusic.Read;

        public enum RightForMusic
        {
            Nothing,
            Read,
            Edit,
        }
        public int SetRight(RightForMusic _rightEnum)
        {
            return (int)(right = _rightEnum);
        }
        public RightForMusic SetRight(int _val)
        {
            right = (RightForMusic)Utils.ConvertToInteger(_val);
            return right;
        }
        public RightForMusic SetRight(string _val)
        {
            RightForMusic rfm = (RightForMusic)Enum.Parse(typeof(RightForMusic), _val, true);
            right = rfm;
            return rfm;
        }
      

    }

}
