using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    // class with the actions of a customer: Add/ Delete/ View 
    class Customer_BO:IActions_info
    {
        
        
        //Method to add a customer
        public object Add_info(object obj, string type = "Customer")
        {
            // Retrieve information from the customer saved in the customer txt file
            string userName="", first_N = "", last_N = "", add_date = "", password = "", email = "";
            Customer customer = new Customer(userName, first_N, last_N, add_date, password, email);

            //Generate a user name and check if it doesnot exist
            List<string> user_used = new List<string>();
            if (Main_menu.customers_list != null)
            {
                foreach (Customer c_user in Main_menu.customers_list)
                { user_used.Add(c_user.UserName1); }
            }
            Console.WriteLine("Generate a username");
            string new_user=Console.ReadLine();
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
            customer.UserName1 = new_user;

            
            //select a valid password
            Console.WriteLine($"Enter a Password that: \n" +
                $"1.The password should contain at least one uppercase and lowercase alphabets. \n " +
                $"2.The password should contain at least one numerical value. \n " +
                $"3.The password should contain at least one special character.\n" +
                $"4.The password should not contain any whitespaces. \n"+
                $"5.The length of the password should more than 7 characters.");
            bool validpassword = false;
            string val_password = Console.ReadLine();
            while (!validpassword)
            {  
                validpassword = Valpassword.ValidatePassword(val_password);
                if (validpassword == true)
                {
                    Console.Write("Valid password");
                    customer.Password1 = val_password;
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
            Console.WriteLine("Email");
            customer.Email1 = Console.ReadLine(); 
            Console.WriteLine("First Name");
            customer.First_N1 = Console.ReadLine();
            Console.WriteLine("Last Name");
            customer.Last_N1 = Console.ReadLine();
            customer.Addtime();
            return customer;
        }

        //Method to delete the information saved in the customer list from a customer
        
        public void Delete_info(object user_name)
        {
            List<Customer> customers = Main_menu.customers_list;
            string user = Convert.ToString(user_name);
            customers.RemoveAll(customer => customer.UserName1 == user);
            Main_menu.customers_list = customers;
        }
        //Method to show the information saved from the customer
       
        public void Show_info(object customer_data)
        {
            Customer customer = (Customer)customer_data;
            Console.WriteLine(customer.Display_inf());
        }
    }
}

