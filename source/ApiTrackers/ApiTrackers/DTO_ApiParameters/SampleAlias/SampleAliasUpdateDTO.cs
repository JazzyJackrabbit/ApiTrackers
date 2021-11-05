using ApiTrackers.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.DTO_ApiParameters
{
    public class SampleAliasUpdateDTO
    {
        public int id { get; set; }
        public int idSample { get; set; }
        public string color { get; set; }
        public int idLogo { get; set; }
        public string name { get; set; }
        public int idUser { get; set; }


        public SampleAlias toSampleAlias()
        {
            var sampleToInsert = new SampleAlias(idUser, idSample);
            sampleToInsert.color = color;
            sampleToInsert.id = id;
            sampleToInsert.idLogo = idLogo;
            sampleToInsert.name = name;
            return sampleToInsert;
        }

    }
}
