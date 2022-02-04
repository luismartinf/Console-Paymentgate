using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    class Client_menu
    {
        public void Menu_clients()
        {


            Read_write_files files_rw = new Read_write_files();
            Client_BO client_actions = new Client_BO();
            Payment_BO payment_Actions = new Payment_BO();
            //path where is saved the file  (@"C:\Users\luis.martin\Downloads)
            //read the database of clients
            List<Client> clients = (List<Client>)files_rw.Read_list("Clients Gateway", @"C:\Users\luis.martin\Downloads\", "Clients");
            SortedList<string, Payment_method> payments = (SortedList<string, Payment_method>)files_rw.Read_list("Paymethod Gateway", @"C:\Users\luis.martin\Downloads\", "Paymethod");
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
                        object obj = "";
                        //Check if the information is correct otherwise reenter the information
                        while (!correct_data)
                        {
                            Client new_client = (Client)client_actions.Add_info(obj);
                            Payment_method new_pay = (Payment_method)payment_Actions.Add_info(new_client.UserName1, "Client");
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
                        Main_menu.clients_list = clients;
                        Main_menu.paymethod_list = payments;
                        //Update the client list that is a txt file saved from the list
                        files_rw.Write_file("Clients Gateway", Main_menu.clients_list, @"C:\Users\luis.martin\Downloads\");
                        files_rw.Write_file("Paymethod Gateway", Main_menu.paymethod_list, @"C:\Users\luis.martin\Downloads\");
                        validoption = true;
                        break;

                    case 'D':
                        // Search the client information
                        Console.WriteLine("Enter your username");
                        string user_name = Console.ReadLine();
                        List<string> pay_del = new List<string>();
                        foreach (var item_C in clients)
                        {
                            if (item_C.UserName1 == user_name)
                            {   // Find all the paymethods for a client to delete the if erase the user

                                foreach (var pay in payments.Keys)
                                {
                                    if (pay.Contains(user_name))
                                    { pay_del.Add(pay); }
                                }
                                //Confirm to erase the information with the password with 3 attempts 
                                Console.WriteLine("If you want to erase your information enter your password");
                                string key_client = Console.ReadLine();
                                int attempts = 0;
                                while (attempts < 3)
                                {
                                    if (item_C.Password1 == key_client)
                                    {
                                        //Delete client information
                                        foreach (var dpay in pay_del)
                                        { payment_Actions.Delete_info(dpay); }
                                        pay_del.Clear();
                                        client_actions.Delete_info(user_name);
                                        Console.WriteLine("Personal Information Deleted");
                                        files_rw.Write_file("Clients Gateway", Main_menu.clients_list, @"C:\Users\luis.martin\Downloads\");
                                        files_rw.Write_file("Paymethod Gateway", Main_menu.paymethod_list, @"C:\Users\luis.martin\Downloads\");
                                        attempts += 3;
                                    }
                                    else
                                    {
                                        attempts++;
                                        Console.WriteLine($"Incorrect pasword, try again remain attempst {3 - attempts}");
                                        key_client = Console.ReadLine();
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
                                while (attempts < 3)
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
                                        attempts += 3;
                                    }
                                    else
                                    {
                                        attempts++;
                                        Console.WriteLine($"Incorrect pasword, try again remain attempst {3 - attempts}");
                                        key_client = Console.ReadLine();
                                    }
                                }
                            }
                        }
                        validoption = true;
                        break;

                    case 'V':
                        Console.WriteLine("Enter your username");
                        user_name = Console.ReadLine();
                        pay_del = new List<string>();
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
                                while (attempts < 3)
                                {
                                    if (item_C.Password1 == key_client)
                                    {   //show information for client
                                        Console.WriteLine($"This is your information saved {item_C.Add_date1}");
                                        client_actions.Show_info(item_C);
                                        //show information for all the payment methods
                                        foreach (var dpay in pay_del)
                                        { payment_Actions.Show_info(dpay); }
                                        pay_del.Clear();
                                        Console.WriteLine("If is not correct deleted your data and add the new one");
                                        attempts += 3;
                                    }
                                    else
                                    {
                                        attempts++;
                                        Console.WriteLine($"Incorrect pasword, try again remain attempst {3 - attempts}");
                                        key_client = Console.ReadLine();
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
    }
}
