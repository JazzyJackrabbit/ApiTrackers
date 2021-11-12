using Microsoft.AspNetCore.Http;
using SharpMod;
using SharpMod.Song;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Objects
{
    public class ModulePistesDTO
    {

        List<Piste> pistes = null;
        SongModule module = null;

        public ModulePistesDTO(IFormFile _fileContent)
        {
            if (_fileContent.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    _fileContent.CopyTo(ms);
                    module = ModuleLoader.Instance.LoadModule(ms);
                }
            }
        }

        public List<Piste> getPistes()
        {
            return pistes;
        }

        public List<Piste> moduleToPistes(Tracker _tracker)
        {
            pistes = new List<Piste>();
            foreach (Pattern pattern in module.Patterns)
                for (int j = 0; j < pattern.Tracks.Count; j++)
                {
                    Piste piste = new Piste();
                    piste.id = j;
                    piste.color = "#ddd";
                    piste.name = "Piste #"+(j+1).ToString();

                    pistes.Add(piste);
                }
            return pistes;
        }

    }
}
