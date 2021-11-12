using ApiTrackers.Objects;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ApiTrackers.DTO_ApiParameters.Module
{
    public class Module
    {
        ModuleTrackerDTO trackerDTO { get; set; }
        ModuleSamplesDTO samplesDTO { get; set; }
        ModulePistesDTO pistesDTO { get; set; }
        ModuleCellsDTO cellsDTO { get; set; }

        Tracker tracker = new Tracker();
        List<Piste> pistes = new List<Piste>();
        List<Sample> samples = new List<Sample>();
        List<Note> notes = new List<Note>();

        public Module(IFormFile _file, User _user)
        {
            string pathSamples = @"C:\Users\Alexandre\Desktop\samples";
            trackerDTO = new ModuleTrackerDTO(_file);
            samplesDTO = new ModuleSamplesDTO(_file);
            pistesDTO = new ModulePistesDTO(_file);
            cellsDTO = new ModuleCellsDTO(_file);

            tracker = trackerDTO.moduleToTracker(_user);
            pistes = pistesDTO.moduleToPistes(tracker);
            tracker.trackerContent.pistes = pistes;
            samples = samplesDTO.moduleToSamples(true, pathSamples);
            foreach(Piste piste in pistes)
            {
                List<Note> subListNote = cellsDTO.moduleToCells(tracker, piste, samples);
                foreach(Note pisteSub in subListNote)
                    notes.Add(pisteSub);
            }
        }

        public Tracker getTracker()
        {
            return tracker;
        }
        public List<Piste> getPistes()
        {
            return pistes;
        }
        public List<Sample> getSamples()
        {
            return samples;
        }
        public List<Note> getNotes()
        {
            return notes;
        }

    }
}
