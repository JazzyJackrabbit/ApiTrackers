using ApiTrackers;

namespace ApiCells.Controllers
{
    public class CellCreateDTO
    {
        public int idTracker { get; set; }
        public int idPiste { get; set; }
        public int idEffect { get; set; }
        public int idSample { get; set; }
        public double frequence { get; set; }
        public double volume { get; set; }
        public double position { get; set; }
        public string positionKey { get; set; }

        public int idUser { get; set; }

        public Note toCell()
        {
            var cellToInsert = new Note();

            // todo

            cellToInsert.sample.id = idSample;
            cellToInsert.piste.id = idPiste;
            cellToInsert.effect.id = idEffect;

            cellToInsert.surround.id = 0;
            cellToInsert.parentTracker.id = idTracker;

            cellToInsert.frequence = frequence;
            cellToInsert.volume = volume;
            cellToInsert.position = position;
            cellToInsert.positionKey = positionKey;

            return cellToInsert;
        }

    }
}