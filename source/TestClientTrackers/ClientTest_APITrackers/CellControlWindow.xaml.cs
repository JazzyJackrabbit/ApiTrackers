﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
    /// Logique d'interaction pour CellControlWindow.xaml
    /// </summary>
    public partial class CellControlWindow : Window
    {

        MainWindow main; 

        public CellControlWindow(MainWindow _main)
        {
            main = _main;
            InitializeComponent();
        }

        public int getIdTrackerByINTERFACE()
        {
            return Convert.ToInt32(tb_idTracker.Text);
        }
        public int getIdCellByINTERFACE()
        {
            return Convert.ToInt32(tb_id.Text);
        }
        public int setIdTrackerOnINTERFACE(int _id)
        {
            return Convert.ToInt32(tb_idTracker.Text = _id.ToString());
        }
        public int setIdCellOnINTERFACE(int _id)
        {
            return Convert.ToInt32(tb_id.Text = _id.ToString());
        }

        private void setOnInterface(JObject json)
        {
            
                foreach (JProperty p in json.Properties())
                {
                    try
                    {
                        if (p.Name == "id") tb_id.Text = p.Value.ToString();
                        if (p.Name == "frequence")
                        {
                            tb_freq.Text = p.Value.ToString().Replace(",", ".");
                            double d = Convert.ToDouble(p.Value.ToString()); 
                            sl_freq.Value = d;
                        }
                         if (p.Name == "position") tb_position.Text = p.Value.ToString();
                        if (p.Name == "volume")
                        {
                            tb_vol.Text = p.Value.ToString().Replace(",", ".");
                            double d = Convert.ToDouble(p.Value.ToString());
                            sl_vol.Value = d;
                        }
                        if (p.Name == "key") tb_Key.Text = p.Value.ToString();
                        if (p.Name == "idTracker") tb_idTracker.Text = p.Value.ToString();
                        if (p.Name == "idSample") tb_idSample.Text = p.Value.ToString();
                        if (p.Name == "idEffect") tb_idEffect.Text = p.Value.ToString();
                        if (p.Name == "idPiste") tb_idPiste.Text = p.Value.ToString();
                    }
                    catch (Exception ex){ main.log(ex); }
                }
           
        }
        private JObject getFromInterface()
        {
            var json = new JObject{
                new JProperty("id", tb_id.Text),
                new JProperty("frequence", tb_freq.Text.Replace(",",".")),
                new JProperty("position", tb_position.Text),
                new JProperty("volume", tb_vol.Text.Replace(",",".")),
                new JProperty("key", tb_Key.Text),
                new JProperty("idTracker", tb_idTracker.Text),
                new JProperty("idSample", tb_idSample.Text),
                new JProperty("idEffect", tb_idEffect.Text),
                new JProperty("idPiste", tb_idPiste.Text),
                new JProperty("idUser", main.getUserId())
            };
            return json;
        }

        public void selectAndDisplay(int _idCell)
        {
            try
            {
                int idCell = _idCell;

                JContainer json = API.SELECT_Cell(main.getTrackerId_TRACKER_INTERFACE(), idCell);
                try
                {
                    setOnInterface((JObject)json);
                }
                catch
                {
                    try
                    {
                        foreach (JObject o in json)
                        {
                            setOnInterface(o);
                            return;
                        }
                    }
                    catch { }
                }

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                main.log(ex.ToString());
            }
        }

        private void btn_SELECT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idCell = Convert.ToInt32(tb_id.Text);
                selectAndDisplay(idCell);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                main.log(ex.ToString());
            }
           
        }

        private void btn_INSERT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idCell = Convert.ToInt32(tb_id.Text);

                JObject json = getFromInterface();
                JObject json_2 = API.INSERT_Cell(main.getTrackerId_TRACKER_INTERFACE(), json);
                setOnInterface(json_2);
                main.log("INSERED: " + json);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                main.log(ex.ToString());
            }
        }

        private void btn_DELETE_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idCell = Convert.ToInt32(tb_id.Text);

                JObject json = API.DELETE_Cell(main.getTrackerId_TRACKER_INTERFACE(), idCell);
                main.log("DELETED: " + json);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                main.log(ex.ToString());
            }
        }

        private void btn_UPDATE_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idCell = Convert.ToInt32(tb_id.Text);

                JObject json = getFromInterface();
                JObject json_2 = API.UPDATE_Cell(main.getTrackerId_TRACKER_INTERFACE(), json);
                setOnInterface(json_2);
                main.log("UPDATED: " + json);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                main.log(ex.ToString());
            }
        }

        private void sl_vol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tb_vol.Text = sl_vol.Value.ToString();
        }

        private void sl_freq_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tb_freq.Text = sl_freq.Value.ToString();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            //e.Cancel = true;
            //this.Visibility = Visibility.Collapsed;
            //e.Cancel = true;
            //this.Visibility = Visibility.Collapsed;
        }

         private void btn_SELECT_ALL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JArray json = (JArray)API.SELECT_Cells(main.getTrackerId_CELL_INTERFACE());

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
            }
        }
    }
}
