using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    // class with the actions of a client: Add/ Delete/ View and Make a new transaction(purchase)
    class Client_actions:IActions_info
    {
        
        
        //Method to add a client
        public object Add_info(object obj, string type = "Client")
        {
            // Retrieve information from the client saved in the client txt file
            Client client = new Client();

            //Generate a user name and check if it doesnot exist
            List<string> user_used = new List<string>();
            if (Menu.clients_list != null)
            {
                foreach (Client c_user in Menu.clients_list)
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
            client.UserName1 = new_user;

            //Rest of information
            Console.WriteLine("Email");
            client.Email1 = Console.ReadLine();
            Console.WriteLine("Password");
            client.Password1 = Console.ReadLine();
            Console.WriteLine("First Name");
            client.First_N1 = Console.ReadLine();
            Console.WriteLine("Last Name");
            client.Last_N1 = Console.ReadLine();
            return client;
        }

        //Method to delete the information saved in the client list from a client
        readonly List<Client> clients = Menu.clients_list;
        public void Delete_info(object user_name)
        {            
            string user = Convert.ToString(user_name);
            clients.RemoveAll(client => client.UserName1 == user);
            Menu.clients_list = clients;
        }
        //Method to show the information saved from the client
       
        public void Show_info(object client_data)
        {
            Client client = (Client)client_data;
            Console.WriteLine(client.Display_inf());
        }
    }
}

