using ApiTrackers.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.DTO_ApiParameters
{
    public class SampleAliasCreateDTO
    {
        public int idSample { get; set; }
        public string color { get; set; }
        public int idLogo { get; set; }
        public string name { get; set; }

        public int idUser { get; set; }

        public SampleAlias toSampleAlias()
        {
            var sampleAToInsert = new SampleAlias(idUser, idSample);
            sampleAToInsert.color = color;
            sampleAToInsert.idLogo = idLogo;
            sampleAToInsert.name = name;
            return sampleAToInsert;
        }

    }
}
