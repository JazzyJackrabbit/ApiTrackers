using Microsoft.AspNetCore.Http;
using SharpMod;
using SharpMod.Song;
using System;
using System.Collections.Generic;
using System.IO;

namespace ApiTrackers.Objects
{
    public class ModuleSamplesDTO
    {

        List<Sample> samples = null;
        SongModule module = null;

        public ModuleSamplesDTO(IFormFile _fileContent)
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
        public List<Sample> getSamples()
        {
            return samples;
        }

        public List<Sample> moduleToSamples(bool _makeFiles = false, string _samplesPath = "")
        {
            List<Sample> tempSamples = new List<Sample>();

            foreach (Instrument instrument in module.Instruments)
            {
                int posISample = 0;
                foreach (SharpMod.Song.Sample sample in instrument.Samples)
                {
                    Byte[] sampleBytes = sample.SampleBytes;
                    string sampleName = sample.SampleName;
                    short panning = sample.Panning;
                    short volume = sample.Volume;

                    Tuple<string, string> fileTuple = filepath(_samplesPath, sampleName);
                    string pathSample = fileTuple.Item1;
                    string filenameSample = fileTuple.Item2;

                    if (_makeFiles)
                        if (!saveDirectorySample(sampleBytes, pathSample)) { 
                            pathSample = "Unknown";
                        }
                    else
                        pathSample = "Unknown";
                    
                    Sample sampl = new Sample();
                    sampl.name = sampleName;
                    sampl.idLogo = 100; // module
                    sampl.color = "#dddddd";
                    sampl.linkSample = pathSample;
                    sampl.localInstrumentId = posISample;

                    samples.Add(sampl);
                    posISample++;
                }
            }

            return tempSamples;

        }

        public bool saveDirectorySample(Byte[] _sampleBytes, string _pathSample)
        {
            if (_sampleBytes != null && _sampleBytes.Length > 1)
            {
                MemoryStream ms = new MemoryStream(_sampleBytes);
                FileStream file = new FileStream(_pathSample, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                file.Close();
                ms.Close();

                return true;
            }
            return false;
        }

        public string filename(string _sampleName)
        {
            if (_sampleName == null || _sampleName == "") return null;

            string filename = _sampleName;

            // remove special caracters
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                filename = filename.Replace(c, '_');
            }
            String timeStamp = (DateTime.Now).ToString("__yyyy_MM_dd__HH_mm_ss__ffff");
            filename += "-" + timeStamp + ".sampleFT1";

            return filename;
        }
        public Tuple<string, string> filepath(string _samplesPath, string _sampleName)
        {
            string filename = this.filename(_sampleName);

            if(filename != null && filename != "")
            {
                return new Tuple<string, string>(_samplesPath + @"\" + filename, filename);
            }

            return null;
        }
    }

}
