using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Objects
{
    public class RightMusic
    {
        public int id;
        public int idTracker;
        public int idUser;
        public RightForMusic right = RightForMusic.canRead;

        public enum RightForMusic
        {
            nothing,
            canRead,
            canEdit,
        }
    }


}
