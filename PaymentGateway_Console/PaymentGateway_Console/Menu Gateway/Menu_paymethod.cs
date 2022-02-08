using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    class Menu_paymethod
    {
  
        //menu of the customer
        public static void Menu_customers()
        //read the database of customers
        {
            Read_write_files files_rw = new Read_write_files();
            Customer_BO customer_actions = new Customer_BO();
            Payment_BO payment_Actions = new Payment_BO();
            //path where is saved the file  (@"C:\Users\luis.martin\Downloads)
            List<Customer> customers = (List<Customer>)files_rw.Read_list("Customers Gateway",Main_menu.fullpath, "Customers");
            SortedList<string, Payment_method> payments = (SortedList<string, Payment_method>)files_rw.Read_list("Paymethod Gateway",Main_menu.fullpath, "Paymethod");
            Console.WriteLine($"If you need to add your information type A");
            Console.WriteLine($"If you saved your information and want delete it type D");
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
                        Console.WriteLine("Enter your username");
                        string user_name = Console.ReadLine();
                        //Check if the information is correct otherwise reenter the information
                        foreach (var item_C in customers)
                        {
                            if (item_C.UserName1 == user_name)
                            {
                                while (!correct_data)
                                {                            
                                    Console.WriteLine("This is your personal information");
                                    customer_actions.Show_info(item_C);
                                    Console.WriteLine("Add the new payment method");
                                    Payment_method new_pay = (Payment_method)payment_Actions.Add_info(item_C.UserName1,"Customer");
                                    payment_Actions.Show_info(new_pay);
                                    DateTime tdy = DateTime.Now;
                                    Console.WriteLine($"Your data saved {tdy} is correct Yes/No?");
                                    string validinformation = Console.ReadLine();
                                    if (validinformation == "Yes")
                                    {
                                        
                                        string id_pay = item_C.UserName1 + new_pay.L4_digit1 as string;
                                        payments.Add(id_pay, new_pay);
                                        correct_data = true;
                                    }
                                    else
                                    {
                                        correct_data = false;
                                    }
                                }
                            }    
                        }  
                        //Update the list
                        Main_menu.paymethod_list = payments;
                        //Update the customer list that is a txt file saved from the list
                        files_rw.Write_file("Paymethod Gateway", Main_menu.paymethod_list,Main_menu.fullpath);
                        validoption = true;
                        break;

                    case 'D':
                        // Search the customer information
                        Console.WriteLine("Enter your username");
                        user_name = Console.ReadLine();
                        Console.WriteLine("Enter your payment_id (username + last 4 digits of the card)");
                        string payment_id = Console.ReadLine();
                        foreach (var item_C in customers)
                        {
                            if (item_C.UserName1 == user_name)
                            {                                                                  
                                //Confirm to erase the information with the password with 3 attempts 
                                Console.WriteLine("If you want to erase your information enter your password");
                                string key_customer = Console.ReadLine();
                                int attempts = 0;
                                while (attempts < 3)
                                {
                                    if (item_C.Password1 == key_customer)
                                    {
                                        //Delete customer information
                                        payment_Actions.Delete_info(payment_id);
                                        Console.WriteLine("Payment method deleted");
                                        files_rw.Write_file("Paymethod Gateway", Main_menu.paymethod_list,Main_menu.fullpath);
                                        attempts += 3;
                                    }
                                    else
                                    {
                                        attempts++;
                                        Console.WriteLine($"Incorrect pasword, try again remain attempst {3 - attempts}");
                                        key_customer = Console.ReadLine();
                                    }
                                }
                            }
                        }
                        validoption = true;
                        break;

                    

                    case 'V':
                        Console.WriteLine("Enter your username");
                        user_name = Console.ReadLine();
                        Console.WriteLine("Enter your payment_id(username + last 4 digits of the card)");
                        payment_id = Console.ReadLine();
                        // Find customer information to display
                        foreach (var item_C in customers)
                        {
                            if (item_C.UserName1 == user_name)
                            {
                                //view the information
                                Console.WriteLine("If you want to view your information enter your password");
                                string key_customer = Console.ReadLine();
                                int attempts = 0;
                                while (attempts < 3)
                                {
                                    if (item_C.Password1 == key_customer)
                                    {
                                        Console.WriteLine($"This is your information saved {item_C.Add_date1}");
                                        customer_actions.Show_info(item_C);
                                        payment_Actions.Show_info(payment_id); 
                                        Console.WriteLine("If is not correct deleted your data and add the new one");
                                        attempts += 3;
                                    }
                                    else
                                    {
                                    attempts++;
                                    Console.WriteLine($"Incorrect pasword, try again remain attempst {3 - attempts}");
                                    key_customer = Console.ReadLine();
                                    }

                                }
                            }
                        }
                        validoption = true;
                        break;
                    default:
                        Console.WriteLine("Option not exist, reenter the value");
                        Console.WriteLine($"If you are new customer, you need to add your information type A");
                        Console.WriteLine($"If you saved your information and want delete it type D");
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
            Seller_BO seller_actions = new Seller_BO();
            Payment_BO payment_Actions = new Payment_BO();
            //path where is saved the file  (@"C:\Users\luis.martin\Downloads)
            List<Seller> sellers = (List<Seller>)files_rw.Read_list("Sellers Gateway", @"C: \Users\luis.martin\Downloads\", "Sellers");
            SortedList<string, Payment_method> payments = (SortedList<string, Payment_method>)files_rw.Read_list("Paymethod Gateway",Main_menu.fullpath, "Paymethod");

            // Sellect one of the four possible actions Add/ Delete/ Shipping/ View for a seller and validate 
            Console.WriteLine($"If you are new seller, you need to add your information type A");
            Console.WriteLine($"If you saved your information and want delete it type D");
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
                        Console.WriteLine("Enter your username");
                        //Find the information of the seller
                        string user_name = Console.ReadLine();
                        //Check the correct information to be saved fron the Sellers
                        foreach (var item_C in sellers)
                        {
                            if (item_C.UserName1 == user_name)
                            { 
                                while (!correct_data)
                                {
                                    Console.WriteLine("This is your personal information");
                                    seller_actions.Show_info(item_C);
                                    Console.WriteLine("Add the new payment method");
                                    Payment_method new_pay = (Payment_method)payment_Actions.Add_info(item_C.UserName1,"Seller");
                                    payment_Actions.Show_info(new_pay);
                                    DateTime tdy = DateTime.Now;
                                    Console.WriteLine($"Your data saved {tdy} is correct Yes/No?");
                                    string validinformation = Console.ReadLine();
                                    if (validinformation == "Yes")
                                    {
                                        
                                        string id_pay = item_C.UserName1 + new_pay.L4_digit1 as string;
                                        payments.Add(id_pay, new_pay);
                                        correct_data = true;
                                    }
                                    else
                                    {
                                        correct_data = false;
                                    }
                                }
                            }
                        }
                        //Update the list
                        Main_menu.paymethod_list = payments;
                        //Update the customer list that is a txt file saved from the list
                        files_rw.Write_file("Paymethod Gateway", Main_menu.paymethod_list,Main_menu.fullpath);
                        validoption = true;
                        break;

                    case 'D':
                        Console.WriteLine("Enter your username");
                        //Find the information of the seller
                        user_name = Console.ReadLine();
                        Console.WriteLine("Enter your payment_id (username + last 4 digits of the card)");
                        //Find the paymethod of the seller
                        string payment_id = Console.ReadLine();
                        foreach (var item_C in sellers)
                        {
                            if (item_C.UserName1 == user_name)
                            {
                                // Validate with the password maximun in three attempts to erase the data
                                Console.WriteLine("If you want to erase your information enter your password");
                                string key_customer = Console.ReadLine();
                                int attempts = 0;
                                while (attempts < 3)
                                {
                                    if (item_C.Password1 == key_customer)
                                    {
                                        payment_Actions.Delete_info(payment_id);
                                        Console.WriteLine("Personal Information Deleted");
                                        files_rw.Write_file("Paymethod Gateway", Main_menu.paymethod_list,Main_menu.fullpath);
                                        attempts += 3;
                                    }
                                    else
                                    {
                                        attempts++;
                                        Console.WriteLine($"Incorrect pasword, try again remain attempst {3 - attempts}");
                                        key_customer = Console.ReadLine();
                                    }
                                }
                            }
                        }
                        validoption = true;
                        break;

                    case 'V':
                        Console.WriteLine("Enter your username");
                        //Find the information of the seller
                        user_name = Console.ReadLine();
                        Console.WriteLine("Enter your payment_id (username + last 4 digits of the card)");
                        //Find the paymethod of the seller
                        payment_id = Console.ReadLine();
                        foreach (var item_C in sellers)
                        {
                            if (item_C.UserName1 == user_name)
                            {
                                //Validating the display of the data with the password with maximun 3 attempts
                                Console.WriteLine("If you want to view your information enter your password");
                                string key_customer = Console.ReadLine();
                                int attempts = 0;
                                while (attempts < 3)
                                {
                                    if (item_C.Password1 == key_customer)
                                    {
                                        Console.WriteLine($"This is your information saved {item_C.Add_date1}");
                                        seller_actions.Show_info(item_C);
                                         payment_Actions.Show_info(payment_id); 
                                        Console.WriteLine("If is not correct deleted your data and add the new one");
                                        attempts += 3;
                                    }
                                    else
                                    {
                                        attempts++;
                                        Console.WriteLine($"Incorrect pasword, try again remain attempst {3 - attempts}");
                                        key_customer = Console.ReadLine();
                                    }
                                }
                            }
                        }
                        validoption = true;
                        break;
                    default:
                        Console.WriteLine("Option not exist, reenter the value");
                        Console.WriteLine($"If you are new customer, you need to add your information type A");
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
