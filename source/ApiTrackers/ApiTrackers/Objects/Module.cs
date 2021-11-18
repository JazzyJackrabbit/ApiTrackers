using ApiTrackers.Exceptions;
using ApiTrackers.Objects;
using ApiTrackers.Services;
using ClientTest_APITrackers;
using Microsoft.AspNetCore.Http;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Newtonsoft.Json;
using SharpMik;
using SharpMik.Drivers;
using SharpMik.Loaders;
using SharpMik.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;
using WMPLib;

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
      
        string modarchiveUrl(int _id)
        {
            return "https://api.modarchive.org/downloads.php?moduleid=" + _id.ToString();
        }

        string localpathSamples()
        {
            return @"C:\Users\Alexandre\Desktop\samples\";
        }

        public Module(User _user, int _modarchiveIdMusic)
        {
            ModDriver.LoadDriver<NoAudio>();
            ModDriver.MikMod_Init("");

            string url = modarchiveUrl(_modarchiveIdMusic);
            setModule(getStreamFromAPI(url), _user);
        }

        public Module(User _user, Stream _file)
        {
            ModDriver.LoadDriver<NoAudio>();
            ModDriver.MikMod_Init("");

            setModule(_file, _user);
        }

        public Stream getStreamFromAPI(string _url)
        {
            try
            {
                Stream streamContentModule = WebClient.PostUrlWithoutData(_url);
                return streamContentModule;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
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
        public void setModule(Stream modStream, User _user)
        {
            samplesDTO = new ModuleSamplesDTO(modStream);
            trackerDTO = new ModuleTrackerDTO(modStream);
            pistesDTO = new ModulePistesDTO(modStream);
            cellsDTO = new ModuleCellsDTO(modStream);

            samples = samplesDTO.moduleToSamples(true, localpathSamples());
            tracker = trackerDTO.moduleToTracker(_user);
            pistes = pistesDTO.moduleToPistes(tracker);
            notes = cellsDTO.moduleToCells(tracker, samples);
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
        

    }
}
