using Microsoft.AspNetCore.Http;
using SharpMod;
using SharpMod.Song;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Objects
{
    public class ModuleCellsDTO
    {

        List<Note> cells = null;
        SongModule module = null;

        public ModuleCellsDTO(IFormFile _fileContent)
        {
            if (_fileContent.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    _fileContent.CopyTo(ms);
                    module = ModuleLoader.Instance.LoadModule(ms);
                }
            }
        }

        public List<Note> getCells()
        {
            return cells;
        }

        public List<Note> moduleToCells(Tracker _tracker, Piste _piste, List<Sample> _samples)
        {
            foreach (Pattern pattern in module.Patterns)
                for (int j = 0; j < pattern.Tracks.Count; j++)
                {
                    Track track = pattern.Tracks[j];

                    int PosICell = 0;
                    foreach (PatternCell cell in track.Cells)
                    {
                        short effect = cell.Effect;
                        short effectData = cell.EffectData;
                        short instrument = cell.Instrument; // < local file sample ID 
                        short period = cell.Period;
                        int? noteValue = cell.Note;
                        int? octaveValue = cell.Octave;
                        double position = PosICell * 256;

                        string OctaveNote = "{\"O\":" + octaveValue + ", \"N\":" + noteValue + "}";

                        Note note = new Note(_tracker, _piste, position, OctaveNote);

                        note.effect = new Effect(effect);
                        note.freqSample = 1;

                        note.sample = new Sample();
                        foreach (Sample sampleFind in _samples)
                            if(sampleFind.localInstrumentId == instrument)
                                note.sample = sampleFind;

                        note.surround = new Surround();
                        note.volume = 1;

                        cells.Add(note);
                        PosICell++;
                    }

                }
            return cells;
        }

    }
}
