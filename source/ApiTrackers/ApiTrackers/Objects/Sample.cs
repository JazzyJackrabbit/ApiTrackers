using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Objects
{
    public class Sample
    {
        public Sample()
        {
            id = 0;
            name = "none";
            linkSample = null;
            color = null;
            idLogo = 0;
        }
        public Sample(int _id, string _nameSample, string _linkSample)
        {
            id = _id;
            name = _nameSample;
            linkSample = _linkSample;
            color = null;
            idLogo = 0;
        }
        public Sample(int _id, string _nameSample, string _linkSample, string _color, int _idLogo)
        {
            id = _id;
            name = _nameSample;
            linkSample = _linkSample;
            color = _color;
            idLogo = _idLogo;
        }

        public int id;
        public string name;
        public string linkSample;

        public string color;
        public int idLogo;
    }
}
