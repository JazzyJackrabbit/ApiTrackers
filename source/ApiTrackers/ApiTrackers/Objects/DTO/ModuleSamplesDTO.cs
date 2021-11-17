using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace ApiTrackers.Objects
{
    public class ModuleSamplesDTO
    {

        List<Sample> samples = null;
        SharpMik.Module module = null;

        public ModuleSamplesDTO(SharpMik.Module _module)
        {
            module = _module; 
        }
        public List<Sample> getSamples()
        {
            return samples;
        }

        public List<Sample> moduleToSamples(bool _makeFiles = false, string _samplesPath = "")
        {
            List<Sample> tempSamples = new List<Sample>();

            if (module.instruments == null) return new List<Sample>();
            foreach (SharpMik.INSTRUMENT instru in module.instruments)
            {
                int posISample = 0;
                  foreach (SharpMik.SAMPLE sample in module.samples)
                    {
                    foreach (ushort sampleN in instru.samplenumber)
                        if (sampleN == posISample)
                        {
                            Byte[] sampleBytes = instru.samplenote;
                            string sampleName = sample.samplename;
                            short panning = sample.panning;
                            short volume = sample.volume;

                            Tuple<string, string> fileTuple = filepath(_samplesPath, sampleName);
                            string pathSample = fileTuple.Item1;
                            string filenameSample = fileTuple.Item2;

                            if (_makeFiles)
                                if (!saveDirectorySample(sampleBytes, pathSample))
                                {
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
