using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    // class with the actions of a seller: Add/ Delete/ View
    class Seller_BO : IActions_info
    {

        //Method to add a seller
        public object Add_info(object obj, string type = "Seller")
        {
            string userName = "", first_N = "", last_N = "", add_date = "", password = "", email = "", os_url="";
            Seller seller = new Seller(userName, first_N, last_N, add_date, password, email, os_url);

            //Generate a user name and check if it doesnot exist
            List<string> user_used = new List<string>();
            if (Main_menu.customers_list != null)
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

            //select a valid password
            Console.WriteLine($"Enter a Password that: \n" +
                $"1.The password should contain at least one uppercase and lowercase alphabets. \n " +
                $"2.The password should contain at least one numerical value. \n " +
                $"3.The password should contain at least one special character.\n" +
                $"4.The password should not contain any whitespaces. \n" +
                $"5.The length of the password should more than 7 characters.");
            bool validpassword = false;
            string val_password = Console.ReadLine();
            while (!validpassword)
            {
                validpassword = Valpassword.ValidatePassword(val_password);
                if (validpassword == true)
                {
                    Console.Write("Valid password");
                    seller.Password1 = val_password;
                }
                else
                {
                    Console.Write("InValid password, create a new one that");
                    Console.WriteLine($"1.The password should contain at least one uppercase and lowercase alphabets. \n " +
                        $"2.The password should contain at least one numerical value. \n " +
                        $"3.The password should contain at least one special character.\n" +
                        $"4.The password should not contain any whitespaces. \n" +
                        $"5.The length of the password should more than 7 characters.");
                    val_password = Console.ReadLine();
                }
            }

            //Rest of information
            Console.WriteLine("First Name");
            seller.First_N1 = Console.ReadLine();
            Console.WriteLine("Last Name");
            seller.Last_N1 = Console.ReadLine();
            Console.WriteLine("Email");
            seller.Email1 = Console.ReadLine();
            Console.WriteLine("Online Shopping Url");
            seller.OS_Url1 = Console.ReadLine();
            seller.Addtime();
            return seller;
        }

        
        //Method to delete the information saved in the customer list from a customer
        public void Delete_info(object user_name)
        {
            List<Seller> sellers = Main_menu.sellers_list;
            string user = Convert.ToString(user_name);
            sellers.RemoveAll(seller => seller.UserName1 == user);
            Main_menu.sellers_list = sellers;
        }
        //Method to show the information saved from the customer

        public void Show_info(object seller_data)
        {
            Seller sellers = (Seller) seller_data;
            Console.WriteLine(sellers.Display_inf());
        }


    }
}
