using Newtonsoft.Json.Linq;
using System;
using System.Windows;

namespace ClientTest_APITrackers
{
    /// <summary>
    /// Logique d'interaction pour User__Window.xaml
    /// </summary>
    public partial class User__Window : Window
    {
        MainWindow main;
        public User__Window(MainWindow _main)
        {
            InitializeComponent();
            main = _main;
        }

        private JObject getFromInterface()
        {
            var json = new JObject{
                new JProperty("id", main.getUserId()),
                new JProperty("mail", tb_mail.Text),
                new JProperty("permissions", tb_permissions.Text),
                new JProperty("passwordHash", tb_passwordHash.Text),
                new JProperty("pseudo", tb_pseudo.Text),
                new JProperty("wantReceiveMails", tb_recoverMails.Text)
            };

            return json;
        }
        private void clearInterface()
        {
            tb_id.Text = "";
            tb_mail.Text = "";
            tb_permissions.Text = "";
            tb_passwordHash.Text = "";
            tb_pseudo.Text = "";
            tb_recoverMails.Text = "";
        }

        private JObject setOnInterface(JObject json)
        {
            foreach (JProperty p in json.Properties())
            {
                try
                {
                    if (p.Name == "id") tb_id.Text = p.Value.ToString();
                    if (p.Name == "mail") tb_mail.Text = p.Value.ToString();
                    if (p.Name == "permissions") tb_permissions.Text = p.Value.ToString();
                    if (p.Name == "passwordHash") tb_passwordHash.Text = p.Value.ToString();
                    if (p.Name == "pseudo") tb_pseudo.Text = p.Value.ToString();
                    if (p.Name == "recoverMails") tb_recoverMails.Text = p.Value.ToString();
                }
                catch
                { }
            }
            return json;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        public int getUserID()
        {
            try
            {
                return Convert.ToInt32(tb_id.Text);
            }
            catch
            {
                return -1;
            }
        }

        private void btn_SELECT_Click(object sender, RoutedEventArgs e)
        {
            try {
                JObject json = (JObject)API.SELECT_User(getUserID());
                setOnInterface(json);
            }
            catch {
                try
                {
                    JArray json = (JArray)API.SELECT_User(getUserID());
                    JToken jo = json[0];
                    setOnInterface(
                         (JObject)jo.ToObject((new JObject()).GetType())
                    );
                }
                catch {
                    clearInterface();
                }
            }
        }

        private void btn_UPDATE_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JObject json = API.UPDATE_User(getFromInterface());
                setOnInterface(json);
            }
            catch { clearInterface(); }
        }

        private void btn_DELETE_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JObject json = API.DELETE_User(getUserID());
                setOnInterface(json);
            }
            catch { clearInterface(); }
        }

        private void btn_INSERT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JObject json = API.INSERT_User(getFromInterface());
                setOnInterface(json);
            }
            catch { clearInterface(); }
        }

        private void btn_SELECT_ALL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JArray json = (JArray)API.SELECT_Users();

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
