using ApiTrackers.Exceptions;
using ApiTrackers.Objects;
using ApiTrackers.Services;
using ClientTest_APITrackers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SharpMik;
using SharpMik.Drivers;
using SharpMik.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sample = ApiTrackers.Objects.Sample;

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
        List<Objects.Sample> samples = new List<Sample>();
        List<Note> notes = new List<Note>();
        SharpMik.Module lastModule = new SharpMik.Module();
        Stream lastStreamApiContent;

        string pathSamples = @"C:\Users\Alexandre\Desktop\samples";

        string modarchiveUrl(int _id)
        {
            return "https://api.modarchive.org/downloads.php?moduleid=" + _id.ToString();
        }

        public Module(User _user, int _modarchiveIdMusic)
        {
            string url = modarchiveUrl(_modarchiveIdMusic);
            lastModule = getModuleFromAPI(url);
            setModule(lastModule, _user);
        }

        public Module(User _user, Stream _file)
        {
            lastModule = getModuleFromStream(_file);
            setModule(lastModule, _user);
        }

        public SharpMik.Module getModuleFromAPI(string _url)
        {
            try
            {
                Stream streamContentModule = WebClient.PostUrl(_url);
                return getModuleFromStream(streamContentModule);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public SharpMik.Module getModuleFromStream(Stream streamContentModule)
        {
            try
            {
                string totalPathFileModule = @"C:\Users\Alexandre\Desktop\serverTempModulesFiles\test1.Mod";
                using (Stream file = File.Create(totalPathFileModule))
                    CopyStream(streamContentModule, file);

                lastStreamApiContent = streamContentModule;

                ModDriver.LoadDriver<NoAudio>();
                ModDriver.MikMod_Init("");
                SharpMik.Module mod = ModuleLoader.Load(lastStreamApiContent, 64, 0);

                return mod;
            }
            catch
            {
                throw new TODOEXCEPTION();
            }
        }
        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        public void setModule(SharpMik.Module mod, User _user)
        {


            trackerDTO = new ModuleTrackerDTO(mod);
            samplesDTO = new ModuleSamplesDTO(mod);
            pistesDTO = new ModulePistesDTO(mod);
            cellsDTO = new ModuleCellsDTO(mod);


            tracker = trackerDTO.moduleToTracker(_user);
            pistes = pistesDTO.moduleToPistes(tracker);
            tracker.trackerContent.pistes = pistes;
            samples = samplesDTO.moduleToSamples(true, pathSamples);

            /*List<Note> subListNote = cellsDTO.moduleToCells(tracker, pistes, samples);
            foreach (Note pisteSub in subListNote)
                notes.Add(pisteSub);
            */

        }

        // getters

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
        public SharpMik.Module getSongModule()
        {
            return lastModule;
        }
        public Stream getLastApiStream()
        {
            return lastStreamApiContent;
        }




    }
}
