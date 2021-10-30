using ApiTrackers;

namespace ApiCells.Controllers
{
    public class CellUpdateDTO
    {
        public int id { get; set; }
        public int idTracker { get; set; }
        public int idPiste { get; set; }
        public int idEffect { get; set; }
        public int idSample { get; set; }
        public double frequence { get; set; }
        public double volume { get; set; }
        public double position { get; set; }
        public string key { get; set; }

        public Note toCell()
        {
            var cellToInsert = new Note();
            cellToInsert.id = id;

            cellToInsert.sample.id = idSample;
            cellToInsert.piste.id = idPiste;
            cellToInsert.effect.id = idEffect;

            cellToInsert.surround.id = 0;
            cellToInsert.parentTracker.idTracker = idTracker;

            cellToInsert.freqSample = frequence;
            cellToInsert.volume = volume;
            cellToInsert.position = position;
            cellToInsert.key = key;

            return cellToInsert;
        }

    }
}