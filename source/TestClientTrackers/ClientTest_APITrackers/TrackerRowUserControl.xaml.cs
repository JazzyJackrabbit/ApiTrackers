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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientTest_APITrackers
{
    /// <summary>
    /// Logique d'interaction pour TrackerRowUserControl.xaml
    /// </summary>
    public partial class TrackerRowUserControl : UserControl
    {

        MainWindow main;
        public int idPiste;
        public TrackerRowUserControl(MainWindow _main, int _idPiste)
        {
            InitializeComponent();
            //idTracker = _idTracker;
            idPiste = _idPiste;
            main = _main;

        }

        public Grid getContent()
        {
            return grid_content;
        }
        public int getIDTrackerInterface()
        {
            return main.getTrackerId_TRACKER_INTERFACE();
        }

        public SubCellControl makeNoteInsert()
        {
            int _idCell = main.trackerCtrlWindow.insertCell(getIDTrackerInterface());


            int idSample = Convert.ToInt32(((JObject)main.api().SELECT_Cell(getIDTrackerInterface(), _idCell)).GetValue("idSample"));
            string url = ((JObject)main.api().SELECT_Sample(idSample)).GetValue("linkSample").ToString();


            SubCellControl tcUC = new SubCellControl(main, _idCell, 0, url);
            tcUC.grid_visible.Visibility = Visibility.Visible;
            listbox_notes.Items.Add(tcUC);
            return tcUC;
        }
        /*
        public SubCellControl makeNoteOnInterfaceOnly(int _idCell, double _vol, double _key)
        {
            SubCellControl tcUC = new SubCellControl(main, _idCell, _vol, _key, 0);
            tcUC.grid_visible.Visibility = Visibility.Visible;
            listbox_notes.Items.Add(tcUC);
            return tcUC;
        }
        public SubCellControl makeNoteOnInterfaceOnly(int _idCell, double _vol, double _key, bool _isVisible, int _posit)
        {
            SubCellControl tcUC = new SubCellControl(main, _idCell, _vol, _key, _posit);
            if(_isVisible)
                tcUC.grid_visible.Visibility = Visibility.Visible;
            else
                tcUC.grid_visible.Visibility = Visibility.Collapsed;

            if (_posit < listbox_notes.Items.Count)
                listbox_notes.Items[_posit] = tcUC;

            return tcUC;
        }*/
        public SubCellEmptyControl makeEMPTY()
        {
            SubCellEmptyControl tcUC = new SubCellEmptyControl();
            listbox_notes.Items.Add(tcUC);
            return tcUC;
        }
        private void btn_addNOTE_Click(object sender, RoutedEventArgs e)
        {
            makeNoteInsert();
        }

        private void btn_addEMPTY_Click(object sender, RoutedEventArgs e)
        {
            makeEMPTY();
        }
    }
}
