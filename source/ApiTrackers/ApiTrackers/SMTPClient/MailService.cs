using ApiTrackers;
using ApiTrackers.Exceptions;
using ApiTrackers.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace MailingTest
{
    class MailService
    {
        string jsonFileName = "SMTPClient\\MailConfig.json";

        private string host;
        private int port = 0;
        private string from_name;
        private string from_mail;
        private string from_pass;

        public MailService()
        {
            string pathJsonFile = Directory.GetCurrentDirectory() + "\\" + jsonFileName;
            if (File.Exists(pathJsonFile))
            {
                try { 
                    StreamReader r = new StreamReader(pathJsonFile);
                    string smtpConfig = r.ReadToEnd();
                    JObject json = JsonConvert.DeserializeObject<JObject>(smtpConfig);
                    host = json.GetValue("smtp_server").ToString();
                    port = Convert.ToInt32(json.GetValue("smtp_port"));
                    from_pass = json.GetValue("smtp_pass").ToString();
                    from_name = json.GetValue("smtp_name").ToString();
                    from_mail = json.GetValue("smtp_mail").ToString();
                    from_pass = json.GetValue("smtp_pass").ToString();
                }
                catch
                {
                    throw new MailServerConfigurationException();
                }
            }
            else
                throw new MailServerConfigurationException();

        }

        public void sendMail_GivenWriteAccess(Main _main, RightMusic _rightMusic)
        {
            int idTracker = _rightMusic.idTracker;
            Tracker tracker = _main.bddTracker.selectTracker(idTracker);
            ; string titleTracker = tracker.trackerMetadata.title;

            int idUserOwner = tracker.idUser;
            User userowner = _main.bddUser.selectUser(idUserOwner);
            ; string pseudoOwner = userowner.pseudo;

            int idUserGiven = _rightMusic.idUser;
            User usergiven = _main.bddUser.selectUser(idUserGiven);
            ; string userGivenMail = usergiven.mail;


            MailService mailService = new MailService();
            mailService.sendMail_RightMusic_GivenWriteAccess(pseudoOwner, titleTracker, userGivenMail);

        }
        public void sendMail_GivenWriteAccess(Tracker _tracker, User _userowner, User _usergiven)
        {
            string titleTracker = _tracker.trackerMetadata.title;
            string pseudoOwner = _userowner.pseudo;
            string userGivenMail = _usergiven.mail;

            MailService mailService = new MailService();
            mailService.sendMail_RightMusic_GivenWriteAccess(pseudoOwner, titleTracker, userGivenMail);
        }

        public void sendMail_RightMusic_GivenWriteAccess(string _pseudoOwner, string _nameMusic, string _toMail)
        {
            string subject = _pseudoOwner + " Give you write Access ! "+ from_name;
            string message =
            "<h1>"+ from_name + "</h1>"
            +"<p>Droit d'écriture donné par <b>" + _pseudoOwner + "</b> "
            +"pour la musique <b>'" + _nameMusic + "'</b> !</p>";

            sendMail(subject, message, _toMail);
        }

        public void sendMail(string _subject, string _body, string _to)
        {
            if (IsValidEmail(_to)) { 
                string CONFIGSET = "ConfigSet";
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.From = new MailAddress(from_mail, from_name + " 🔥");
                message.To.Add(new MailAddress(_to));
                message.Subject = _subject;
                message.Body = _body;

                message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

                using (var client = new SmtpClient(host, port))
                {
                    client.Credentials = new NetworkCredential(from_mail, from_pass);
                    client.EnableSsl = true;
                    client.Send(message);
                }
            } else throw new InvalidMailAddressServerException();
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
