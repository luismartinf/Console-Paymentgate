using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEbox_example1
{
    class Program_example1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Introduce username, mailid, password");
            string user = Console.ReadLine();
            string[] user_array = user.Split(',');
            string username = user_array[0];
            string mailid = user_array[1];
            string password = user_array[2];
            User user1 = new User(username, mailid, password);

            Console.WriteLine("Introduce username, mailid, password");
            user = Console.ReadLine();
            user_array = user.Split(',');
            username = user_array[0];
            mailid = user_array[1];
            password = user_array[2];
            User user2 = new User(username, mailid, password);

            string same;
            if (user1.Equals(user2))
            { same = "User 1 is same as User 2"; }
            else
            { same = "User 1 and User 2 are different"; }
            Console.WriteLine($"{user1} \n \n \n {user2} \n \n \n {same}" );
            _=Console.ReadKey();

        }
    }
}
