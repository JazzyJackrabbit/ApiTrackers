using NAudio.Dsp;
using NAudio.Wave;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientTest_APITrackers
{
    /// <summary>
    /// Logique d'interaction pour TrackerCellUserControl.xaml
    /// </summary>
    public partial class SubCellControl: UserControl
    {

        MainWindow main;
        public int idCell;
        public int position;

        public double vol;
        public double freq;

        // AUDIO PLAYER 
        // AUDIO PLAYER 
        // AUDIO PLAYER 

        public NAudio.Wave.WaveOut waveOut = new WaveOut();
        public MemoryStream audioStream;


        WebClient web1 = new WebClient();

        public MemoryStream loadAudio(string url, double _vol, double _freq)
        {
            try
            {
                byte[] datas;
                datas = web1.DownloadData(url);
                audioStream = new MemoryStream(datas);
                waveOut.Init(new NAudio.Wave.StreamMediaFoundationReader(audioStream));


                vol = _vol;
                freq = _freq;
            }
            catch { }

            return audioStream;
        }
        public MemoryStream loadAudio(string url)
        {
            try
            {
                byte[] datas;
                datas = web1.DownloadData(url);
                audioStream = new MemoryStream(datas);
                waveOut.Init(new NAudio.Wave.StreamMediaFoundationReader(audioStream));

            }
            catch { }

            return audioStream;
        }

        public void playAudio()
        {
            //if(audioStream==null||audioStream.Length==0)
            try {
                if (waveOut != null)
                {


                    /* smbpi.ShortTimeFourierTransform(audioStream, 256, 256);

                     smbpi.PitchShift(_freq, 1 )
                                        NWaves.Effects.
                    waveOut = new NAudio.Wave.WaveOut();
                    waveOut.Volume = (float)_vol;

                    waveOut.

                     audioPlayer.vol

                     If Volume = Nothing Then Wave.Volume = Volume
                     Wave.Play()


                     WindowsMediaPlayer wmp = new WindowsMediaPlayer();
                     wmp.URL = @"E:\01. IMANY - Don't Be so Shy (Filatov & Karas Remix).mp3";
                     wmp.settings.volume = 50;
                     wmp.controls.play();

                    DiscreteSignal left, right, both;
                    using (audioStream)
                    {
                        var waveFile = new WaveFile(audioStream);
                        left = waveFile[Channels.Left];
                        right = waveFile[Channels.Right];

                        left.Amplify((float)_vol);
                        right.Amplify((float)_vol);
                    }
                    NWaves.Effects.PitchShiftEffect p = new NWaves.Effects.PitchShiftEffect(_freq);
                    left = p.ApplyTo(new NWaves.Signals.DiscreteSignal(20000, left.Samples));
                    right = p.ApplyTo(new NWaves.Signals.DiscreteSignal(20000, right.Samples));

                    both = new DiscreteSignal(20000, )
                    var waveFile2 = new WaveFile();
                    waveFile2[0].
                     audioPlayer.*/
                    waveOut.Init(new NAudio.Wave.StreamMediaFoundationReader(audioStream));
                    waveOut.Stop();
                    waveOut.Volume = (float)vol;
                    waveOut.Play();
                }
            }
            catch { }
        }

        // 
        // 
        // 
        // 




        public SubCellControl(MainWindow _main, int _idCell, int _posit, string _urlSample)
        {
            InitializeComponent();
            main = _main;
            idCell = _idCell;

            lbl_idCell.Content = idCell;

            slider_key.Value = 1;
            slider_vol.Value = 1;

            position = _posit;

            grid_visible.Visibility = Visibility.Collapsed;

            loadAudio(_urlSample, 1, 1);
        }

        public SubCellControl(MainWindow _main, int _idCell, double _vol, double _freq, int _posit, string _urlSample)
        {
            InitializeComponent();
            main = _main;
            idCell = _idCell;

            lbl_idCell.Content = idCell;

            slider_key.Value = _freq;
            slider_vol.Value = _vol;

            position = _posit;

            grid_visible.Visibility = Visibility.Collapsed;

            loadAudio(_urlSample, _vol, _freq);
        }
       

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            
            if (grid_visible.Visibility != Visibility.Visible)
            {
                grid_visible.Visibility = Visibility.Visible;

                int idd = main.insertCell(main.getTrackerId_TRACKER_INTERFACE());

                lbl_idCell.Content = idCell = idd;
            }
            else
            {
                main.cellControlWindow.tb_id.Text = idCell.ToString();
                
                main.editCell(main.getTrackerId_TRACKER_INTERFACE(), idCell);

                main.openCellInterface();
            }

            
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            grid_visible.Visibility = Visibility.Collapsed;
            int idd = main.getTrackerId_TRACKER_INTERFACE();
            main.deleteCell(idd, idCell);

        }
        private void slider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            updateCell(main.getTrackerId_TRACKER_INTERFACE(), idCell, slider_key.Value, slider_vol.Value);

        }
     

        public int updateCell(int _idTracker, int _idCell, double _key, double _vol)
        {
            try
            {
                JObject json = new JObject();

                json.Add(new JProperty("volume", _vol));
                json.Add(new JProperty("frequence", _key));
                json.Add(new JProperty("idTracker", _idTracker));
                json.Add(new JProperty("id", _idCell));
                json.Add(new JProperty("idUser", main.getUserId()));

                JObject json_2 = API.UPDATE_Cell(_idTracker, json);
                main.log("UPDATED: " + json_2);
                foreach (JProperty j in json_2.Properties())
                    if (j.Name == "id") return Convert.ToInt32(j.Value);


                int idSample = Convert.ToInt32(((JObject)API.SELECT_Cell(main.getTrackerId_TRACKER_INTERFACE(), _idCell)).GetValue("idSample"));
                string url = ((JObject)API.SELECT_Sample(idSample)).GetValue("linkSample").ToString();


                loadAudio(url, _vol, _key);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                main.log(ex.ToString());
            }
            return -1;
        }

 
    }
}
