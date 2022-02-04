using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    // class with the actions of a seller: Add/ Delete/ View and Make a new transaction(shipping)
    class Seller_BO : IActions_info
    {

        //Method to add a seller
        public object Add_info(object obj, string type = "Seller")
        {
            // Retrieve information from the client saved in the client txt file
            Seller seller = new Seller();

            //Generate a user name and check if it doesnot exist
            List<string> user_used = new List<string>();
            if (Main_menu.clients_list != null)
            {
                foreach (Seller c_user in Main_menu.sellers_list)
                { user_used.Add(c_user.UserName1);}
            }
            Console.WriteLine("Generate a username");
            string new_user = Console.ReadLine();
            bool validuser = false;
            while (!validuser)
            {
                if (user_used.Contains(new_user))
                {
                    Console.WriteLine("Taken user please generate a new one");
                    new_user = Console.ReadLine();
                    validuser = false;
                }
                else { validuser = true; }
            }
            seller.UserName1 = new_user;

            //Rest of information
            Console.WriteLine("Password");
            seller.Password1 = Console.ReadLine();
            Console.WriteLine("First Name");
            seller.First_N1 = Console.ReadLine();
            Console.WriteLine("Last Name");
            seller.Last_N1 = Console.ReadLine();
            Console.WriteLine("Email");
            seller.Email1 = Console.ReadLine();
            Console.WriteLine("Online Shopping Url");
            seller.OS_Url1 = Console.ReadLine();
            return seller;
        }

        
        //Method to delete the information saved in the client list from a client
        public void Delete_info(object user_name)
        {
            List<Seller> sellers = Main_menu.sellers_list;
            string user = Convert.ToString(user_name);
            sellers.RemoveAll(seller => seller.UserName1 == user);
            Main_menu.sellers_list = sellers;
        }
        //Method to show the information saved from the client

        public void Show_info(object seller_data)
        {
            Seller sellers = (Seller) seller_data;
            Console.WriteLine(sellers.Display_inf());
        }


    }
}
