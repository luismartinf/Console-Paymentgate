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
            //read the database of customers
            List<Customer> customers = (List<Customer>)files_rw.Read_list("Customers Gateway", @"C:\Users\luis.martin\Downloads\", "Customers");
            SortedList<string, Payment_method> payments = (SortedList<string, Payment_method>)files_rw.Read_list("Paymethod Gateway", @"C:\Users\luis.martin\Downloads\", "Paymethod");
            Dictionary<long, Transaction> trans_list = (Dictionary<long, Transaction>)files_rw.Read_list("Transaction Gateway", @"C:\Users\luis.martin\Downloads\", "Transaction");
            SortedList<long, Transfer_Bank> transfers = (SortedList<long, Transfer_Bank>)files_rw.Read_list("Transfer Gateway", @"C:\Users\luis.martin\Downloads\", "Transfer");

            Console.WriteLine($"If you need to add your information type A");
            Console.WriteLine($"If you saved your information and want delete it type D");
            Console.WriteLine($"If you saved your information and want new transaction(Purchase) type P");
            Console.WriteLine($"If you saved your information and want to view type V");
            Console.WriteLine($"If you want to search or update a transaction in progress type T");
            Console.WriteLine($"If you want to Cancel a transaction in progress type C");
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
                            Customer new_customer = (Customer)customer_actions.Add_info(obj);
                            Payment_method new_pay = (Payment_method)payment_Actions.Add_info(new_customer.UserName1, "Customer");
                            //add customer information
                            Console.WriteLine("This is your personal information");
                            customer_actions.Show_info(new_customer);
                            //add paymentmethod
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
                        files_rw.Write_file("Customers Gateway", Main_menu.customers_list, @"C:\Users\luis.martin\Downloads\");
                        files_rw.Write_file("Paymethod Gateway", Main_menu.paymethod_list, @"C:\Users\luis.martin\Downloads\");
                        validoption = true;
                        break;

                    case 'D':
                        // Search the customer information
                        Console.WriteLine("Enter your username");
                        string user_name = Console.ReadLine();
                        List<string> pay_del = new List<string>();
                        foreach (var item_C in customers)
                        {
                            if (item_C.UserName1 == user_name)
                            {   // Find all the paymethods for a customer to delete the if erase the user

                                foreach (var pay in payments.Keys)
                                {
                                    if (pay.Contains(user_name))
                                    { pay_del.Add(pay); }
                                }
                                //Confirm to erase the information with the password with 3 attempts 
                                Console.WriteLine("If you want to erase your information enter your password");
                                string key_customer = Console.ReadLine();
                                int attempts = 0;
                                while (attempts < 3)
                                {
                                    if (item_C.Password1 == key_customer)
                                    {
                                        //Delete customer information
                                        foreach (var dpay in pay_del)
                                        { payment_Actions.Delete_info(dpay); }
                                        pay_del.Clear();
                                        customer_actions.Delete_info(user_name);
                                        Console.WriteLine("Personal Information Deleted");
                                        files_rw.Write_file("Customers Gateway", Main_menu.customers_list, @"C:\Users\luis.martin\Downloads\");
                                        files_rw.Write_file("Paymethod Gateway", Main_menu.paymethod_list, @"C:\Users\luis.martin\Downloads\");
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

                    case 'P':
                        Console.WriteLine("Enter your username");
                        //find the customer information to do a new purchase
                        user_name = Console.ReadLine();
                        foreach (var item_C in customers)
                        {
                            if (item_C.UserName1 == user_name)
                            {

                                Console.WriteLine("If you want to purchase enter your password");
                                string key_customer = Console.ReadLine();
                                int attempts = 0;
                                while (attempts < 3)
                                {
                                    if (item_C.Password1 == key_customer)
                                    {
                                        // Confirm the trasaction and customer information with password 3 attempts
                                        Console.WriteLine("This is your information");
                                        customer_actions.Show_info(item_C);
                                        Console.WriteLine("select payment Id (username plus 4 las digits of the card)");
                                        string pay_key = Console.ReadLine();
                                        payment_Actions.Show_info(pay_key);
                                        // Do the new transaction
                                        Console.WriteLine("Enter the Item purchase given by the Online shopping");
                                        int item_p = Convert.ToInt32(Console.ReadLine());
                                        Transaction new_trans=(Transaction)trans_Actions.Add_trans(item_p, "Customer", item_C.UserName1);
                                        long trans_id = new_trans.Id_transaction1(item_p, "Customer");
                                        trans_list.Add(trans_id, new_trans);
                                        Main_menu.transaction_list = trans_list;
                                        // Return the information of the transaction
                                        files_rw.Write_file("Transaction Gateway", Main_menu.transaction_list, @"C:\Users\luis.martin\Downloads\");
                                        Console.WriteLine($"Succesful Purchase the Id_transaction of your purchase is {trans_id}");
                                        trans_Actions.Show_trans(new_trans);
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
                        pay_del = new List<string>();
                        // Find customer information to display
                        foreach (var item_C in customers)
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
                                string key_customer = Console.ReadLine();
                                int attempts = 0;
                                while (attempts < 3)
                                {
                                    if (item_C.Password1 == key_customer)
                                    {   //show information for customer
                                        Console.WriteLine($"This is your information saved {item_C.Add_date1}");
                                        customer_actions.Show_info(item_C);
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
                                        key_customer = Console.ReadLine();
                                    }
                                }
                            }
                        }
                        validoption = true;
                        break;

                    case 'T':
                        Console.WriteLine("Select an Option type All for search all the transactions, type One for seach one transaction, finally type Recd to confirm the delivered of the purchase");
                        string trans_ops = Console.ReadLine();
                        if (trans_ops == "All")
                        {
                            Console.WriteLine("Enter your username");
                            user_name = Console.ReadLine();
                            List<long> trans_don = new List<long>();
                            // Find customer information to display
                            foreach (var item_t in trans_list)
                            {
                                if (item_t.Value.Username == user_name)
                                { trans_don.Add(item_t.Key); }
                            }
                            //view the information
                            foreach (var item_C in customers)
                            {
                                if (item_C.UserName1 == user_name)
                                {
                                    Console.WriteLine("If you want to view your transactions enter your password");
                                    string key_customer = Console.ReadLine();
                                    int attempts = 0;
                                    while (attempts < 3)
                                    {

                                        if (item_C.Password1 == key_customer)
                                        {   //show information for all the transactions
                                            foreach (var td in trans_don)
                                            {
                                                Console.WriteLine($"Transaction id {td}:");
                                                trans_Actions.Show_trans(td);
                                            }
                                                trans_don.Clear();
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

                        }
                        else if (trans_ops == "One")
                        {
                            Console.WriteLine("Enter your username");
                            user_name = Console.ReadLine();
                            Console.WriteLine("If you want to view your transactions enter your password");
                            string key_customer = Console.ReadLine();
                            int attempts = 0;
                            foreach (var item_C in customers)
                            {
                                if (item_C.UserName1 == user_name)
                                { 
                                    while (attempts < 3)
                                    {

                                        if (item_C.Password1 == key_customer)
                                        {   //show information for on the transactions
                                            Console.WriteLine("Enter your transaction id");
                                            long td = Convert.ToInt64(Console.ReadLine());
                                            trans_Actions.Show_trans(td);
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

                        }
                        else if (trans_ops == "Recd")
                        {
                            Console.WriteLine("Enter your username");
                            user_name = Console.ReadLine();
                            Console.WriteLine("If you want to search your transactions enter your password");
                            string key_customer = Console.ReadLine();
                            int attempts = 0;
                            foreach (var item_C in customers)
                            {
                                if (item_C.UserName1 == user_name)
                                {
                                    while (attempts < 3)
                                    {

                                        if (item_C.Password1 == key_customer)
                                        {   //show information for all the ptransactions
                                            Console.WriteLine("Enter your transaction id");
                                            long td = Convert.ToInt64(Console.ReadLine());
                                            trans_Actions.Update_trans(td, "Recd");
                                            files_rw.Write_file("Transaction Gateway", Main_menu.transaction_list, @"C:\Users\luis.martin\Downloads\");
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
                        }
                        validoption = true;
                        break;

                    case 'c':
                        Console.WriteLine("Enter your username");
                        user_name = Console.ReadLine();
                        Console.WriteLine("If you want to cancel your transactions enter your password");
                        string key_cust = Console.ReadLine();
                        int attem = 0;
                        foreach (var item_C in customers)
                        {
                            if (item_C.UserName1 == user_name)
                            {
                                while (attem < 3)
                                {

                                    if (item_C.Password1 == key_cust)
                                    {   //show information for on the transactions
                                        Console.WriteLine("Enter your transaction id");
                                        long td = Convert.ToInt64(Console.ReadLine());
                                        trans_Actions.Cancel_trans(td,"Customer");
                                        transfer_Actions.Cancel_trans(td, "Seller");
                                        //write files
                                        files_rw.Write_file("Transaction Gateway", Main_menu.transaction_list, @"C:\Users\luis.martin\Downloads\");
                                        files_rw.Write_file("Tranfer Gateway", Main_menu.transfer_list, @"C:\Users\luis.martin\Downloads\");
                                        attem += 3;
                                    }
                                    else
                                    {
                                        attem++;
                                        Console.WriteLine($"Incorrect pasword, try again remain attempst {3 - attem}");
                                        key_cust = Console.ReadLine();
                                    }
                                }
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Option not exist, reenter the value");
                        Console.WriteLine($"If you are new customer, you need to add your information type A");
                        Console.WriteLine($"If you saved your information and want delete it type D");
                        Console.WriteLine($"If you saved your information and want new purchase type P");
                        Console.WriteLine($"If you saved your information and want to view type V");
                        Console.WriteLine($"If you want to search or update a transaction in progress type T");
                        Console.WriteLine($"If you want to Cancel a transaction in progress type C");
                        options = Convert.ToChar(Console.ReadLine());
                        validoption = false;
                        break;
                }
            }
        }
    }
}
