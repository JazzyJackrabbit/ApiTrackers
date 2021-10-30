using ApiTrackers.Objects;

namespace ApiTrackers.DTO_ApiParameters
{
    public class UserCreateDTO
    {
        public string pseudo { get; set; }
        public string mail { get; set; }
        public string passwordHash { get; set; }
        public int wantReceiveMails { get; set; }
        public int isEnable { get; set; }

        public User toUser()
        {
            User user = new User(pseudo, mail, passwordHash);
            user.recoverMails = wantReceiveMails;
            user.isEnable = isEnable;
            return user;
        }
    }
}
