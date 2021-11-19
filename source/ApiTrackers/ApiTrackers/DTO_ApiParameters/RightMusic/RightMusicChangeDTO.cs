using ApiTrackers.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.DTO_ApiParameters
{
    public class RightMusicChangeDTO
    {
        public int id { get; set; }
        public int idUser { get; set; }
        public int idTracker { get; set; }
        public string right { get; set; }

        public RightMusic toRightMusic()
        {
            var rmusic = new RightMusic();
            rmusic.id = id;
            rmusic.idUser = idUser;
            rmusic.idTracker = idTracker;
            rmusic.SetRight(right);
            return rmusic;
        }

    }
}
