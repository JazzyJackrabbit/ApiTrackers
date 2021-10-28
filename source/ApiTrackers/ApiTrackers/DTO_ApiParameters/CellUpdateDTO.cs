using ApiTrackers;

namespace ApiCells.Controllers
{
    public class CellUpdateDTO
    {
        public int id { get; set; }
        public int idTracker { get; set; }
        public int idPiste { get; set; }
        public int idEffect { get; set; }
        public double frequence { get; set; }
        public double volume { get; set; }
        public double position { get; set; }
        public string keyVal { get; set; }

        public Note toCell()
        {
            var cellToInsert = new Note();
            cellToInsert.id = id;

            // todo
            // cellToInsert.idTracker = idTracker; 
            // cellToInsert.idPiste = idPiste;
            // cellToInsert.idEffect = idEffect;

            cellToInsert.freqSample = frequence;
            cellToInsert.volume = volume;
            cellToInsert.position = position;
            cellToInsert.key = keyVal;

            return cellToInsert;
        }

    }
}