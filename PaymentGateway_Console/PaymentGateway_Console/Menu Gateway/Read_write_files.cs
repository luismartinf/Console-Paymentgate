using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    //Class to read and write the Customer/ seller and Puchase tct files from the arrays
    class Read_write_files
    {
        public object Read_list(string text_file, string filepath, string data_list)
        {
            //path to read the file
            string path = filepath + text_file;
            string[] data_file;
            // Validate if exist each file acording to the data provide in the string (Customers/ Sellers/ Payments/Transactions/Transfers)
            // If the file exist read the data in the file and return an array with the corresponding objects
            // else create a new list with the corresponding class
            if (File.Exists(path))
            {
               // array of strings with all the files saved each one in one line
               data_file = File.ReadAllLines(path);
               int len_data = data_file.Length;
               switch (text_file)
               {
                    case "Customers Gateway":
                        List<Customer> list_addedc = new List<Customer>();
                        for (int n_customers = 0; n_customers < len_data; n_customers++)
                        {
                            //string array with each value for all the fields of a customer
                            string[] customer_added = data_file[n_customers].Split(',');

                            //Reading and creating the list for all the customers
                            Customer customer_s = new Customer(customer_added[0], customer_added[1], customer_added[2], customer_added[3], customer_added[4], customer_added[5]);
                            list_addedc.Add(customer_s);
                        }
                        //Update customer list
                        Main_menu.customers_list = list_addedc;
                        return list_addedc;
                        

                    case "Paymethod Gateway":

                        SortedList<string, Payment_method> list_addedp = new SortedList<string, Payment_method>();
                        for (int n_payments = 0; n_payments < len_data; n_payments++)
                        {
                            // string array with each value for all the fields of a paymethod
                            string[] payment_added = data_file[n_payments].Split(',');
                            
                            //reading and creating the SortedList for all the paymethods
                            Payment_method payment_s = new Payment_method(payment_added[1], payment_added[2], payment_added[3], payment_added[4], payment_added[5], payment_added[6], payment_added[7], payment_added[8], payment_added[9], payment_added[10], payment_added[11], payment_added[12], payment_added[13]);
                            list_addedp.Add(payment_added[0],payment_s);
                        }
                        //Update paymethod list
                        Main_menu.paymethod_list = list_addedp;
                        return list_addedp;
                        

                    case "Sellers Gateway":
                        
                        List<Seller> list_addeds = new List<Seller>();
                        for (int n_sellers = 0; n_sellers < len_data; n_sellers++)
                        {
                            // string array with each value for all the fields of a seller
                            string[] sellers_added = data_file[n_sellers].Split(',');

                            //reading and creating the list for all the sellers
                            Seller seller_s = new Seller(sellers_added[0], sellers_added[1], sellers_added[2], sellers_added[3], sellers_added[4], sellers_added[5], sellers_added[6]);
                            list_addeds.Add(seller_s);
                        }
                        Main_menu.sellers_list = list_addeds;
                        return list_addeds;

                    case "Transaction Gateway":

                        Dictionary<long, Transaction> list_addedt = new Dictionary<long, Transaction>();
                        for (int n_customers = 0; n_customers < len_data; n_customers++)
                        {
                            // string array with each value for all the fields of a paymethod
                            string[] customer_added = data_file[n_customers].Split(',');

                            //reading and creating the SortedList for all the paymethods
                           Transaction trans_s = new Transaction(customer_added[1], customer_added[2], customer_added[3], customer_added[4], customer_added[5], customer_added[6], customer_added[7]);
                            list_addedt.Add(Convert.ToInt64(customer_added[0]), trans_s);
                        }
                        //Update paymethod list
                        Main_menu.transaction_list = list_addedt;
                        return list_addedt;

                    case "Transfer Gateway":

                        SortedList<long, Transfer_Bank> list_addedtf = new SortedList<long, Transfer_Bank>();
                        for (int n_customers = 0; n_customers < len_data; n_customers++)
                        {
                            // string array with each value for all the fields of a paymethod
                            string[] customer_added = data_file[n_customers].Split(',');

                            //reading and creating the SortedList for all the paymethods
                            Transfer_Bank transfer_s = new Transfer_Bank(customer_added[1], customer_added[2], customer_added[3], customer_added[4], customer_added[5]);
                            list_addedtf.Add(Convert.ToInt64(customer_added[0]), transfer_s);
                        }
                        //Update paymethod list
                        Main_menu.transfer_list = list_addedtf;
                        return list_addedtf;


                    default:
                        Console.WriteLine("error name");
                        return"c";
                       
               }
            }
            //create new files
            else
            {
                if (data_list == "Customers")
                {
                    List<Customer> list_added = new List<Customer>();
                    return list_added;
                }
                else if (data_list == "Sellers")
                {
                    List<Seller> list_added = new List<Seller>();
                    return list_added;
                }
                else if (data_list == "Paymethod")
                {
                    SortedList<string, Payment_method> list_added = new SortedList<string, Payment_method>() ;
                    return list_added;
                }
                else if (data_list == "Transaction")
                {
                    Dictionary<long, Transaction> list_added = new Dictionary<long, Transaction>();
                    return list_added;
                }
                else  
                {
                    SortedList<long, Transfer_Bank> list_added = new SortedList<long, Transfer_Bank>();
                    return list_added;
                }
            }


        }

        //Method to write a Customer txt file with the override to string method for each class
        public void Write_file(string text_file, List<Customer> customers_saved, string filepath)
        {
            string path = filepath + text_file;
            using (TextWriter tw = new StreamWriter(path))
            { foreach (object customer_s in customers_saved)
                { tw.WriteLine(value: customer_s); }
            }

        }
        //Method to write a seller txt file
        public void Write_file(string text_file, List<Seller> seller_saved, string filepath)
        {
            string path = filepath + text_file;
            using (TextWriter tw = new StreamWriter(path))
            {
                foreach (object seller_s in seller_saved)
                { tw.WriteLine(value: seller_s); }
            }

        }
        //Method to write the Paymethods txt file
        public void Write_file(string text_file, SortedList<string,Payment_method> payment_saved, string filepath)
        {
            string path = filepath + text_file;
            using (TextWriter tw = new StreamWriter(path))
            {
                foreach ( KeyValuePair<string,Payment_method> pay_s in payment_saved)
                { tw.WriteLine($"{pay_s.Key},{pay_s.Value}"); }
            }
        }
        //Method to write the Transfers txt file
        public void Write_file(string text_file, SortedList<long, Transfer_Bank> transfers_saved, string filepath)
        {
            string path = filepath + text_file;
            using (TextWriter tw = new StreamWriter(path))
            {
                foreach (KeyValuePair<long, Transfer_Bank> trf_s in transfers_saved)
                { tw.WriteLine($"{trf_s.Key},{trf_s.Value}"); }
            }
        }

        //Method to write the Transactions txt file
        public void Write_file(string text_file, Dictionary<long, Transaction> trans_saved, string filepath)
        {
            string path = filepath + text_file;
            using (TextWriter tw = new StreamWriter(path))
            {
                foreach (KeyValuePair<long, Transaction> trans_s in trans_saved)
                { tw.WriteLine($"{trans_s.Key},{trans_s.Value}"); }
            }
        }



    }
}
