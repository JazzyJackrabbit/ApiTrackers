using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
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
    /// Logique d'interaction pour TrackerControlWindow.xaml
    /// </summary>
    public partial class Tracker : Window
    {



        MainWindow main;
        ListView lv;

        public bool playing;
        public int positionPlaying = -1;

        Timer timer = new Timer();

        public Tracker(MainWindow _main)
        {
            InitializeComponent();
            main = _main;

            lv = new ListView();
            grid_content.Children.Add(lv); // lv);

            timer.Elapsed += new ElapsedEventHandler(TimerEventProcessor);
  


            //timer.Start();
        }

        private double changeBPM()
        {
            try
            {
                int bpm = Convert.ToInt32(tb_bpm.Text);
                TimeSpan ts = BpmExtensions.ToInterval(bpm);
                return timer.Interval = ts.TotalMilliseconds;
            }
            catch { }
            return timer.Interval;
        }
        private int playNote(int _position, Brush b)
        {
            try
            {
                foreach(TrackerRowUserControl truc in main.trackerCtrlWindow.lv.Items)
                {
                    try
                    {
                        object cell = null;
                        if (_position < truc.listbox_notes.Items.Count)
                            cell = truc.listbox_notes.Items[_position];
                        else
                        {
                            positionPlaying = -1;

                            if (b == Brushes.DarkBlue)
                                main.trackerCtrlWindow.brush = Brushes.Green;

                            if (b == Brushes.DarkRed)
                                main.trackerCtrlWindow.brush = Brushes.DarkBlue;

                            if (main.trackerCtrlWindow.brush == Brushes.Green)
                                main.trackerCtrlWindow.brush = Brushes.DarkRed;



                        }
                        if (cell != null)
                        {
                            if (cell.GetType().Name == "SubCellControl")
                            {
                                SubCellControl cellSub = (SubCellControl)cell;
                                cellSub.playAudio();

                                cellSub.grid_background.Background = b;

                               
                            }
                            if (cell.GetType().Name == "SubCellEmptyControl")
                            {
                                SubCellEmptyControl cellSub = (SubCellEmptyControl)cell;

                                cellSub.grid_background.Background = b;

                                
                            }
                        }
                    }
                    catch { }

                    // truc.listbox_notes.Items.Clear();
                    
                }

            }
            catch { }

            return _position;
        }

        private void TimerEventProcessor(object sender, EventArgs e)
        {
            /*if (playing) { 
                positionPlaying++;
                playNote(positionPlaying);
            }
            changeBPM();*/
        }

        private void setOnInterface(JObject json)
        {

            foreach (JProperty p in json.Properties())
            {
                try
                {
                    if (p.Name == "metadata")
                    {
                        JToken jsonmeta = p.Value;
                        foreach (JProperty p2 in ((JObject)jsonmeta).Properties())
                        {
                            if (p2.Name == "artist") tb_artist.Text = p2.Value.ToString();
                            if (p2.Name == "title") tb_title.Text = p2.Value.ToString();
                            if (p2.Name == "comments") tb_comments.Text = p2.Value.ToString();
                            if (p2.Name == "coprightInformations") { };
                        }
                    }
                    if (p.Name == "content")
                    {
                        JToken jsonmeta = p.Value;
                        foreach (JProperty p2 in ((JObject)jsonmeta).Properties())
                        {
                            if (p2.Name == "bpm") tb_bpm.Text = p2.Value.ToString();
                            if (p2.Name == "notes") {

                                ((TrackerRowUserControl)main.trackerCtrlWindow.lv.Items[0])
                                    .listbox_notes.Items.Clear();

                                JToken jsonCells = p2.Value;
                                if (jsonCells != null && jsonCells.HasValues)
                                {
                                    //      main.trackerCtrlWindow.lv.ItemsTrackerRowUserControl _tr = (TrackerRowUserControl)main.trackerCtrlWindow.lv.Items[0];
                                    //_tr.listbox_notes.Items.Clear();
                                    /*for (int i = 0; i < 16; i++)
                                        _tr.listbox_notes.Items.Add( _tr.makeNoteOnInterfaceOnly(
                                                      Convert.ToInt32(-1),
                                                      0,
                                                      0,
                                                      false,
                                                      i
                                                  )
                                            );*/
                                    //for (int i = 0; i < 16; i++)


                                    foreach (JObject p3 in ((JArray)jsonCells))
                                    {
                                        string volstr = p3.GetValue("volume").ToString();
                                        string keystr = p3.GetValue("frequence").ToString();
                                        int positElement = Convert.ToInt32(p3.GetValue("position").ToString());
                                        
                                        int idSample = Convert.ToInt32(p3.GetValue("idSample").ToString());
                                        string urlSample = "";
                                        try
                                        {
                                            // test
                                            urlSample = ((JObject)main.api().SELECT_Sample(idSample)).GetValue("linkSample").ToString();
                                        }
                                        catch { }


                                        SubCellControl newT = new SubCellControl(main,
                                                         Convert.ToInt32(p3.GetValue("id")),
                                                         Double.Parse(volstr),
                                                         Double.Parse(keystr),
                                                         positElement,
                                                         urlSample
                                                      );

                                        newT.grid_visible.Visibility = Visibility.Visible;

                                        //newT.position = positFind;
                                        ((TrackerRowUserControl)main.trackerCtrlWindow.lv.Items[0])
                                            .listbox_notes.Items.Add(newT);



                                    };
                             
                                };
                            };
                        }
                    }
                    if (p.Name == "id") tb_idTracker.Text = p.Value.ToString();
                    if (p.Name == "idUser")
                        lbl_infos.Content = "idUser: " + p.Value;
                }
                catch (Exception ex) { main.logErr(ex); }
            }

        }

        private JObject getFromInterface()
        {
            var json = new JObject{
                new JProperty("id", tb_idTracker.Text),
                new JProperty("idUser", main.getUserId()),
                new JProperty("artist", tb_artist.Text),
                new JProperty("title", tb_title.Text),
                new JProperty("bpm", tb_bpm.Text),
                new JProperty("comments", tb_comments.Text),
                new JProperty("coprightInformations", "")
            };

            return json;
        }
        private void clearInterface()
        {

            //tb_idTracker.Text
            tb_artist.Text = "";
            tb_title.Text = "";
            tb_bpm.Text = "";
            tb_comments.Text = "";


        }

        public void openInterface()
        {
            main.cellControlWindow.Show();
        }


        public void openInterfaceAndDisplaySelect(int _idTracker, int idCell)
        {
            JObject json = new JObject();
            json.Add(new JProperty("id", idCell));
            json.Add(new JProperty("idTracker", _idTracker));

            JContainer json2 = main.api().SELECT_Tracker(_idTracker, main.getTrackerId_TRACKER_INTERFACE());

            openInterface();
           // InvalidateVisual();
           // UpdateLayout();

            setOnInterface( (JObject)json2 );
            JObject check = getFromInterface();
        }

        internal int insertCell()
        {
            return insertCell(main.getTrackerId_TRACKER_INTERFACE());
        }

        internal int insertCell(int _idTracker /*, int idCell*/)
        {
            try
            {

                int idSampleDefault = 1;

                JObject json = new JObject();

                json.Add(new JProperty("volume", 1));

                json.Add(new JProperty("frequence", 1));

                //json.Add(new JProperty("id", idCell));
                json.Add(new JProperty("idTracker", _idTracker));
                json.Add(new JProperty("idSample", idSampleDefault));
                json.Add(new JProperty("idUser", main.getUserId()));
                JObject json_2 = main.api().INSERT_Cell(main.getTrackerId_TRACKER_INTERFACE(), json);
                foreach (JProperty j in json_2.Properties())
                    if (j.Name == "id") return Convert.ToInt32(j.Value);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                main.logErr(ex.ToString());
            }
            return -1;
        }

  
        internal void insertCellAndOpenWindow()
        {
            insertCellAndOpenWindow(main.getTrackerId_TRACKER_INTERFACE());
        }

        internal void insertCellAndOpenWindow(int _idTracker)
        {
            try
            {
                int idC = insertCell();
                openInterfaceAndDisplaySelect(_idTracker, idC);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                main.logErr(ex.ToString());
            }
        }

        internal void deleteCell(int idCell)
        {
            deleteCell(main.getTrackerId_TRACKER_INTERFACE(), idCell);
        }
        internal void deleteCell(int _idtracker, int idCell)
        {
            try
            {
                JObject json = new JObject();
                json.Add(new JProperty("id", idCell));
                json = main.api().DELETE_Cell(main.getTrackerId_TRACKER_INTERFACE(), _idtracker);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                main.logErr(ex.ToString());
            }
        }

      /*  internal void editCell(int idCell)
        {
            // select + open interface
            try
            {
                openInterfaceAndDisplaySelect(main.getTrackerId_TRACKER_INTERFACE(), idCell);

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                main.log(ex.ToString());
            }
        }*/
        internal void editCell(int _idTracker, int idCell)
        {
            // select + open interface
            try
            {
                main.cellControlWindow.selectAndDisplay(idCell);

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                main.logErr(ex.ToString()); clearInterface();
            }
        }


        private void btn_SELECT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idTracker = Convert.ToInt32(tb_idTracker.Text);

                JContainer json = main.api().SELECT_Tracker(idTracker, main.getTrackerId_TRACKER_INTERFACE());
                try
                { // object

                    main.trackerCtrlWindow.lv.Items.Clear();
                    addPiste();
                    setOnInterface((JObject)json);
                }
                catch
                {
                    try
                    { // array
                        main.trackerCtrlWindow.lv.Items.Clear();
                        addPiste();
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
                main.logErr(ex.ToString()); clearInterface();
            }
        }

        private void btn_INSERT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idTr = Convert.ToInt32(tb_idTracker.Text);

                JObject json = getFromInterface();
                JObject json_2 = (JObject)main.api().INSERT_Tracker(main.getTrackerId_TRACKER_INTERFACE(), json);
                setOnInterface(json_2);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                main.logErr(ex.ToString()); clearInterface();
            }
        }

        private void btn_DELETE_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idTr = Convert.ToInt32(tb_idTracker.Text);

                JObject json = main.api().DELETE_Tracker(main.getTrackerId_TRACKER_INTERFACE(), idTr);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                main.logErr(ex.ToString()); clearInterface();
            }
        }

        private void btn_UPDATE_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idTr = Convert.ToInt32(tb_idTracker.Text);

                JObject json = getFromInterface();
                
                //main.trackerCtrlWindow.lv.Items.Clear();
                JObject json_2 = main.api().UPDATE_Tracker(main.getTrackerId_TRACKER_INTERFACE(), json);


                setOnInterface(json_2);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                main.logErr(ex.ToString()); clearInterface();
            }
        }
        public class DataGridRowElement
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }


        public void addPiste()
        {
            int _idPiste = lv.Items.Count;
            UserControl childRow = new TrackerRowUserControl(main, _idPiste);
            childRow.Width = grid_content.ActualWidth - 20;
            childRow.Height = 285;

            lv.Items.Add(childRow);
        }
        private void btn_ADD_Click(object sender, RoutedEventArgs e)
        {

            addPiste();
        }

        private void btn_LESS_Click(object sender, RoutedEventArgs e)
        {
            if (lv.Items.Count > 0)
                lv.Items.RemoveAt(lv.Items.Count - 1);
        }

        public Brush brush = Brushes.DarkBlue;
        private void btn_PLAY_Click(object sender, RoutedEventArgs e)
        {
            playing = true;

            if (playing)
            {
                positionPlaying++;
                playNote(positionPlaying, brush);
            }
            changeBPM();
        }

        private void btn_PAUSE_Click(object sender, RoutedEventArgs e)
        {
            playing = false;
        }

        private void btn_STOP_Click(object sender, RoutedEventArgs e)
        {
            playing = false;
            positionPlaying = -1;

            try
            {
                foreach (TrackerRowUserControl truc in main.trackerCtrlWindow.lv.Items)
                {
                    try
                    {
                        foreach (object truc2 in truc.listbox_notes.Items)
                        {
                            if (truc2 != null)
                            {
                                if (truc2.GetType().Name == "SubCellControl")
                                {
                                    SubCellControl cellSub = (SubCellControl)truc2;

                                    cellSub.grid_background.Background = Brushes.Black;
                                }
                                if (truc2.GetType().Name == "SubCellEmptyControl")
                                {
                                    SubCellEmptyControl cellSub = (SubCellEmptyControl)truc2;

                                    cellSub.grid_background.Background = Brushes.Black;
                                }
                            }


                        }
                        
                    }
                    catch { }

                }

            }
            catch { }
        }



        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void btn_SELECT_ALL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JArray json = (JArray)main.api().SELECT_Trackers(main.getUserId());

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

        private void btn_SELECT_ALL_CELLS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JArray json = (JArray)main.api(). SELECT_Cells(main.getTrackerId_TRACKER_INTERFACE());

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

        private void btn_RightMusics_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (new RightMusic(main, main.getUserId(), main.getTrackerId_TRACKER_INTERFACE())).Show();
            }
            catch
            {
                (new RightMusic(main)).Show();
            }
        }

        private void btn_SELECT_LEFT_ID_Click(object sender, RoutedEventArgs e)
        {
            try { 
                tb_idTracker.Text = "" + (Convert.ToInt32(tb_idTracker.Text) - 1);
                btn_SELECT_Click(sender, e);
            }
            catch { 
                clearInterface(); 
            }
        }

        private void btn_SELECT_RIGHT_ID_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tb_idTracker.Text = "" + (Convert.ToInt32(tb_idTracker.Text) + 1);
                btn_SELECT_Click(sender, e);
            }
            catch {            
                clearInterface();
            }
        }
    }
}
