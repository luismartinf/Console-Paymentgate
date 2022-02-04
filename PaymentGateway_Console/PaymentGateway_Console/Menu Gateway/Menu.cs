using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console

{   //Menu to display the options of the clients
    class Menu
    {
        //Collections to saved the information of clients, Sellers and purchases in txt iles
        public static object[] Gateway;
        public static List<Seller> sellers_list;
        public static List<Client> clients_list;
        public static Dictionary <string,Transaction>transaction_list;
        public static SortedList<string,Payment_method> paymethod_list;
        public static SortedList<string, Transfer_Bank> transfer_list;

        //menu of the client
        public static void Menu_clients()
        //read the database of clients
        {
            Read_write_files files_rw = new Read_write_files();
            Client_actions client_actions = new Client_actions();
            Payment_actions payment_Actions = new Payment_actions();
            //path where is saved the file  (@"C:\Users\luis.martin\Downloads)
            List<Client> clients = (List<Client>)files_rw.Read_list("Clients Gateway", @"C:\Users\luis.martin\Downloads\", "Clients");
            SortedList<string,Payment_method> payments = (SortedList<string, Payment_method>)files_rw.Read_list("Paymethod Gateway", @"C:\Users\luis.martin\Downloads\", "Paymethod");
            Console.WriteLine($"If you need to add your information type A");
            Console.WriteLine($"If you saved your information and want delete it type D");
            Console.WriteLine($"If you saved your information and want new  type P");
            Console.WriteLine($"If you saved your information and want to view type V");
            char options = Convert.ToChar(Console.ReadLine());
            bool validoption = false;
            //    Validate one of the four possible actions Add/ Delete/ Purchase/ View       
            while (!validoption)
            {
                switch (options)
                {
                    case 'A':
                        bool correct_data = false;
                        object obj="";
                        //Check if the information is correct otherwise reenter the information
                        while (!correct_data)
                        {
                            Client new_client =(Client) client_actions.Add_info(obj);
                            Payment_method new_pay = (Payment_method)payment_Actions.Add_info(new_client.UserName1);
                            //add client information
                            Console.WriteLine("This is your personal information");
                            client_actions.Show_info(new_client);
                            //add paymentmethod
                            payment_Actions.Show_info(new_pay);
                            Console.WriteLine($"Your data saved {new_client.Add_date1} is correct Yes/No?");
                            string validinformation = Console.ReadLine();
                            if (validinformation == "Yes")
                            { 
                              clients.Add(new_client);
                              string id_pay = new_client.UserName1 + new_pay.L4_digit1 as string;
                              payments.Add(id_pay, new_pay);
                              correct_data = true;
                            }
                            else
                            {
                              correct_data = false;
                            }
                        }
                        //Update the list
                        clients_list = clients;
                        paymethod_list = payments;
                        //Update the client list that is a txt file saved from the list
                        files_rw.Write_file("Clients Gateway", clients_list, @"C:\Users\luis.martin\Downloads\");
                        files_rw.Write_file("Paymethod Gateway", paymethod_list, @"C:\Users\luis.martin\Downloads\");
                        validoption = true;
                        break;

                    case 'D':
                        // Search the client information
                        Console.WriteLine("Enter your username");
                        string user_name = Console.ReadLine();
                        List<string> pay_del=null;
                        foreach (var item_C in clients)
                        {
                            if (item_C.UserName1 == user_name)
                            {   // Find all the paymethods for a client to delete the if erase the user
                                
                                foreach (var pay in payments.Keys)
                                { if (pay.Contains(user_name))
                                        {pay_del.Add(pay); } 
                                }
                                //Confirm to erase the information with the password with 3 attempts 
                                Console.WriteLine("If you want to erase your information enter your password");
                                string key_client = Console.ReadLine();
                                int attempts = 0;
                                bool correct_pas = false;
                                while (!correct_pas | attempts < 3)
                                {
                                    if (item_C.Password1 == key_client)
                                    {
                                        //Delete client information
                                        client_actions.Delete_info(user_name);
                                        foreach (var dpay in pay_del)
                                        { payment_Actions.Delete_info(pay_del); }
                                        pay_del.Clear();
                                        Console.WriteLine("Personal Information Deleted");
                                        files_rw.Write_file("Clients Gateway", clients_list, @"C:\Users\luis.martin\Downloads\");
                                        files_rw.Write_file("Paymethod Gateway", paymethod_list, @"C:\Users\luis.martin\Downloads\");
                                        correct_pas = true;
                                    }
                                    else
                                    {
                                        attempts++;
                                        Console.WriteLine($"Incorrect pasword, try again remain attempst {3 - attempts}");
                                        key_client = Console.ReadLine();
                                        correct_pas = false;
                                    }
                                }
                            }
                        }
                        validoption = true;
                        break;

                    case 'P':
                        Console.WriteLine("Enter your username");
                        //find the client information to do a new purchase
                        user_name = Console.ReadLine(); 
                        foreach (var item_C in clients)
                        {
                            if (item_C.UserName1 == user_name)
                            {

                                Console.WriteLine("If you want to purchase enter your password");
                                string key_client = Console.ReadLine();
                                int attempts = 0;
                                bool correct_pas = false;
                                while (!correct_pas | attempts < 3)
                                {
                                    if (item_C.Password1 == key_client)
                                    {
                                        // Confirm the trasaction and client information with password 3 attempts
                                        Console.WriteLine("This is your information");
                                        client_actions.Show_info(item_C);
                                        // Do the new transaction
                                        //client_actions.Purchase_client(item_C.Card_N);
                                        Console.WriteLine("Succesful Purchase the Order Number and Shipped Order are");
                                        // Return the information of the transaction
                                        //Console.WriteLine($"Shipped Order: { } ");
                                        correct_pas = true;
                                    }
                                    else
                                    {
                                        attempts++;
                                        Console.WriteLine($"Incorrect pasword, try again remain attempst {3 - attempts}");
                                        key_client = Console.ReadLine();
                                        correct_pas = false;
                                    }
                                }
                            }
                        }
                        validoption = true;
                        break;

                    case 'V':
                        Console.WriteLine("Enter your username");
                        user_name = Console.ReadLine();
                        pay_del = null;
                        // Find client information to display
                        foreach (var item_C in clients)
                        {
                            if (item_C.UserName1 == user_name)
                            {
                                foreach (var pay in payments.Keys)
                                {
                                    if (pay.Contains(user_name))
                                    { pay_del.Add(pay); }
                                }
                                //view the information
                                Console.WriteLine("If you want to view your information enter your password");
                                string key_client = Console.ReadLine();
                                int attempts = 0;
                                bool correct_pas = false;
                                while (!correct_pas | attempts < 3)
                                {
                                    if (item_C.Password1 == key_client)
                                    {   //show information for client
                                        Console.WriteLine($"This is your information saved {item_C.Add_date1}");
                                        client_actions.Show_info(item_C);
                                        //show information for all the payment methods
                                        foreach (var dpay in pay_del)
                                        { payment_Actions.Show_info(pay_del); }
                                        pay_del.Clear();
                                        Console.WriteLine("If is not correct deleted your data and add the new one");
                                        correct_pas = true;
                                    }
                                    else
                                    {
                                        attempts++;
                                        Console.WriteLine($"Incorrect pasword, try again remain attempst {3 - attempts}");
                                        key_client = Console.ReadLine();
                                        correct_pas = false;
                                    }
                                }
                            }
                        }
                        validoption = true;
                        break;
                    default:
                        Console.WriteLine("Option not exist, reenter the value");
                        Console.WriteLine($"If you are new client, you need to add your information type A");
                        Console.WriteLine($"If you saved your information and want delete it type D");
                        Console.WriteLine($"If you saved your information and want new purchase type P");
                        Console.WriteLine($"If you saved your information and want to view type V");
                        options = Convert.ToChar(Console.ReadLine());
                        validoption = false;
                        break;
                }
            }
        }

        //Menu for the seller 
        public static void Menu_sellors()
        {
            //Read the file with the Sellers information
            Read_write_files files_rw = new Read_write_files();
            Seller_actions seller_actions = new Seller_actions();
            Payment_actions payment_Actions = new Payment_actions();
            //path where is saved the file  (@"C:\Users\luis.martin\Downloads)
            List<Seller> sellers = (List<Seller>)files_rw.Read_list("Sellers Gateway", @"C: \Users\luis.martin\Downloads\", "Sellers");
            SortedList<string, Payment_method> payments = (SortedList<string, Payment_method>)files_rw.Read_list("Paymethod Gateway", @"C:\Users\luis.martin\Downloads\", "Paymethod");

            // Sellect one of the four possible actions Add/ Delete/ Shipping/ View for a seller and validate 
            Console.WriteLine($"If you are new seller, you need to add your information type A");
            Console.WriteLine($"If you saved your information and want delete it type D");
            Console.WriteLine($"If you saved your information and want new shipping type S");
            Console.WriteLine($"If you saved your information and want to view type V");
            char options = Convert.ToChar(Console.ReadLine());
            bool validoption = false;
            while (!validoption)
            {
                switch (options)
                {
                    case 'A':
                        //Modifing the size of the seller array to create a new seller
                        bool correct_data = false;
                        object obj = "";
                        //Check the correct information to be saved fron the Sellers
                        while (!correct_data)
                        {
                            Seller new_seller = (Seller)seller_actions.Add_info(obj);
                            Payment_method new_pay = (Payment_method)payment_Actions.Add_info(new_seller.UserName1);
                            Console.WriteLine("This is your information");
                            seller_actions.Show_info(new_seller);
                            payment_Actions.Show_info(new_pay);
                            Console.WriteLine($"Your data saved {new_seller.Add_date1} is correct Yes/No?");
                            string validinformation = Console.ReadLine();
                            if (validinformation == "Yes")
                            {
                                sellers.Add(new_seller);
                                string id_pay = new_seller.UserName1 + new_pay.L4_digit1 as string;
                                payments.Add(id_pay, new_pay);
                                correct_data = true; }
                            else
                            {
                                correct_data = false;
                            }
                        }
                        //Update the list
                        sellers_list = sellers;
                        paymethod_list = payments;
                        //Update the client list that is a txt file saved from the list
                        files_rw.Write_file("Sellers Gateway", sellers_list, @"C:\Users\luis.martin\Downloads\");
                        files_rw.Write_file("Paymethod Gateway", paymethod_list, @"C:\Users\luis.martin\Downloads\");
                        validoption = true;
                        break;

                    case 'D':
                        Console.WriteLine("Enter your username");
                        //Find the information of the seller
                        string user_name = Console.ReadLine();
                        List<string> pay_del = null;
                        foreach (var item_C in sellers)
                        {
                            if (item_C.UserName1 == user_name)
                            { 

                                // Find all the paymethods for a client to delete the if erase the user
                                foreach (var pay in payments.Keys)
                                {
                                    if (pay.Contains(user_name))
                                    { pay_del.Add(pay); }
                                }
                            
                                // Validate with the password maximun in three attempts to erase the data
                                Console.WriteLine("If you want to erase your information enter your password");
                                string key_client = Console.ReadLine();
                                int attempts = 0;
                                bool correct_pas = false;
                                while (!correct_pas | attempts < 3)
                                {
                                    if (item_C.Password1 == key_client)
                                    {
                                        //Delete seller information
                                        seller_actions.Delete_info(user_name);
                                        foreach (var dpay in pay_del)
                                        { payment_Actions.Delete_info(pay_del); }
                                        pay_del.Clear();
                                        Console.WriteLine("Personal Information Deleted");
                                        files_rw.Write_file("Clients Gateway", sellers_list, @"C:\Users\luis.martin\Downloads\");
                                        files_rw.Write_file("Paymethod Gateway", paymethod_list, @"C:\Users\luis.martin\Downloads\");
                                        correct_pas = true;
                                    }
                                    else
                                    {
                                        attempts++;
                                        Console.WriteLine($"Incorrect pasword, try again remain attempst {3 - attempts}");
                                        key_client = Console.ReadLine();
                                        correct_pas = false;
                                    }
                                }
                            }
                        }
                        validoption = true;
                        break;

                    case 'S':
                        Console.WriteLine("Enter your username");
                        //find the seller´s data to a new shhiping
                        user_name = Console.ReadLine();
                        pay_del = null; ;
                        foreach (var item_C in sellers)
                        {
                            if (item_C.UserName1 == user_name)
                            {
                                foreach (var pay in payments.Keys)
                                {
                                    if (pay.Contains(user_name))
                                    { pay_del.Add(pay); }
                                }
                                Console.WriteLine("If you want to shhiping enter your password");
                                string key_client = Console.ReadLine();
                                int attempts = 0;
                                bool correct_pas = false;
                                while (!correct_pas | attempts < 3)
                                {
                                    if (item_C.Password1 == key_client)
                                    {
                                        Console.WriteLine("This is your information");
                                        seller_actions.Show_info(item_C);
                                        // Creating a new shipping from a seller
                                        //sellor_actions.Purchase_client(item_C.Card_N);
                                        Console.WriteLine("Succesful Shipping the Order Number and Purchase Item are");
                                        // Displaying the Items Purchase by the client
                                        //Console.WriteLine($"Shipped Order: { } ");
                                        correct_pas = true;
                                    }
                                    else
                                    {
                                        attempts++;
                                        Console.WriteLine($"Incorrect pasword, try again remain attempst {3 - attempts}");
                                        key_client = Console.ReadLine();
                                        correct_pas = false;
                                    }
                                }
                            }
                        }
                        validoption = true;
                        break;

                    case 'V':
                        Console.WriteLine("Enter your card number");
                        //Finding the information of a seller for display it
                        user_name = Console.ReadLine();
                        pay_del = null;
                        foreach (var item_C in sellers)
                        {
                            if (item_C.UserName1 == user_name)
                            {
                                //VAlidating the display of the data with the password with maximun 3 attempts
                                Console.WriteLine("If you want to view your information enter your password");
                                string key_client = Console.ReadLine();
                                int attempts = 0;
                                bool correct_pas = false;
                                while (!correct_pas | attempts < 3)
                                {
                                    if (item_C.Password1 == key_client)
                                    {
                                        Console.WriteLine($"This is your information saved {item_C.Add_date1}");
                                        seller_actions.Show_info(item_C);
                                        foreach (var dpay in pay_del)
                                        { payment_Actions.Show_info(pay_del); }
                                        pay_del.Clear();
                                        Console.WriteLine("If is not correct deleted your data and add the new one");
                                        correct_pas = true;
                                    }
                                    else
                                    {
                                        attempts++;
                                        Console.WriteLine($"Incorrect pasword, try again remain attempst {3 - attempts}");
                                        key_client = Console.ReadLine();
                                        correct_pas = false;
                                    }
                                }
                            }
                        }
                        validoption = true;
                        break;
                    default:
                        Console.WriteLine("Option not exist, reenter the value");
                        Console.WriteLine($"If you are new client, you need to add your information type A");
                        Console.WriteLine($"If you saved your information and want delete it type D");
                        Console.WriteLine($"If you saved your information and want new purchase type P");
                        Console.WriteLine($"If you saved your information and want to view type V");
                        options = Convert.ToChar(Console.ReadLine());
                        validoption = false;
                        break;
                }
            }
        }
        public static void Menu_managers()
        {
            //   Menu for the managers
            //   Read and Display all the information of the three databases CLient, Sellers and Transactions in a object jared array
            Read_write_files files_rw = new Read_write_files();
            Gateway = new object[4][];
            List<Client> clients = (List<Client>)files_rw.Read_list("Clients Gateway", @"C:\Users\luis.martin\Downloads\", "Clients");
            Gateway[0] = clients;
            List<Seller> sellers = (List<Seller>)files_rw.Read_list("Sellers Gateway", @"C: \Users\luis.martin\Downloads\", "Sellers");
            Gateway[1] = sellers;
            Dictionary<string,Transaction> transactions = (Dictionary<string, Transaction>)files_rw.Read_list("Transactions Gateway", @"C: \Users\luis.martin\Downloads\", "Transactions");
            Gateway[2] = Menu.transaction_list;
            SortedList<string, Payment_method> payments = (SortedList<string, Payment_method>)files_rw.Read_list("Paymethod Gateway", @"C:\Users\luis.martin\Downloads\", "Paymethod");
            Gateway[3] = payments;
            SortedList<string, Transfer_Bank> transfers = (SortedList<string, Transfer_Bank>)files_rw.Read_list("Transfers Gateway", @"C:\Users\luis.martin\Downloads\", "Transfers");
            Gateway[4] = transfers;
        }



    }
}