using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientTest_APITrackers
{
    /// <summary>
    /// Logique d'interaction pour RightMusic.xaml
    /// </summary>
    public partial class RightMusic : Window
    {

        MainWindow main;
        public RightMusic(MainWindow _main)
        {
            InitializeComponent();
            main = _main;
        }
        public RightMusic(MainWindow _main, int _idUser, int _idTracker)
        {
            InitializeComponent();

            tb_idTracker.Text = _idTracker.ToString();
            tb_idUser.Text = _idUser.ToString();

            main = _main;
        }

        public void clearInterface()
        {
            //tb_idTracker.Text = "";
            //tb_idUser.Text = "";
            tb_right.Text = "";
        }
        public void setInterface(JObject json)
        {
            foreach (JProperty p in json.Properties())
            {
                try
                {
                    if (p.Name == "idTracker") { tb_idTracker.Text = p.Value.ToString(); }
                    if (p.Name == "idUser") { tb_idUser.Text = p.Value.ToString(); }
                    if (p.Name == "right") { tb_right.Text = p.Value.ToString(); }
                }
                catch { }
            }
        }
        public JObject getFromInterface()
        {
            try
            {
                var json = new JObject{
                    new JProperty("idTracker", tb_idTracker.Text),
                    new JProperty("idUser", tb_idUser.Text),
                    new JProperty("right", tb_right.Text),

                };
                return json;
            }
            catch
            {
            }
            return new JObject();
        }


        private void SelectClick(object sender, RoutedEventArgs e)
        {
            try
            {
                setInterface((JObject)
                    main.api().SELECT_RightMusic(
                        Convert.ToInt32(tb_idUser.Text), 
                        Convert.ToInt32(tb_idTracker.Text) ));
            }
            catch { clearInterface(); }
        }

        private void InsertClick(object sender, RoutedEventArgs e)
        {
            try
            {
                setInterface(
                main.api().INSERT_RightMusic(getFromInterface()));
            }
            catch (Exception xe) { main.logErr(xe.ToString()); clearInterface(); }
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                setInterface(
                main.api().UPDATE_RightMusic(getFromInterface())
            );
            }
            catch (Exception x)
            {
                main.logErr(x.ToString());
                clearInterface();
            }
        }

        private void SelectAllByTrackerClick(object sender, RoutedEventArgs e)
        {
            try
            {
                JArray json = (JArray)main.api().SELECT_RightMusics_byTracker(Convert.ToInt32(tb_idTracker.Text));

                ListWindow luw = main.getListWindow();
                luw.lv.Children.Clear();
                foreach (JToken jo in json)
                {
                    JObject jobj = (JObject)jo.ToObject(new JObject().GetType());
                    luw.addOnList(jobj);
                } 
                 main.showListWindow();
            }
            catch { clearInterface(); }
        }

        private void SelectAllByUserClick(object sender, RoutedEventArgs e)
        {
            try
            {
                JArray json = (JArray)main.api().SELECT_RightMusics_byUser(Convert.ToInt32(tb_idUser.Text));

                ListWindow luw = main.getListWindow();
                luw.lv.Children.Clear();
                foreach (JToken jo in json)
                {
                    JObject jobj = (JObject)jo.ToObject((new JObject()).GetType());
                    luw.addOnList(jobj);
                }
                 main.showListWindow();
            }
            catch { clearInterface(); }
        }

        private void btn_idLeftUser_click(object sender, RoutedEventArgs e)
        {
            try
            {
                tb_idUser.Text = "" + (Convert.ToInt32(tb_idUser.Text) - 1);
                SelectClick(sender, e);
            }
            catch { }
        }

        private void btn_idLeftTracker_click(object sender, RoutedEventArgs e)
        {
            try
            {
                tb_idTracker.Text = "" + (Convert.ToInt32(tb_idTracker.Text) - 1);
                SelectClick(sender, e);
            }
            catch { }
        }

        private void btn_idRightUser_click(object sender, RoutedEventArgs e)
        {
            try
            {
                tb_idUser.Text = "" + (Convert.ToInt32(tb_idUser.Text) + 1);
                SelectClick(sender, e);
            }
            catch { }
        }

        private void btn_idRightTracker_click(object sender, RoutedEventArgs e)
        {
            try
            {
                tb_idTracker.Text = "" + (Convert.ToInt32(tb_idTracker.Text) + 1);
                SelectClick(sender, e);
            }
            catch {  }
        }
    }
}
