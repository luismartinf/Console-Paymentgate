using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEbox_example1
{
    class User
    {
        //fields
        string username;
        string mailId;
        string password;

        public User()
        {
        }

        public User(string username, string mailId, string password)
        {
            this.username = username;
            this.mailId = mailId;
            this.password = password;
        }

        public string Username { get => username; set => username = value; }
        public string MailId { get => mailId; set => mailId = value; }
        public string Password { get => password; set => password = value; }

        public override bool Equals(object user_compare)
        {
            User user = (User)user_compare;
            bool equals;
            if (this.Username.Equals(user.Username) && this.Password.Equals(user.Password))
            { equals = true; }
            else
            { equals = false; }
            return equals;
        }

        public override int GetHashCode()
        {
            int hashCode = 1710835385;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(username);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(password);
            return hashCode;
        }

        public override string ToString()
        {
            string user = $"Username: {Username} \n Mail Id:{MailId} \n Password: {Password}";
            return user ;
        }
    }
}
