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
    public partial class SampleAlias : Window
    {
        MainWindow main;
        public System.Media.SoundPlayer audioPlayer;
        public MemoryStream audioStream;

        public SampleAlias(MainWindow m)
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
            //tb_idUser.Text = "";
            tb_idLogo.Text = "";
            tb_name.Text = "";
            tb_color.Text = "";
            //tb_idSample.Text = "";
        }
        public void setInterface(JObject json)
        {
            foreach (JProperty p in json.Properties())
            {
                try
                {
                    if (p.Name == "idUser") { tb_idUser.Text = p.Value.ToString(); }
                    if (p.Name == "idLogo") { tb_idLogo.Text = p.Value.ToString(); }
                    if (p.Name == "name") { tb_name.Text = p.Value.ToString(); }
                    if (p.Name == "color") { tb_color.Text = p.Value.ToString(); }
                    if (p.Name == "idSample") { tb_idSample.Text = p.Value.ToString(); }
                }
                catch { }
            }
        }
        public JObject getFromInterface()
        {
            try
            {
                var json = new JObject{
                    new JProperty("idUser", tb_idUser.Text),
                    new JProperty("idLogo", tb_idLogo.Text),
                    new JProperty("name", tb_name.Text),
                    new JProperty("color", tb_color.Text),
                    new JProperty("idSample", tb_idSample.Text)
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
                   (JObject)main.api().SELECT_SampleAlias(Convert.ToInt32(tb_idUser.Text), Convert.ToInt32(tb_idSample.Text))
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
                   main.api().INSERT_SampleAlias(getFromInterface())
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
                    main.api().DELETE_SampleAlias(Convert.ToInt32(tb_idUser.Text), Convert.ToInt32(tb_idSample.Text))
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
                    main.api().UPDATE_SampleAlias(getFromInterface())
                );

            }
            catch
            {
                clearInterface();
            }
        }


        private void btn_selectallbyUser_click(object sender, RoutedEventArgs e)
        {
            try
            {
                JArray json = (JArray)main.api().SELECT_SamplesAlias(Convert.ToInt32(tb_idUser.Text));

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


        private void btn_test_click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btn_play_click(object sender, RoutedEventArgs e)
        {
            
        }
        private void btn_playPitch_click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btn_stop_click(object sender, RoutedEventArgs e)
        {
           
        }


        private void btn_goUserView_click(object sender, RoutedEventArgs e)
        {
            main.goUserView();
        }

        private void btn_goSampleView_click(object sender, RoutedEventArgs e)
        {
            main.goSampleView();
        }

        private void btn_LEFT_ID_SAMPLE_click(object sender, RoutedEventArgs e)
        {
            tb_idSample.Text = "" + (Convert.ToInt32(tb_idSample.Text) - 1);
            btn_select_click(sender, e);
        }

        private void btn_RIGHT_ID_SAMPLE_click(object sender, RoutedEventArgs e)
        {
            tb_idSample.Text = "" + (Convert.ToInt32(tb_idSample.Text) + 1);
            btn_select_click(sender, e);
        }
        private void btn_LEFT_ID_USER_click(object sender, RoutedEventArgs e)
        {
            try { 
                tb_idUser.Text = "" + (Convert.ToInt32(tb_idUser.Text) - 1);
                btn_select_click(sender, e);
            }
            catch { }
        }

        private void btn_RIGHT_ID_USER_click(object sender, RoutedEventArgs e)
        {
            try
            {
                tb_idUser.Text = "" + (Convert.ToInt32(tb_idUser.Text) + 1);
                btn_select_click(sender, e);
            }
            catch { }
        }
    }
}