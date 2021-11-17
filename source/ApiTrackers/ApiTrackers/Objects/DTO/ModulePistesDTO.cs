using Microsoft.AspNetCore.Http;
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
        SharpMik.Module module = null;

        public ModulePistesDTO(SharpMik.Module _module)
        {
            module = _module;
        }

        public List<Piste> getPistes()
        {
            return pistes;
        }

        public List<Piste> moduleToPistes(Tracker _tracker)
        {
            pistes = new List<Piste>();

            /*
            int posI = 0;
            //foreach (byte[] patternI in module.tracks) 
            foreach (byte[] track in module.tracks)
            {
                Piste piste = new Piste();
                piste.id = posI;
                piste.color = "#ddd";
                piste.name = "Piste #" + (posI + 1).ToString();

                pistes.Add(piste);
                posI++;
            }
            */

            pistes.Add(new Piste());

            _tracker.trackerContent.pistes = pistes;

            return pistes;
        }

    }
}
