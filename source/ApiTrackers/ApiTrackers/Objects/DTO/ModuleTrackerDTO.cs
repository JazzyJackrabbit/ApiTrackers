using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using SharpMod;
using SharpMod.Song;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Objects
{
    public class ModuleTrackerDTO
    {

        Tracker tracker = null;
        SongModule module = null;

        public ModuleTrackerDTO(IFormFile _fileContent)
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

        public Tracker getTracker()
        {
            return tracker;
        }

        public Tracker moduleToTracker(User _user)
        {
            string songName = module.SongName;
            string modType = module.ModType;
            short speed = module.InitialSpeed;
            short tempo = module.InitialTempo;
            string comment = module.Comment;

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
