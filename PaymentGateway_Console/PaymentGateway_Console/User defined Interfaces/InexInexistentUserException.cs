using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    class InexistentUserException : Exception
    {
        public InexistentUserException() { }

        private InexistentUserException(string user)
            : base($"User {user} does not exist")
        { }

        public void InexistentUser(string user, char role)
        {
            // Search if the user exist or need to be added
            List<string> user_exist = new List<string>();
            if (role == 'C')
            {
                if (Main_menu.customers_list != null)
                {
                    foreach (Customer c_user in Main_menu.customers_list)
                    { user_exist.Add(c_user.UserName1); }
                }
            }
            else
            {
                if (Main_menu.sellers_list != null)
                {
                    foreach (Seller c_user in Main_menu.sellers_list)
                    { user_exist.Add(c_user.UserName1); }
                }
            }
            if (!user_exist.Contains(user))
            {
                throw new InexistentUserException(user);
            }
        }

    }
}
