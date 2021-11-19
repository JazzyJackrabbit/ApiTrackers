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
    /// Logique d'interaction pour ListUserWindow.xaml
    /// </summary>
    public partial class ListWindow : Window
    {
        public ListWindow()
        {
            InitializeComponent();

        }

        private void scrollevnt(object sender, ScrollChangedEventArgs e)
        {
           

        }

        internal void addOnList(JObject jobj)
        {
            string text = ("" + jobj).Replace("\n", "");

            TextBox box = new TextBox();
            box.IsEnabled = true;
            box.Text = text;

            if (lv.Children.Count % 2 == 0)
                box.Background = new SolidColorBrush(Color.FromRgb(
                    (byte)(0.70 * 256), 
                    (byte)(0.78 * 256), 
                    (byte)(0.78 * 256)));
            else
                box.Background = new SolidColorBrush(Color.FromRgb(
                    (byte)(0.59 * 256),
                    (byte)(0.66 * 256),
                    (byte)(0.66 * 256)));


            lv.Children.Add(box);

            offsetScroll = 100;
        }

        int offsetScroll = 50;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // up
            try {
                scrollviewer.ScrollToVerticalOffset(scrollviewer.VerticalOffset - offsetScroll);
            }
            catch { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // down
            try
            {
                scrollviewer.ScrollToVerticalOffset(scrollviewer.VerticalOffset + offsetScroll);
            }
            catch { }
        }
    }
}
