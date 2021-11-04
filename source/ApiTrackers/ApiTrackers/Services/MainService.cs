
using ApiCells.Services;
using ApiRightMusics.Services;
using ApiSamples.Services;
using ApiTrackers.Services;

namespace ApiTrackers
{
    public class MainService
    {

        public DB_MainService bdd;
        public DB_TrackerService bddTracker;
        public DB_UserService bddUser;
        public DB_SampleService bddSamples;
        public DB_CellService bddCells;
        public DB_RightMusicService bddRightMusics;

        public MainService()
        {
            bdd = new DB_MainService(this);
            bddTracker = new DB_TrackerService(this);
            bddUser = new DB_UserService(this);
            bddSamples = new DB_SampleService(this);
            bddCells = new DB_CellService(this);
            bddRightMusics = new DB_RightMusicService(this);
        }
    }
}