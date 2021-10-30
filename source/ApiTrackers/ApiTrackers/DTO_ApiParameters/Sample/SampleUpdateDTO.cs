using ApiTrackers.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.DTO_ApiParameters
{
    public class SampleUpdateDTO
    {
        public int id { get; set; }
        public string linkSample { get; set; }
        public string color { get; set; }
        public int idLogo { get; set; }
        public string name { get; set; }

        public int idUser { get; set; }

        public Sample toSample()
        {
            var sampleToInsert = new Sample();
            sampleToInsert.linkSample = linkSample;
            sampleToInsert.color = color;
            sampleToInsert.id = id;
            sampleToInsert.idLogo = idLogo;
            sampleToInsert.name = name;
            return sampleToInsert;
        }

    }
}
