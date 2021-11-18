using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using SharpMik.Drivers;
using SharpMik.Player;
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
        Stream module = null;

        public ModuleTrackerDTO(Stream _module)
        {
            module = _module;
        }

        public Tracker getTracker()
        {
            return tracker;
        }

        public Tracker moduleToTracker(User _user)
        {
            MikMod player;
            player = new MikMod();
            player.Init<NoAudio>("temp.wav");
            SharpMik.Module moduleSharpMik = player.LoadModule(module);

            string songName = moduleSharpMik.songname;
            string modType = moduleSharpMik.modtype;
            short speed = moduleSharpMik.initspeed;
            ushort tempo = moduleSharpMik.inittempo;
            string comment = moduleSharpMik.comment;

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
