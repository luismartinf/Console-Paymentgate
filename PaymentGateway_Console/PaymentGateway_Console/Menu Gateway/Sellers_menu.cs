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
            Transaction_BO trans_Actions = new Transaction_BO();
            Transfer_BO transfer_Actions = new Transfer_BO();
            InexistentUserException userException = new InexistentUserException();

            //path where is saved the file  (@"C:\Users\luis.martin\Downloads)
            //Read the file with the Sellers information
            List<Seller> sellers = (List<Seller>)files_rw.Read_list("Sellers Gateway",Main_menu.fullpath, "Sellers");
            SortedList<string, Payment_method> payments = (SortedList<string, Payment_method>)files_rw.Read_list("Paymethod Gateway",Main_menu.fullpath, "Paymethod");
            Dictionary<long, Transaction> trans_list = (Dictionary<long, Transaction>)files_rw.Read_list("Transaction Gateway",Main_menu.fullpath, "Transaction");
            SortedList<long, Transfer_Bank> transfers = (SortedList<long, Transfer_Bank>)files_rw.Read_list("Transfer Gateway",Main_menu.fullpath, "Transfer");

            // Sellect one of the five possible actions Add/ Delete/ Shipping/ View/ Cancel for a seller and validate 
            Console.WriteLine($"If you are new seller, you need to add your information type A");
            Console.WriteLine($"If you saved your information and want delete it type D");
            Console.WriteLine($"If you saved your information and want new shipping type S");
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
                    //Check the correct information to be saved fron the Sellers
                    while (!correct_data)
                    {
                        //add seller information and payment method
                        Seller new_seller = (Seller)seller_actions.Add_info(obj);
                        Payment_method new_pay = (Payment_method)payment_Actions.Add_info(new_seller.UserName1, "Seller");

                        //show seller information and payment method
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
                    Console.WriteLine("If you want to shipping something enter again with your user and password an select new shipping");

                    //Update the list
                    Main_menu.sellers_list = sellers;
                    Main_menu.paymethod_list = payments;

                    //Update the sellers list that is a txt file saved from the list
                    files_rw.Write_file("Sellers Gateway", Main_menu.sellers_list, Main_menu.fullpath);
                    files_rw.Write_file("Paymethod Gateway", Main_menu.paymethod_list, Main_menu.fullpath);
                    validoption = true;
                }
                else if (options == 'D' || options == 'S' || options == 'V' || options == 'T' || options == 'C')
                {  
                    // Search the sellers information
                    Console.WriteLine("Enter your username");
                    string user_name = Console.ReadLine();
                    List<string> pay_del = new List<string>();
                    int attempts = 0;

                    try { userException.InexistentUser(user_name, 'S'); }
                    catch
                    {
                        attempts += 3;
                        Console.WriteLine($"User {user_name} does not exist add a new user");
                    }

                    foreach (var item_C in sellers)
                    {
                        if (item_C.UserName1 == user_name)
                        {   
                            // Find all the paymethods for a seller to delete the if erase the user
                            foreach (var pay in payments.Keys)
                            {
                                if (pay.Contains(user_name))
                                { pay_del.Add(pay); }
                            }

                            //Confirm to update the information with the password with 3 attempts 
                            Console.WriteLine("Please enter your password");
                            string key_sellers = Console.ReadLine();
                            while (attempts < 3)
                            {
                                if (item_C.Password1 == key_sellers)
                                {
                                    switch (options)
                                    {
                                        case 'D':
                                            //Delete seller information
                                            foreach (var dpay in pay_del)
                                            { payment_Actions.Delete_info(dpay); }
                                            pay_del.Clear();
                                            seller_actions.Delete_info(user_name);
                                            Console.WriteLine("Personal Information Deleted");
                                            files_rw.Write_file("Sellers Gateway", Main_menu.sellers_list, Main_menu.fullpath);
                                            files_rw.Write_file("Paymethod Gateway", Main_menu.paymethod_list, Main_menu.fullpath);
                                            break;

                                        case 'S':
                                            // Confirm the trasaction and sellerr information 
                                            Console.WriteLine("This is your information");
                                            seller_actions.Show_info(item_C);
                                            Console.WriteLine("select payment Id (username plus 4 las digits of the card)");
                                            string pay_key = Console.ReadLine();
                                            try { payment_Actions.Show_info(pay_key); }
                                            catch { Console.WriteLine("paymethod doesnot exist add a new one or reenter to the app"); }
                                            // Do the new transaction
                                            Console.WriteLine("Enter the Item purchase given by the Online shopping");
                                            
                                            int item_p = Convert.ToInt32(Console.ReadLine());
                                            Transaction new_trans = (Transaction)trans_Actions.Add_trans(item_p, "Seller", item_C.UserName1);
                                            long trans_id = new_trans.Id_transaction1(item_p, "Seller");
                                            trans_Actions.Update_trans(trans_id, "Shipment");
                                            try
                                            {
                                                trans_list[trans_id - 1000000000].Notes = $"Shipping Item:{new_trans.Item1}";
                                                trans_list.Add(trans_id, new_trans);
                                            }
                                            catch { Console.WriteLine("Transaction doesnot exist update the transaction purchase, shiping a new item or reenter to the app"); }

                                            //Create te tranfer to gave the money of the sale 
                                            Transfer_Bank new_transfer = (Transfer_Bank)transfer_Actions.Add_trans(trans_id, "Seller", item_C.UserName1);
                                            long transfer_id = new_transfer.Id_transfer1(item_p, "Seller");
                                            transfers.Add(transfer_id, new_transfer);
                                            Main_menu.transfer_list = transfers;

                                            // Return the information of the transaction
                                            files_rw.Write_file("Transaction Gateway", Main_menu.transaction_list, Main_menu.fullpath);
                                            files_rw.Write_file("Transfer Gateway", Main_menu.transfer_list, Main_menu.fullpath);
                                            Console.WriteLine($"Succesful shipping the Id_transaction of your shipping is {trans_id}");
                                            trans_Actions.Show_trans(new_trans);
                                            break;

                                        case 'V':
                                           
                                            // Show the information of the seller
                                            Console.WriteLine($"This is your information saved {item_C.Add_date1}");
                                            seller_actions.Show_info(item_C);
                                            foreach (var dpay in pay_del)
                                            { payment_Actions.Show_info(dpay); }
                                            pay_del.Clear();
                                            Console.WriteLine("If is not correct deleted your data and add the new one");
                                            break;

                                        case 'T':
                                            Console.WriteLine("Select an Option type All for search all the transactions, type One for seach one transaction");
                                            string trans_ops = Console.ReadLine();
                                            if (trans_ops == "All")
                                            {
                                                List<long> trans_don = new List<long>();

                                                // Find sellers information to display
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
                                                trans_Actions.Show_trans(tdi);
                                            }
                                            break;

                                        case 'c':

                                            //show information for on the transactions
                                            Console.WriteLine("Enter your transaction id");
                                            long td = Convert.ToInt64(Console.ReadLine());
                                            try
                                            {
                                                trans_Actions.Cancel_trans(td, "Seller");
                                                transfer_Actions.Cancel_trans(td, "Seller");
                                                Console.WriteLine("Transacton canceled succesfully");
                                            }
                                            catch { Console.WriteLine("Transaction doesnot exist update the transaction purchase, shiping a new item or reenter to the app"); }
                                            //write files
                                            files_rw.Write_file("Transaction Gateway", Main_menu.transaction_list, Main_menu.fullpath);
                                            files_rw.Write_file("Transfer Gateway", Main_menu.transfer_list, Main_menu.fullpath);
                                            break;
                                    }
                                    attempts += 3;
                                }
                                else
                                {
                                    attempts++;
                                    Console.WriteLine($"Incorrect pasword, try again remain attempst {3 - attempts}");
                                    key_sellers = Console.ReadLine();
                                }
                            }
                        }
                    }
                    validoption = true;
                }
                else
                {
                    Console.WriteLine("Option not exist, reenter the value");
                    Console.WriteLine($"If you are new seller, you need to add your information type A");
                    Console.WriteLine($"If you saved your information and want delete it type D");
                    Console.WriteLine($"If you saved your information and want new Shipping type S");
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
