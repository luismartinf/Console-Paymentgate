using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    class Sellers_menu
    {
        public void Menu_sellers()
        {
            Read_write_files files_rw = new Read_write_files();
            Seller_BO seller_actions = new Seller_BO();
            Payment_BO payment_Actions = new Payment_BO();
            //path where is saved the file  (@"C:\Users\luis.martin\Downloads)
            //Read the file with the Sellers information
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
                            Payment_method new_pay = (Payment_method)payment_Actions.Add_info(new_seller.UserName1, "Seller");
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
                                correct_data = true;
                            }
                            else
                            {
                                correct_data = false;
                            }
                        }
                        //Update the list
                        Main_menu.sellers_list = sellers;
                        Main_menu.paymethod_list = payments;
                        //Update the client list that is a txt file saved from the list
                        files_rw.Write_file("Sellers Gateway", Main_menu.sellers_list, @"C:\Users\luis.martin\Downloads\");
                        files_rw.Write_file("Paymethod Gateway", Main_menu.paymethod_list, @"C:\Users\luis.martin\Downloads\");
                        validoption = true;
                        break;

                    case 'D':
                        Console.WriteLine("Enter your username");
                        //Find the information of the seller
                        string user_name = Console.ReadLine();
                        List<string> pay_del = new List<string>();
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
                                while (attempts < 3)
                                {
                                    if (item_C.Password1 == key_client)
                                    {
                                        //Delete seller information
                                        foreach (var dpay in pay_del)
                                        { payment_Actions.Delete_info(dpay); }
                                        pay_del.Clear();
                                        seller_actions.Delete_info(user_name);
                                        Console.WriteLine("Personal Information Deleted");
                                        files_rw.Write_file("Clients Gateway", Main_menu.sellers_list, @"C:\Users\luis.martin\Downloads\");
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

                    case 'S':
                        Console.WriteLine("Enter your username");
                        //find the seller´s data to a new shhiping
                        user_name = Console.ReadLine();
                        pay_del = new List<string>(); ;
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

                                while (attempts < 3)
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
                        Console.WriteLine("Enter your card number");
                        //Finding the information of a seller for display it
                        user_name = Console.ReadLine();
                        pay_del = new List<string>();
                        foreach (var item_C in sellers)
                        {
                            if (item_C.UserName1 == user_name)
                            {
                                //Validating the display of the data with the password with maximun 3 attempts
                                Console.WriteLine("If you want to view your information enter your password");
                                string key_client = Console.ReadLine();
                                int attempts = 0;
                                while (attempts < 3)
                                {
                                    if (item_C.Password1 == key_client)
                                    {
                                        Console.WriteLine($"This is your information saved {item_C.Add_date1}");
                                        seller_actions.Show_info(item_C);
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
