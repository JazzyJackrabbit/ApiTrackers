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
        Stream module = null;

        public ModulePistesDTO(Stream _module)
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

            pistes.Add(new Piste());

            if (_tracker == null) return null;
            _tracker.trackerContent.pistes = pistes;

            return pistes;
        }

    }
}
