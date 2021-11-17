using ApiTrackers.DTO_ApiParameters.Module;
using Microsoft.AspNetCore.Http;
using SharpMik;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTrackers.Objects
{
    public class ModuleCellsDTO
    {

        List<Note> cells = new List<Note>();
        SharpMik.Module module = null;

        public ModuleCellsDTO(SharpMik.Module _module)
        {
            module = _module;
        }

        public List<Note> getCells()
        {
            return cells;
        }

        public List<Note> moduleToCells(Tracker _tracker, List<Piste> _pistes, List<Sample> _samples)
        {
            int posCount = 8;
            foreach (byte[] track in module.tracks)
            {
                if(posCount>1)
                foreach (byte cellI in track) //array of numtrk pointers to tracks
                {
                    try
                    {
                        MP_CONTROL controlCell = module.control[cellI];

                        short effect = controlCell.sseffect;
                        short effectVolume = controlCell.voleffect;
                        short instrument = controlCell.dct; // < local file sample ID 
                        ushort period = controlCell.wantedperiod;
                        int? noteValue = controlCell.anote;
                        int? octaveValue = 5; //;
                        double position = cellI * 256;

                        string OctaveNote = "{\"O\":" + octaveValue + ", \"N\":" + noteValue + "}";


                        Piste pisteCurrent = _pistes[0];

                        Note note = new Note(_tracker, pisteCurrent, position, OctaveNote);

                        note.effect = new Effect(effect);
                        note.freqSample = 1;

                        note.sample = new Sample();
                        foreach (Sample sampleFind in _samples)
                            if (sampleFind.localInstrumentId == instrument)
                                note.sample = sampleFind;

                        note.surround = new Surround();
                        note.volume = 1;

                        cells.Add(note);
                    }
                    catch { }

                }
                posCount--;
            }
              
            return cells;
        }

    }
}
