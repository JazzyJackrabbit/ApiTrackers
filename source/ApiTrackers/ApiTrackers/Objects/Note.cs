using ApiTrackers.Objects;

namespace ApiTrackers
{
    public class Note
    {
        public int id;
        public Tracker parentTracker;
        public Piste parentPiste;

        public Note(Tracker _parentTracker, Piste _parentPiste, int _position)
        {
            parentTracker = _parentTracker;
            parentPiste = _parentPiste;
            position = _position;
            freqSample = 1;
            volume = 1;
            effect = new Effect();
            sample = new Sample();
            surround = new Surround().setStereo();
        }

        public int position;
        public Sample sample;
        public double freqSample;
        public double volume;
        public Effect effect;
        public Surround surround;
    }
}