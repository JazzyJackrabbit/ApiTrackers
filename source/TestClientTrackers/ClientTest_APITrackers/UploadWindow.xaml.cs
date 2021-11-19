using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logique d'interaction pour UploadWindow.xaml
    /// </summary>
    public partial class UploadWindow : Window
    {
        MainWindow main;
        public UploadWindow(MainWindow _main)
        {
            main = _main;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { }
            
        // download from api
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                int idModArchive = Convert.ToInt32(tb_idMODARCHIVE.Text);

                main.api().downloadSamples(idModArchive);
            }
            catch { }
        }
    }
}
