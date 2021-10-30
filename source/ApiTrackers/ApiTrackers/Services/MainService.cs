
using ApiCells.Services;
using ApiRightMusics.Services;
using ApiSamples.Services;
using ApiTrackers.Services;

namespace ApiTrackers
{
    public class MainService
    {

        public BDD_MainService bdd;
        public BDD_TrackerService bddTracker;
        public BDD_UserService bddUser;
        public BDD_SampleService bddSamples;
        public BDD_CellService bddCells;
        public BDD_RightMusicService bddRightMusics;

        public MainService()
        {
            bdd = new BDD_MainService(this);
            bddTracker = new BDD_TrackerService(this);
            bddUser = new BDD_UserService(this);
            bddSamples = new BDD_SampleService(this);
            bddCells = new BDD_CellService(this);
            bddRightMusics = new BDD_RightMusicService(this);
        }
    }
}