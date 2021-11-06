
using ApiCells.Services;
using ApiRightMusics.Services;
using ApiSamples.Services;
using ApiTrackers.Services;

namespace ApiTrackers
{
    public class Main
    {

        public SqlDatabase bdd;
        public DB_TrackerService bddTracker;
        public DB_UserService bddUser;
        public DB_SampleService bddSamples;
        public DB_SampleAliasService bddSamplesAlias;
        public DB_CellService bddCells;
        public DB_RightMusicService bddRightMusics;

        public Main()
        {
            bdd = new SqlDatabase(this);
            bddTracker = new DB_TrackerService(this);
            bddUser = new DB_UserService(this);
            bddSamples = new DB_SampleService(this);
            bddSamplesAlias = new DB_SampleAliasService(this);
            bddCells = new DB_CellService(this);
            bddRightMusics = new DB_RightMusicService(this);
        }
    }
}