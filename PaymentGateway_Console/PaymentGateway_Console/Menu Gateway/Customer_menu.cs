using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    class Customer_menu
    {
        public void Menu_customers()
        {


            Read_write_files files_rw = new Read_write_files();
            Customer_BO customer_actions = new Customer_BO();
            Payment_BO payment_Actions = new Payment_BO();
            Transaction_BO trans_Actions = new Transaction_BO();
            Transfer_BO transfer_Actions = new Transfer_BO();
            InexistentUserException userException = new InexistentUserException();

            //read the database of customers
            List<Customer> customers = (List<Customer>)files_rw.Read_list("Customers Gateway", Main_menu.fullpath, "Customers");
            SortedList<string, Payment_method> payments = (SortedList<string, Payment_method>)files_rw.Read_list("Paymethod Gateway", Main_menu.fullpath, "Paymethod");
            Dictionary<long, Transaction> trans_list = (Dictionary<long, Transaction>)files_rw.Read_list("Transaction Gateway", Main_menu.fullpath, "Transaction");

            //    Validate one of the six possible actions Add/ Delete/ Purchase/ View / Confirmed delivery/ Cancel  
            Console.WriteLine($"If you need to add your information type A");
            Console.WriteLine($"If you saved your information and want delete it type D");
            Console.WriteLine($"If you saved your information and want new transaction(Purchase) type P");
            Console.WriteLine($"If you saved your information and want to view type V");
            Console.WriteLine($"If you want to search or update a transaction in progress type T");
            Console.WriteLine($"If you want to Cancel a transaction in progress type C");
            char options = Convert.ToChar(Console.ReadLine());
            bool validoption = false;
                
            while (!validoption)
            {
                if (options == 'A')
                {
                    bool correct_data = false;
                    object obj = "";

                    //Check if the information is correct otherwise reenter the information
                    while (!correct_data)
                    {
                        //add customer information ans payment method
                        Customer new_customer = (Customer)customer_actions.Add_info(obj);
                        Payment_method new_pay = (Payment_method)payment_Actions.Add_info(new_customer.UserName1, "Customer");
                        Console.WriteLine("This is your personal information");
                        customer_actions.Show_info(new_customer);

                        //show customer information ans payment method
                        payment_Actions.Show_info(new_pay);
                        Console.WriteLine($"Your data saved {new_customer.Add_date1} is correct Yes/No?");
                        string validinformation = Console.ReadLine();
                        if (validinformation == "Yes")
                        {
                            customers.Add(new_customer);
                            string id_pay = new_customer.UserName1 + new_pay.L4_digit1 as string;
                            payments.Add(id_pay, new_pay);
                            correct_data = true;
                        }
                        else
                        {
                            correct_data = false;
                        }
                    }
                    Console.WriteLine("If you want to purchase something enter again with your user and password an select new purchase");

                    //Update the list
                    Main_menu.customers_list = customers;
                    Main_menu.paymethod_list = payments;

                    //Update the customer list that is a txt file saved from the list
                    files_rw.Write_file("Customers Gateway", Main_menu.customers_list, Main_menu.fullpath);
                    files_rw.Write_file("Paymethod Gateway", Main_menu.paymethod_list, Main_menu.fullpath);
                    validoption = true;
                }
                else if (options == 'D' || options == 'P' || options == 'V' || options == 'T' || options == 'C')
                {
                    // Search the customer information
                    Console.WriteLine("Enter your username");
                    string user_name = Console.ReadLine();
                    List<string> pay_del = new List<string>();
                    int attempts = 0;

                    try { userException.InexistentUser(user_name, 'C'); }
                    catch
                    {
                        attempts += 3;
                        Console.WriteLine($"User {user_name} does not exist add a new user");
                    }

                    foreach (var item_C in customers)
                    {
                        if (item_C.UserName1 == user_name)
                        {  
                            // Find all the paymethods for a customer to delete the if erase the user
                            foreach (var pay in payments.Keys)
                            {
                                if (pay.Contains(user_name))
                                { pay_del.Add(pay); }
                            }

                            //Confirm to update the information with the password with 3 attempts 
                            Console.WriteLine("Please enter your password");
                            string key_customer = Console.ReadLine();
                            while (attempts < 3)
                            {
                                if (item_C.Password1 == key_customer)
                                {
                                    switch (options)
                                    {
                                        case 'D':
                                            //Delete customer information
                                            foreach (var dpay in pay_del)
                                            { payment_Actions.Delete_info(dpay); }
                                            pay_del.Clear();
                                            customer_actions.Delete_info(user_name);
                                            Console.WriteLine("Personal Information Deleted");
                                            files_rw.Write_file("Customers Gateway", Main_menu.customers_list, Main_menu.fullpath);
                                            files_rw.Write_file("Paymethod Gateway", Main_menu.paymethod_list, Main_menu.fullpath);
                                            break;

                                        case 'P':
                                            // Confirm the trasaction and customer information 
                                            Console.WriteLine("This is your information");
                                            customer_actions.Show_info(item_C);
                                            Console.WriteLine("select payment Id (username plus 4 las digits of the card)");
                                            string pay_key = Console.ReadLine();
                                            try { payment_Actions.Show_info(pay_key); }
                                            catch { Console.WriteLine("payment Id doesnot exist add first then return to purchase"); }
                                            
                                            // Do the new transaction
                                            Console.WriteLine("Enter the Item purchase given by the Online shopping");
                                            int item_p = Convert.ToInt32(Console.ReadLine());
                                            Transaction new_trans = (Transaction)trans_Actions.Add_trans(item_p, "Customer", item_C.UserName1);
                                            long trans_id = new_trans.Id_transaction1(item_p, "Customer");
                                            trans_list.Add(trans_id, new_trans);
                                            Main_menu.transaction_list = trans_list;
                                           
                                            // Return the information of the transaction
                                            files_rw.Write_file("Transaction Gateway", Main_menu.transaction_list, Main_menu.fullpath);
                                            Console.WriteLine($"Succesful Purchase the Id_transaction of your purchase is {trans_id}");
                                            trans_Actions.Show_trans(new_trans);
                                            break;

                                        case 'V':

                                            //show information for customer
                                            Console.WriteLine($"This is your information saved {item_C.Add_date1}");
                                            customer_actions.Show_info(item_C);
                                           
                                            //show information for all the payment methods
                                            foreach (var dpay in pay_del)
                                            { payment_Actions.Show_info(dpay); }
                                            pay_del.Clear();
                                            Console.WriteLine("If is not correct deleted your data and add the new one");
                                            break;

                                        case 'T':
                                            Console.WriteLine("Select an Option type All for search all the transactions, type One for seach one transaction, finally type Recd to confirm the delivered of the purchase");
                                            string trans_ops = Console.ReadLine();
                                            if (trans_ops == "All")
                                            {
                                                List<long> trans_don = new List<long>();
                                                // Find customer information to display
                                                foreach (var item_t in trans_list)
                                                {
                                                    if (item_t.Value.Username == item_C.UserName1)
                                                    { trans_don.Add(item_t.Key); }
                                                }
                                               
                                                //show information for all the transactions
                                                foreach (var tdi in trans_don)
                                                {
                                                    Console.WriteLine($"Transaction id {tdi}:");
                                                    trans_Actions.Show_trans(tdi);
                                                }
                                                trans_don.Clear();
                                            }
                                            else if (trans_ops == "One")
                                            {
                                                //show information for one the transactions
                                                Console.WriteLine("Enter your transaction id");
                                                long tdi = Convert.ToInt64(Console.ReadLine());
                                                try { trans_Actions.Show_trans(tdi); }
                                                catch { Console.WriteLine("Transaction doesnot exist make a new purchase or reenter to the app"); }
                                            }
                                            else if (trans_ops == "Recd")
                                            {
                                                //Confirm recived the the purchase
                                                Console.WriteLine("Enter your transaction id");
                                                long tdi = Convert.ToInt64(Console.ReadLine());
                                                try
                                                {
                                                    trans_Actions.Update_trans(tdi, "Recd");
                                                    Console.WriteLine("Delivered confirmation complete");
                                                }
                                                catch { Console.WriteLine("Transaction doesnot exist make a new purchase or reenter to the app"); }
                                                files_rw.Write_file("Transaction Gateway", Main_menu.transaction_list, Main_menu.fullpath);
                                            }
                                            break;

                                        case 'c':
                                            //show information for on the transactions
                                            Console.WriteLine("Enter your transaction id");
                                            long td = Convert.ToInt64(Console.ReadLine());
                                            try
                                            {
                                                trans_Actions.Cancel_trans(td, "Customer");
                                                transfer_Actions.Cancel_trans(td, "Seller");
                                                Console.WriteLine("Transacton canceled succesfully");
                                            }
                                            catch { Console.WriteLine("Transaction doesnot exist make a new purchase or reenter to the app"); }
                                            //write files
                                            files_rw.Write_file("Transaction Gateway", Main_menu.transaction_list, Main_menu.fullpath);
                                            files_rw.Write_file("Tranfer Gateway", Main_menu.transfer_list, Main_menu.fullpath);
                                            break;
                                    }
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
                }
                else
                {
                    Console.WriteLine("Option not exist, reenter the value");
                    Console.WriteLine($"If you are new customer, you need to add your information type A");
                    Console.WriteLine($"If you saved your information and want delete it type D");
                    Console.WriteLine($"If you saved your information and want new purchase type P");
                    Console.WriteLine($"If you saved your information and want to view type V");
                    Console.WriteLine($"If you want to search or update a transaction in progress type T");
                    Console.WriteLine($"If you want to Cancel a transaction in progress type C");
                    options = Convert.ToChar(Console.ReadLine());
                    validoption = false;
                }
            }
        }
    }
}
