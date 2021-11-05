using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientTest_APITrackers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Tracker trackerCtrlWindow; // tracker
        public CellControlWindow cellControlWindow; // cell
        public User__Window userWindow;  // user
        public Sample sampleWindow; // sample
        public RightMusic rightMusicWindow; // sample
        public ListWindow listWindow;
        public SampleAlias sampleAliasWindow;

        public Grid Grid_trackerCtrlWindow; // tracker
        public Grid Grid_cellControlWindow; // cell
        public Grid Grid_userWindow;  // user
        public Grid Grid_sampleWindow; // sample
        public Grid Grid_rightMusicWindow; // sample
        public Grid Grid_listWindow; // sample
        public Grid Grid_sampleAliasWindow; // sample

        public MainWindow()
        {
            InitializeComponent();
            this.Visibility = Visibility.Visible;

            userWindow = new User__Window(this);
            trackerCtrlWindow = new Tracker(this);
            cellControlWindow = new CellControlWindow(this);
            sampleWindow = new Sample(this);
            rightMusicWindow = new RightMusic(this);
            listWindow = new ListWindow();
            sampleAliasWindow = new SampleAlias(this);

            Grid child = Grid_userWindow = (Grid)userWindow.grid_content.Children[0];
            userWindow.grid_content.Children.Clear();
            grid_content.Children.Add(child);

            child = Grid_trackerCtrlWindow = (Grid)trackerCtrlWindow.grid_content_Parent.Children[0];
            trackerCtrlWindow.grid_content_Parent.Children.Clear();
            grid_content.Children.Add(child);

            child = Grid_cellControlWindow = (Grid)cellControlWindow.grid_content.Children[0];
            cellControlWindow.grid_content.Children.Clear();
            grid_content.Children.Add(child);

            child = Grid_rightMusicWindow = (Grid)rightMusicWindow.grid_content.Children[0];
            rightMusicWindow.grid_content.Children.Clear();
            grid_content.Children.Add(child);

            child = Grid_sampleWindow = (Grid)sampleWindow.grid_content.Children[0];
            sampleWindow.grid_content.Children.Clear();
            grid_content.Children.Add(child);

            child = Grid_listWindow = (Grid)listWindow.grid_content_Parent.Children[0];
            listWindow.grid_content_Parent.Children.Clear();
            grid_content.Children.Add(child);

            child = Grid_sampleAliasWindow = (Grid)sampleAliasWindow.grid_content.Children[0];
            sampleAliasWindow.grid_content.Children.Clear();
            grid_content.Children.Add(child);


            invisibilityGridsAndButtons();
        }

        public void invisibilityGridsAndButtons()
        {
            Grid_trackerCtrlWindow.Visibility = Visibility.Hidden;
            Grid_cellControlWindow.Visibility = Visibility.Hidden;
            Grid_userWindow.Visibility = Visibility.Hidden;
            Grid_sampleWindow.Visibility = Visibility.Hidden;
            Grid_rightMusicWindow.Visibility = Visibility.Hidden;
            Grid_listWindow.Visibility = Visibility.Hidden;
            Grid_sampleAliasWindow.Visibility = Visibility.Hidden;

            Brush b1 = new SolidColorBrush(Color.FromRgb((byte)(48), (byte)(60), (byte)(150)));
            btn_cells.Background = b1;
            btn_rightMusics.Background = b1;
            btn_samples.Background = b1;
            btn_samplesAlias.Background = b1;
            btn_trackers.Background = b1;
            btn_users.Background = b1;

        }
        Brush b2 = new SolidColorBrush(Color.FromRgb((byte)(140), (byte)(50), (byte)(50)));

        private void UsersClick(object sender, RoutedEventArgs e)
        {
            goUserView();
        }

        public void goUserView()
        {
            invisibilityGridsAndButtons();
            Grid_userWindow.Visibility = Visibility.Visible; //
            btn_users.Background = b2;
        }

        private void TrackersClick(object sender, RoutedEventArgs e)
        {
            invisibilityGridsAndButtons();
            Grid_trackerCtrlWindow.Visibility = Visibility.Visible; //
            btn_trackers.Background = b2;
        }

        private void CellsClick(object sender, RoutedEventArgs e)
        {
            invisibilityGridsAndButtons();
            Grid_cellControlWindow.Visibility = Visibility.Visible; //
            btn_cells.Background = b2;
        }
        public void openCellInterface()
        {
            invisibilityGridsAndButtons();
            Grid_cellControlWindow.Visibility = Visibility.Visible; //
            btn_cells.Background = b2;

            //cellControlWindow.selectAndDisplay(Main
        }

        private void RightMusicsClick(object sender, RoutedEventArgs e)
        {
            invisibilityGridsAndButtons();
            Grid_rightMusicWindow.Visibility = Visibility.Visible; //
            btn_rightMusics.Background = b2;

        }

        private void SamplesClick(object sender, RoutedEventArgs e)
        {
            goSampleView();
        }

        public void goSampleView()
        {
            invisibilityGridsAndButtons();
            Grid_sampleWindow.Visibility = Visibility.Visible; //
            btn_samples.Background = b2;
        }

        private void SamplesAliasClick(object sender, RoutedEventArgs e)
        {
            invisibilityGridsAndButtons();
            Grid_sampleAliasWindow.Visibility = Visibility.Visible; //
            btn_samplesAlias.Background = b2;
        }

        public void showListWindow()
        {
            invisibilityGridsAndButtons();
            Grid_listWindow.Visibility = Visibility.Visible; //

        }
        internal ListWindow getListWindow()
        {
            return listWindow;
        }

        public string logErr(string _text)
        {
            tb_console.Text = "XX " + DateTime.Now.ToString() + " XX  ERR/WAR : " + _text + "\n\n\n" + tb_console.Text;
            return _text;
        }
        public string logErr(object _text)
        {
            tb_console.Text = "XX " + DateTime.Now.ToString() + " XX  ERR/WAR : " + _text.ToString() + "\n\n\n" + tb_console.Text;
            return _text.ToString();
        }
        public string logSuite(string _text)
        {
            tb_console.Text = "[[ " + DateTime.Now.ToString() + " ]]  " + _text + "\n\n\n" + tb_console.Text;
            return _text;
        }
        public object logSuite(object _obj)
        {
            tb_console.Text = "[[ " + DateTime.Now.ToString() + " ]]  " + _obj.ToString() + "\n\n\n" + tb_console.Text;
            return _obj;
        }
        public string logLine(string _text)
        {
            tb_console.Text = _text + "\n\n\n" + tb_console.Text;
            return _text;
        }
        public string logLine(object _text)
        {
            tb_console.Text = _text.ToString() + "\n\n\n" + tb_console.Text;
            return _text.ToString();
        }
        internal void editCell(int _idTracker, int idCell)
        {
            trackerCtrlWindow.editCell(_idTracker, idCell);
        }
        internal void deleteCell(int _idTracker, int idCell)
        {
            trackerCtrlWindow.deleteCell(_idTracker, idCell);
        }
        internal int insertCell(int _idTracker)
        {
            return trackerCtrlWindow.insertCell(_idTracker);
        }
        internal void editCell(int idCell)
        {
            trackerCtrlWindow.editCell(getTrackerId_TRACKER_INTERFACE(), idCell);
        }
        internal void deleteCell(int idCell)
        {
            trackerCtrlWindow.deleteCell(getTrackerId_TRACKER_INTERFACE(), idCell);
        }
        internal void insertCell()
        {
            trackerCtrlWindow.insertCell(getTrackerId_TRACKER_INTERFACE());
        }

        public int getTrackerId_TRACKER_INTERFACE()
        {
            try
            {
                int idd = Convert.ToInt32(trackerCtrlWindow.tb_idTracker.Text);
                return idd;
            }
            catch
            {
                return -1;
            }
        }
        public int getTrackerId_CELL_INTERFACE()
        {
            try
            {
                int idd = Convert.ToInt32(cellControlWindow.getIdTrackerByINTERFACE()); //tb_idTracker.Text
                return idd;
            }
            catch
            {
                return -1;
            }
        }
        public int setTrackerId_TRACKER_INTERFACE(int _id)
        {
            try
            {
                int idd = Convert.ToInt32(trackerCtrlWindow.tb_idTracker.Text = _id.ToString());
                return idd;
            }
            catch
            {
                return -1;
            }
        }
        public int setTrackerId_CELL_INTERFACE(int _id)
        {
            try
            {
                int idd = Convert.ToInt32(cellControlWindow.setIdTrackerOnINTERFACE(_id)); //tb_idTracker.Text
                return idd;
            }
            catch
            {
                return -1;
            }
        }


        public int getUserId()
        {
            return userWindow.getUserID();
        }

            

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
            base.OnClosing(e);
        }

        public API api()
        {
            return new API(this);
        }

    }
}
