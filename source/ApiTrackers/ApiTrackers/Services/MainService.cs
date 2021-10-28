
using ApiTrackers.Services;

namespace ApiTrackers
{
    public class MainService
    {

        public BDD_MainService bdd;
        public BDD_TrackerService bddTracker;
        public BDD_UserService bddUser;
        public BDD_SampleService bddSamples;

        public MainService()
        {
            bdd = new BDD_MainService(this);
            bddTracker = new BDD_TrackerService(this);
            bddUser = new BDD_UserService(this);
            bddSamples = new BDD_SampleService(this);
        }
    }
}