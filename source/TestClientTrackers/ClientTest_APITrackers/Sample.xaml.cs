using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ClientTest_APITrackers
{
    /// <summary>
    /// Logique d'interaction pour Sample.xaml
    /// </summary>
    public partial class Sample : Window
    {
        MainWindow main;
        public System.Media.SoundPlayer audioPlayer;
        public MemoryStream audioStream;

        public Sample(MainWindow m)
        {
            main = m;
            InitializeComponent();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        public void clearInterface()
        {
            tb_id.Text = "";
            tb_idLogo.Text = "";
            tb_name.Text = "";
            tb_color.Text = "";
            tb_linksample.Text = "";
        }
        public void setInterface(JObject json)
        {
            foreach (JProperty p in json.Properties())
            {
                try
                {
                    if (p.Name == "id") { tb_id.Text = p.Value.ToString(); }
                    if (p.Name == "idLogo") { tb_idLogo.Text = p.Value.ToString(); }
                    if (p.Name == "name") { tb_name.Text = p.Value.ToString(); }
                    if (p.Name == "color") { tb_color.Text = p.Value.ToString(); }
                    if (p.Name == "linkSample") { tb_linksample.Text = p.Value.ToString(); }
                }
                catch { }
            }
        }
        public JObject getFromInterface()
        {
            try
            {
                var json = new JObject{
                    new JProperty("id", tb_id.Text),
                    new JProperty("idLogo", tb_idLogo.Text),
                    new JProperty("name", tb_name.Text),
                    new JProperty("color", tb_color.Text),
                    new JProperty("linkSample", tb_linksample.Text)
                };
                return json;
            }
            catch
            {
            }
            return new JObject();
        }

        private void btn_select_click(object sender, RoutedEventArgs e)
        {
            try
            {
                setInterface(
                   (JObject)main.api().SELECT_Sample(Convert.ToInt32(tb_id.Text))
                );
            }
            catch
            {
                clearInterface();
            }
        }

        private void btn_insert_click(object sender, RoutedEventArgs e)
        {
            try
            {
                setInterface(
                   main.api().INSERT_Sample(getFromInterface())
                   );
            }
            catch
            {
                clearInterface();
            }
        }

        private void btn_delete_click(object sender, RoutedEventArgs e)
        {
            try
            {
                setInterface(
                    main.api().DELETE_Sample(Convert.ToInt32(tb_id.Text))
                );
            }
            catch
            {
                clearInterface();
            }
        }

        private void btn_update_click(object sender, RoutedEventArgs e)
        {
            try
            {
                setInterface(
                    main.api().UPDATE_Sample(getFromInterface())
                );

            }
            catch
            {
                clearInterface();
            }
        }

        private void btn_selectall_click(object sender, RoutedEventArgs e)
        {
            try
            {
                JArray json = (JArray)main.api().SELECT_Samples();

                ListWindow luw = main.getListWindow();
                luw.lv.Items.Clear();
                foreach (JToken jo in json)
                {
                    JObject jobj = (JObject)jo.ToObject((new JObject()).GetType());
                    luw.lv.Items.Add(("" + jobj).Replace("\n", ""));
                }
                 main.showListWindow(); 
            }
            catch
            {
                clearInterface();
            }
        }

        WebClient web1 = new WebClient();


        public MemoryStream loadAudio(string url)
        {
            byte[] b = getAudioFromURL(url);
            audioStream = new MemoryStream(b);
            audioPlayer = new System.Media.SoundPlayer(audioStream);

            return audioStream;
        }
        public MemoryStream playAudio_Load(string url)
        {
            //if(audioStream==null||audioStream.Length==0)
            MemoryStream m = loadAudio(url);
            audioPlayer.Play();
            return m;
        }
        public void playAudio_Pitch_Load(string url)
        {
            /*
            byte[] data = getAudioFromURL(url);

            double sampleFrequency = 11025.0;
            double multiplier = 2.0 * Math.PI / sampleFrequency;
            int volume = 20;


            // initialize the data to "flat", no change in pressure, in the middle:
            for (int i = 0; i < data.Length; i++)
                data[i] = 128;

            // Add on a change in pressure equal to A440:
            for (int i = 0; i < data.Length; i++)
                data[i] = (byte)(data[i] + volume * Math.Sin(i * multiplier * 440.0));

            // Add on a change in pressure equal to A880:

            for (int i = 0; i < data.Length; i++)
                data[i] = (byte)(data[i] + volume * Math.Sin(i * multiplier * 880.0));

            audioStream = new MemoryStream(data);
            audioPlayer = new System.Media.SoundPlayer(audioStream);

            audioPlayer.Play();
            */
        }

        public void playAudio()
        {
            audioPlayer.Play();
        }


        public void stopAudio()
        {
            audioPlayer.Stop();
        }

        public byte[] getAudioFromURL(string url)
        {
            byte[] datas;
            try { 
                datas = web1.DownloadData(url);
                return datas;
            }
            catch { }
            return Encoding.ASCII.GetBytes(new string(""));
        }

        private void btn_test_click(object sender, RoutedEventArgs e)
        {
            try {
                loadAudio(tb_linksample.Text);
                if (audioStream == null || audioStream.Length == 0)
                    tb_testt.Text = "KO";
                else
                    tb_testt.Text = "OK";
            }
            catch(Exception ex)
            {
                main.logErr(ex);
                tb_testt.Text = "KO";
            }
        }

        private void btn_play_click(object sender, RoutedEventArgs e)
        {
            try
            {
                playAudio_Load(tb_linksample.Text);


            }
            catch (Exception ex)
            {
                main.logErr(ex);
            }
        }
        private void btn_playPitch_click(object sender, RoutedEventArgs e)
        {
            try
            {

                playAudio_Pitch_Load(tb_linksample.Text);

            }
            catch (Exception ex)
            {
                main.logErr(ex.ToString());
            }
        }

        private void btn_stop_click(object sender, RoutedEventArgs e)
        {
            try
            {
                stopAudio();
            }
            catch (Exception ex)
            {
                main.logErr(ex);
            }
        }
    }
}