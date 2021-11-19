using Microsoft.AspNetCore.Http;
using SharpMik;
using SharpMik.Drivers;
using SharpMik.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;

namespace ApiTrackers.Objects
{
    public class ModuleSamplesDTO
    {

        List<Sample> samples = null;
        Stream module = null;

        public ModuleSamplesDTO(Stream _module)
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

            MikMod player;
            player = new MikMod();
            player.Init<NoAudio>("temp.wav");
            SharpMik.Module m = player.LoadModule(module);

            if (m == null) return null;

            foreach (SAMPLE sample in m.samples)
            {
                try { 
                    short[] buffer = sample.buffer;

                    string sampleName = sample.samplename;
                    short panning = sample.panning;
                    short volume = sample.volume;

                    string filepathsample = "NONE";
                    string filenameonly = "NONE";

                    Sample sampleApi = new Sample();

                    if (buffer != null && _makeFiles)
                    {  
                        MemoryStream wavSample = new MemoryStream();

                        Static.WriteWavHeader(wavSample, false, 1, 16, 20000, buffer.Length);


                        for (int i = 0; i < buffer.Length; i++)
                        {
                            Int16 v = buffer[i];
                            byte[] b = BitConverter.GetBytes(v);

                            wavSample.Write(b, 0, b.Length);
                        }

                        wavSample.Position = -0;

                        filenameonly = Static.MakeValidFileName(sampleName) + ".wav";
                        filepathsample = _samplesPath + @"\" + filenameonly;

                        sampleApi.streamModule = wavSample;

                        using (var fileStream = File.Create(filepathsample))
                        {
                            wavSample.Seek(0, SeekOrigin.Begin);
                            wavSample.CopyTo(fileStream);
                        }

                    }

                    sampleApi.name = sampleName;
                    sampleApi.linkSample = filenameonly; // filepathsample;
                    sampleApi.idLogo = 50;
                    sampleApi.color = "#dddddd";

                    tempSamples.Add(sampleApi);

                }
                catch { }

            };

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
