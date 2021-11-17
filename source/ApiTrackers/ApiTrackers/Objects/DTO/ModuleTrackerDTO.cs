using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTrackers.Objects
{
    public class ModuleTrackerDTO
    {

        Tracker tracker = null;
        SharpMik.Module module = null;

        public ModuleTrackerDTO(SharpMik.Module _module)
        {
            module = _module;
        }

        public Tracker getTracker()
        {
            return tracker;
        }

        public Tracker moduleToTracker(User _user)
        {
            string songName = module.songname;
            string modType = module.modtype;
            short speed = module.initspeed;
            ushort tempo = module.inittempo;
            string comment = module.comment;

            Tracker tempTracker = new Tracker();
            tempTracker.idUser = _user.id;
            tempTracker.trackerMetadata.artist = _user.pseudo;
            tempTracker.trackerMetadata.title = songName;
            tempTracker.trackerMetadata.comments = comment;
            tempTracker.trackerMetadata.copyrightInformation = "Unknown";
            tempTracker.trackerContent.BPM = tempo.ToString();
            tempTracker.trackerContent.pistes = new List<Piste>();

            return tempTracker;
        }

    }

}
