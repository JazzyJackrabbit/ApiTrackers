using Newtonsoft.Json.Linq;
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
        public UploadWindow uploadWindow;

        public Grid Grid_trackerCtrlWindow; // tracker
        public Grid Grid_cellControlWindow; // cell
        public Grid Grid_userWindow;  // user
        public Grid Grid_sampleWindow; // sample
        public Grid Grid_rightMusicWindow; // sample
        public Grid Grid_listWindow; // sample
        public Grid Grid_sampleAliasWindow; // sample
        public Grid Grid_UploadWindow; // sample

        public Label title1 = new Label();
        public Label title2 = new Label();
        public Label title3 = new Label();
        public Label title4 = new Label();


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
            uploadWindow = new UploadWindow(this);

            Grid child = Grid_userWindow = (Grid)userWindow.grid_content.Children[0];
            userWindow.grid_content.Children.Clear();
            //grid_content.Children.Add(child);

            child = Grid_trackerCtrlWindow = (Grid)trackerCtrlWindow.grid_content_Parent.Children[0];
            trackerCtrlWindow.grid_content_Parent.Children.Clear();
            //grid_content.Children.Add(child);

            child = Grid_cellControlWindow = (Grid)cellControlWindow.grid_content.Children[0];
            cellControlWindow.grid_content.Children.Clear();
            //grid_content.Children.Add(child);

            child = Grid_rightMusicWindow = (Grid)rightMusicWindow.grid_content.Children[0];
            rightMusicWindow.grid_content.Children.Clear();
            //grid_content.Children.Add(child);

            child = Grid_sampleWindow = (Grid)sampleWindow.grid_content.Children[0];
            sampleWindow.grid_content.Children.Clear();
            //grid_content.Children.Add(child);

            child = Grid_listWindow = (Grid)listWindow.grid_content_Parent.Children[0];
            listWindow.grid_content_Parent.Children.Clear();
            //grid_content.Children.Add(child);

            child = Grid_sampleAliasWindow = (Grid)sampleAliasWindow.grid_content.Children[0];
            sampleAliasWindow.grid_content.Children.Clear();
            //grid_content.Children.Add(child);

            child = Grid_UploadWindow = (Grid)uploadWindow.grid_content.Children[0];
            uploadWindow.grid_content.Children.Clear();


            title1.Content = "--";
            title2.Content = "--";
            title3.Content = "--";
            title4.Content = "--";

            Grid_trackerCtrlWindow.Name = "Trackers";
            Grid_cellControlWindow.Name = "Cells";
            Grid_userWindow.Name = "Users";
            Grid_sampleWindow.Name = "Samples";
            Grid_rightMusicWindow.Name = "RightMusics";
            Grid_listWindow.Name = "List";
            Grid_sampleAliasWindow.Name = "SamplesAlias";
            Grid_UploadWindow.Name = "Upload";


            invisibilityGridsAndButtons();
        }

        public void invisibilityGridsAndButtons()
        {
            Brush b1 = new SolidColorBrush(Color.FromRgb((byte)(48), (byte)(60), (byte)(150)));
            btn_cells.Background = b1;
            btn_rightMusics.Background = b1;
            btn_samples.Background = b1;
            btn_samplesAlias.Background = b1;
            btn_trackers.Background = b1;
            btn_users.Background = b1;
            btn_upload.Background = b1;
        }
       Brush b2 = new SolidColorBrush(Color.FromRgb((byte)(140), (byte)(50), (byte)(50)));

        private void UsersClick(object sender, RoutedEventArgs e)
        {
            goUserView();
        }

        private void UploadClick(object sender, RoutedEventArgs e)
        {
            invisibilityGridsAndButtons();
            display(Grid_UploadWindow); //.Visibility = Visibility.Visible; //
            btn_upload.Background = b2;
        }
        public void goUserView()
        {
            invisibilityGridsAndButtons();
            display(Grid_userWindow); //.Visibility = Visibility.Visible; //
            btn_users.Background = b2;
        }

        private void TrackersClick(object sender, RoutedEventArgs e)
        {
            invisibilityGridsAndButtons();
            display(Grid_trackerCtrlWindow); //.Visibility = Visibility.Visible; //
            btn_trackers.Background = b2;
        }

        private void CellsClick(object sender, RoutedEventArgs e)
        {
            invisibilityGridsAndButtons();
            display(Grid_cellControlWindow); //.Visibility = Visibility.Visible; //
            btn_cells.Background = b2;
        }
        public void openCellInterface()
        {
            invisibilityGridsAndButtons();
            display(Grid_cellControlWindow); //.Visibility = Visibility.Visible; //
            btn_cells.Background = b2;

            //cellControlWindow.selectAndDisplay(Main
        }

        private void RightMusicsClick(object sender, RoutedEventArgs e)
        {
            invisibilityGridsAndButtons();
            display(Grid_rightMusicWindow); //.Visibility = Visibility.Visible; //
            btn_rightMusics.Background = b2;

        }

        private void SamplesClick(object sender, RoutedEventArgs e)
        {
            goSampleView();
        }

        public void goSampleView()
        {
            invisibilityGridsAndButtons();
            display(Grid_sampleWindow); //.Visibility = Visibility.Visible; //
            btn_samples.Background = b2;
        }

        private void SamplesAliasClick(object sender, RoutedEventArgs e)
        {
            invisibilityGridsAndButtons();
            display(Grid_sampleAliasWindow); //.Visibility = Visibility.Visible; //
            btn_samplesAlias.Background = b2;
        }

        public void showListWindow()
        {
            invisibilityGridsAndButtons();
            display(Grid_listWindow); //.Visibility = Visibility.Visible; //

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
            return new API(this, tb_server.Text);
        }

        

        public void display(Grid _gridChild)
        {
            displaySuite(_gridChild);

            title1.HorizontalAlignment = HorizontalAlignment.Right;
            title2.HorizontalAlignment = HorizontalAlignment.Right;
            title3.HorizontalAlignment = HorizontalAlignment.Right;
            title4.HorizontalAlignment = HorizontalAlignment.Right;
            title1.Margin = new Thickness(0, 0, 0, 0);
            title2.Margin = new Thickness(0, 0, 0, 0);
            title3.Margin = new Thickness(0, 0, 0, 0);
            title4.Margin = new Thickness(0, 0, 0, 0);
            title1.FontSize = 32;
            title2.FontSize = 32;
            title3.FontSize = 32;
            title4.FontSize = 32;

            title1.FontWeight = FontWeights.Bold;
            title2.FontWeight = FontWeights.Bold;
            title3.FontWeight = FontWeights.Bold;
            title4.FontWeight = FontWeights.Bold;

            grid_content_1.Children.Remove(title1);
            grid_content_2.Children.Remove(title2);
            grid_content_3.Children.Remove(title3);
            grid_content_4.Children.Remove(title4);

            if (grid_content_1.Children.Count > 0)
                grid_content_1.Children.Add(title1);
            if (grid_content_2.Children.Count > 0)
                grid_content_2.Children.Add(title2);
            if (grid_content_3.Children.Count > 0)
                grid_content_3.Children.Add(title3);
            if (grid_content_4.Children.Count > 0)
                grid_content_4.Children.Add(title4);

            try { title1.Content = ((Grid)grid_content_1.Children[0]).Name + "  "; } catch { title1.Content = "...  "; }
            try { title2.Content = ((Grid)grid_content_2.Children[0]).Name + "  "; } catch { title2.Content = "...  "; }
            try { title3.Content = ((Grid)grid_content_3.Children[0]).Name + "  "; } catch { title3.Content = "...  "; }
            try { title4.Content = ((Grid)grid_content_4.Children[0]).Name + "  "; } catch { title4.Content = "...  "; }
        }

        public void displaySuite(Grid _gridChild)
        {
            Grid gr1 = grid_content_1.Children.Count > 0 ? (Grid)grid_content_1.Children[0] : new Grid();
            Grid gr2 = grid_content_1.Children.Count > 0 ? (Grid)grid_content_2.Children[0] : new Grid();
            Grid gr3 = grid_content_1.Children.Count > 0 ? (Grid)grid_content_3.Children[0] : new Grid();
            Grid gr4 = grid_content_1.Children.Count > 0 ? (Grid)grid_content_4.Children[0] : new Grid();

            if (_gridChild == gr1) return;

            if (grid_content_1.Children.Count > 0) grid_content_1.Children.RemoveAt(0);
            if (grid_content_2.Children.Count > 0) grid_content_2.Children.RemoveAt(0);
            if (grid_content_3.Children.Count > 0) grid_content_3.Children.RemoveAt(0);
            if (grid_content_4.Children.Count > 0) grid_content_4.Children.RemoveAt(0);

            if (_gridChild == gr2)
            {
                grid_content_1.Children.Add(gr2);
                grid_content_2.Children.Add(gr1);
                grid_content_3.Children.Add(gr3);
                grid_content_4.Children.Add(gr4);
                return;
            }
            if (_gridChild == gr3)
            {
                grid_content_1.Children.Add(gr3);
                grid_content_2.Children.Add(gr1);
                grid_content_3.Children.Add(gr2);
                grid_content_4.Children.Add(gr4);
                return;
            }
            if (_gridChild == gr4)
            {
                grid_content_1.Children.Add(gr4);
                grid_content_2.Children.Add(gr1);
                grid_content_3.Children.Add(gr2);
                grid_content_4.Children.Add(gr3);
                return;
            }

            grid_content_1.Children.Add(_gridChild);
            grid_content_2.Children.Add(gr1);
            grid_content_3.Children.Add(gr2);
            grid_content_4.Children.Add(gr3);
        }

        int nGrids = 4;



        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            sizeRows();
        }

        public void sizeRows()
        {
            double height = ActualHeight / nGrids;
            double width = grid_contents.ActualWidth;

            grid_content_1.Height = height;
            grid_content_2.Height = height;
            grid_content_3.Height = height;
            grid_content_4.Height = height;

            grid_content_1.Width = width;
            grid_content_2.Width = width;
            grid_content_3.Width = width;
            grid_content_4.Width = width;


        }

        private void set4Rows(object sender, RoutedEventArgs e)
        {
            nGrids = 4;

            grid_content_2.Visibility = Visibility.Visible;
            grid_content_3.Visibility = Visibility.Visible;
            grid_content_4.Visibility = Visibility.Visible;
            sizeRows();
        }

        private void set3Rows(object sender, RoutedEventArgs e)
        {
            nGrids = 3;

            grid_content_2.Visibility = Visibility.Visible;
            grid_content_3.Visibility = Visibility.Visible;
            grid_content_4.Visibility = Visibility.Collapsed;
            sizeRows();
        }

        private void set2Rows(object sender, RoutedEventArgs e)
        {
            nGrids = 2;

            grid_content_2.Visibility = Visibility.Visible;
            grid_content_3.Visibility = Visibility.Collapsed;
            grid_content_4.Visibility = Visibility.Collapsed;
            sizeRows();
        }

        private void set1Rows(object sender, RoutedEventArgs e)
        {
            nGrids = 1;

            grid_content_2.Visibility = Visibility.Collapsed;
            grid_content_3.Visibility = Visibility.Collapsed;
            grid_content_4.Visibility = Visibility.Collapsed;
            sizeRows();
        }

        private void grid_contents_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            sizeRows();

        }


        private void tb_server_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void TestsClick(object sender, RoutedEventArgs e)
        {
            logLine("= = = = ^^^ START TEST ^^^ = = = = = = = = = = = = = = = = = = ");
            logLine("= = = = ^^^ START TEST ^^^ = = = = = = = = = = = = = = = = = = = = ");
            logLine("= = = = ^^^ START TEST ^^^ = = = = = = = = = = = = = = = = = = = = ");
            logLine("= = = = ^^^ START TEST ^^^ = = = = = = = = = = = = = = = = = = = = ");
            logLine("= = = = ^^^ START TEST ^^^  = = = = = = = = = = = = = = = = = = = ");

            JObject jsonUser = new JObject{
                //new JProperty("id", 1),
                new JProperty("mail", "imjazzyjackrabbit@gmail.com"),
                new JProperty("permissions", "Edit"),
                new JProperty("passwordHash", "password"),
                new JProperty("pseudo", "pseudoTest"),
                new JProperty("wantReceiveMails", 1)
            };
            JObject jsonUser2 = new JObject{
                new JProperty("mail", "imjazzyjackrabbit@gmail.com"),
                new JProperty("permissions", "Edit"),
                new JProperty("passwordHash", "password"),
                new JProperty("pseudo", "pseudoTest2"),
                new JProperty("wantReceiveMails", 1)
            };


            JObject resultUser =
                new API(this).INSERT_User(jsonUser);
            int idUser = Convert.ToInt32(resultUser.GetValue("id").ToString());

            JObject resultUser2 =
                            new API(this).INSERT_User(jsonUser2);
            int idUser2 = Convert.ToInt32(resultUser2.GetValue("id").ToString());


            JObject jsonTracker = new JObject{
                new JProperty("id", 1),
                new JProperty("idUser", idUser),
                new JProperty("artist", "artistTest"),
                new JProperty("title", "titleTest"),
                new JProperty("bpm", "bpmTest"),
                new JProperty("comments", "commentsTest"),
                new JProperty("coprightInformations", "test")
            };


            JObject resultTracker =
                new API(this).INSERT_Tracker(idUser, jsonTracker);
            int idTracker = Convert.ToInt32(resultTracker.GetValue("id").ToString());


            JObject jsonSample = new JObject{
                new JProperty("id", 1),
                new JProperty("idLogo", 1),
                new JProperty("name", "nameTestOriginal"),
                new JProperty("color", "colornameTestOriginal"),
                new JProperty("linkSample", "link Sample")
            };

            JObject resultSample = new API(this).INSERT_Sample(jsonSample);
            int idSample = Convert.ToInt32(resultSample.GetValue("id").ToString());


            JObject jsonSampleAlias = new JObject{
                    new JProperty("idUser", idUser),
                    new JProperty("idLogo", 1),
                    new JProperty("name", "nameTestALIAS"),
                    new JProperty("color", "colorTestALIAS"),
                    new JProperty("idSample", 1)
            };


            JObject resultSampleAlias = new API(this).INSERT_SampleAlias(jsonSampleAlias);



            JObject jsonCell = new JObject
            {
                new JProperty("volume", 0.80),
                new JProperty("frequence", 0.85),
                new JProperty("idTracker", idTracker),
                new JProperty("id", 1),
                new JProperty("idUser", idUser)
            };


            new API(this).INSERT_Cell(idTracker, jsonCell);

            JObject jsonRightMusic = new JObject{
                    new JProperty("idTracker", idTracker),
                    new JProperty("idUser", idUser2),
                    new JProperty("right", "Edit"),
            };

            JObject resultRM = new API(this).INSERT_RightMusic(jsonRightMusic);

        }
    }
}
