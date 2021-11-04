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
    /// Logique d'interaction pour TrackerCellUserControl.xaml
    /// </summary>
    public partial class SubCellEmptyControl : UserControl
    {

        MainWindow main;
        public int position;
        public SubCellEmptyControl(MainWindow _main, int _posit)
        {
            InitializeComponent();
            main = _main;

            position = _posit;

        }
        public SubCellEmptyControl()
        {
            InitializeComponent();



        }



    }
}
