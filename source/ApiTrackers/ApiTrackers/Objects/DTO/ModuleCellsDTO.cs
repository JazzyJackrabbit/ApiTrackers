using ApiTrackers.DTO_ApiParameters.Module;
using Microsoft.AspNetCore.Http;
using SharpMik;
using SharpMik.Drivers;
using SharpMik.Player;
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
        Stream module = null;

        public ModuleCellsDTO(Stream _module)
        {
            module = _module;
        }

        public List<Note> getCells()
        {
            return cells;
        }

        public List<Note> moduleToCells(Tracker _tracker, List<Sample> _samples)
        {

            MikMod player;
            player = new MikMod();
            player.Init<NoAudio>("temp.wav");
            SharpMik.Module moduleSharpMik = player.LoadModule(module);

            cells = new List<Note>();

            for(int p = 0; p < moduleSharpMik.patterns.Length; p++)
            {
                ushort patternsId = moduleSharpMik.patterns[p];
            }
            //TODO:
            /*
            for (int i = 0; i < moduleSharpMik.tracks.Length; i++)
            {
              for (int j = 0; j < moduleSharpMik.tracks[i].Length; j++)
               {
                   byte cellI = moduleSharpMik.tracks[i][j];
                   try
                   {
                       if(cellI < moduleSharpMik.control.Length) { 
                           MP_CONTROL controlCell = moduleSharpMik.control[cellI];

                           short effect = controlCell.sseffect;
                           short effectVolume = controlCell.voleffect;
                           short instrument = controlCell.dct; // < local file sample ID 
                           ushort period = controlCell.wantedperiod;
                           int? noteValue = controlCell.anote;
                           int? octaveValue = 5; //;
                           double position = cellI * 256;

                           string OctaveNote = "{\"O\":" + octaveValue + ", \"N\":" + noteValue + "}";

                           Piste pisteCurrent = _tracker.trackerContent.pistes[0];

                           Note note = new Note(_tracker, pisteCurrent, position, OctaveNote);

                           note.effect = new Effect(effect);
                           note.freqSample = 1;

                           note.sample = new Sample();
                           foreach (Sample sampleFind in _samples)
                               //if (sampleFind.linkSample == instrument)
                                   note.sample = sampleFind;

                           note.surround = new Surround();
                           note.volume = 1;

                           cells.Add(note);
                       }
                   }
                   catch { }

                }
            }*/

            return cells;
        }

    }
}
