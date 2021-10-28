using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Services
{
    public class BDD_SampleService
    {
        BDD_MainService bdd;
        MainService main;
        private int lastId = -1;

        public BDD_SampleService(MainService _main)
        {
            main = _main;
            bdd = _main.bdd;

            lastId = main.bdd.selectLastID(bdd.sqlTableSamples);
        }

        public int getLastId() { return lastId; }
        public int getNextId() { lastId++; return lastId; }
    }
}
