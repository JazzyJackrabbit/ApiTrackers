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

            if (moduleSharpMik == null) return null;

            cells = new List<Note>();

            for(int p = 0; p < moduleSharpMik.patterns.Length; p++)
            {
                ushort patternsId = moduleSharpMik.patterns[p];
            }

            return cells;
        }

    }
}
