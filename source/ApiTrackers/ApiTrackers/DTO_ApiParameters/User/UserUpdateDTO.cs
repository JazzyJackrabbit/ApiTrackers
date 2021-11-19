using ApiTrackers.Objects;

namespace ApiTrackers.DTO_ApiParameters
{
    public class UserUpdateDTO
    {
        public int id {get; set;}
        public string pseudo { get; set; }
        public string mail { get; set; }
        public string passwordHash { get; set; }
        public int wantReceiveMails { get; set; }
        public int adminMode { get; set; } = 0;
        public int isEnable { get; set; }


        public User toUser()
        {
            User user = new User(pseudo, mail, passwordHash);
            user.recoverMails = wantReceiveMails;
            user.adminMode = adminMode;
            user.isEnable = isEnable;
            return user;
        }
    }
}
