﻿using ApiTrackers;

namespace ApiCells.Controllers
{
    public class CellCreateDTO
    {
        public int idTracker { get; set; }
        public int idPiste { get; set; }
        public int idEffect { get; set; }
        public double position { get; set; }
        public double frequence { get; set; }
        public double volume { get; set; }
        public string key { get; set; }

        public Note toCell()
        {
            var cellToInsert = new Note();

            // todo
            // cellToInsert.idTracker = idTracker; 
            // cellToInsert.idPiste = idPiste;
            // cellToInsert.idEffect = idEffect;

            cellToInsert.freqSample = frequence;
            cellToInsert.volume = volume;
            cellToInsert.position = position;
            cellToInsert.key = key;

            return cellToInsert;
        }

    }
}