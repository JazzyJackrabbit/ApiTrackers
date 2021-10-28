using ApiTrackers.Objects;

namespace ApiTrackers
{
    public class Note
    {
        public int id;
        public Tracker parentTracker;
        public Piste parentPiste;

        public Note(Tracker _parentTracker, Piste _parentPiste, double _position, string _key)
        {
            parentTracker = _parentTracker;
            parentPiste 
                = piste 
                = _parentPiste;
            position = _position;
            freqSample = 1;
            volume = 1;
            effect = new Effect();
            sample = new Sample();
            surround = new Surround().setStereo();
            key = _key;
        }

        public Note()
        {
            parentTracker = null;
            parentPiste = null;
            position = 0;
            freqSample = 1;
            volume = 1;
            effect = new Effect();
            sample = new Sample();
            surround = new Surround().setStereo();
            key = "C-5";
        }

        public double position;
        public Sample sample;
        public double freqSample;
        public double volume;
        public Effect effect;
        public Surround surround;
        public Piste piste; // This is Parent Piste Too !
        public string key;
    }
}