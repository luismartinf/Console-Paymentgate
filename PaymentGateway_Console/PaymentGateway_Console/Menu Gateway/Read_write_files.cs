using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    //Class to read and write the Client/ seller and Puchase tct files from the arrays
    class Read_write_files
    {
        public object Read_list(string text_file, string filepath, string data_list)
        {
            //path to read the file
            string path = filepath + text_file;
            string[] clients_file;
            // Validate if exist each file acording to the data provide in the string (Clients/ Sellers/ Payments/Transactions/Transfers)
            //// If the file exist read the data in the file and return an array with the corresponding objects
            //// else create a new array with the corresponding class
            if (File.Exists(path))
            {
               // array of strings with all the files saved each one in one line
               clients_file = File.ReadAllLines(path);
               int len_clients = clients_file.Length;
               switch (text_file)
               {
                    case "Clients Gateway":
                        List<Client> list_addedc = new List<Client>();
                        for (int n_clients = 0; n_clients < len_clients; n_clients++)
                        {
                            //string array with each value for all the fields of a client
                            string[] client_added = new string[6];
                            string str_field = "";
                            int num_field = 0;
                            foreach (char chart_c in clients_file[n_clients])
                            {
                                if (chart_c == ',')
                                {
                                    client_added[num_field] = str_field;
                                    str_field = "";
                                    num_field++;
                                }
                                else
                                { str_field = str_field + chart_c; }
                            }

                            //Reading and creating the array for all the clients
                            Client client_s = new Client
                            {
                                UserName1 = client_added[0],
                                First_N1 = client_added[1],
                                Last_N1 = client_added[2],
                                Add_date1 = DateTime.Parse(client_added[3]),
                                Password1 = client_added[4],
                                Email1 = client_added[5],
                            };
                            list_addedc.Add(client_s);
                        }
                        //Update client list
                        Main_menu.clients_list = list_addedc;
                        return list_addedc;
                        

                    case "Paymethod Gateway":

                        SortedList<string, Payment_method> list_addedp = new SortedList<string, Payment_method>();
                        for (int n_clients = 0; n_clients < len_clients; n_clients++)
                        {
                            // string array with each value for all the fields of a paymethod
                            string[] client_added = new string[14];
                            string str_field = "";
                            int num_field = 0;
                            foreach (char chart_c in clients_file[n_clients])
                            {
                                if (chart_c == ',')
                                {
                                    client_added[num_field] = str_field;
                                    str_field = "";
                                    num_field++;
                                }
                                else
                                { str_field = str_field + chart_c; }

                            }
                            //reading and creating the array for all the paymethods
                            Payment_method payment_s = new Payment_method()
                            {
                                User_name1 = client_added[1],
                                User_type1= client_added[2],
                                Type_card1 = client_added[3],
                                Card_N1 = Convert.ToInt64(client_added[4]),
                                L4_digit1= Convert.ToInt64(client_added[5]),
                                Exp_date1=DateTime.Parse(client_added[6]),
                                CVV1=Convert.ToInt16(client_added[7]),
                                Country1 = client_added[8],                                
                                Add_11 = client_added[9],
                                Add_21 = client_added[10],
                                City1 = client_added[11],
                                State1 = client_added[12],
                                CP1 = client_added[13],
                                
                            };
                            list_addedp.Add(client_added[0],payment_s);
                        }
                        //Update paymethod list
                        Main_menu.paymethod_list = list_addedp;
                        return list_addedp;
                        

                    case "Sellers Gateway":
                        
                        List<Seller> list_addeds = new List<Seller>();
                        for (int n_clients = 0; n_clients < len_clients; n_clients++)
                        {
                            // string array with each value for all the fields of a seller
                            string[] client_added = new string[7];
                            string str_field = "";
                            int num_field = 0;
                            foreach (char chart_c in clients_file[n_clients])
                            {
                                if (chart_c == ',')
                                {
                                    client_added[num_field] = str_field;
                                    str_field = "";
                                    num_field++;
                                }
                                else
                                { str_field = str_field + chart_c; }

                            }
                            //reading and creating the array for all the sellers
                            Seller seller_s = new Seller()
                            {
                                UserName1 = client_added[0],
                                First_N1 = client_added[1],
                                Last_N1 = client_added[2],
                                Add_date1 = DateTime.Parse(client_added[3]),
                                Password1 = client_added[4],
                                Email1 = client_added[5],
                                OS_Url1 = client_added[6],
                                
                            };

                            list_addeds.Add(seller_s);
                        }
                        Main_menu.sellers_list = list_addeds;
                        return list_addeds;

                    default:
                        Console.WriteLine("error name");
                        return"c";
                        ////    }
                        ////    else
                        ////    {
                        ////        Purchase[] List_added = new Purchase[Len_clients];
                        ////        for (int n_clients = 0; n_clients < Len_clients; n_clients++)
                        //{
                        //    string[] client_added = new string[5];
                        //    string str_field = "";
                        //    int num_field = 0;
                        //    foreach (char chart_c in Clients_file[n_clients])
                        //    {
                        //        if (chart_c == ',')
                        //        {
                        //            client_added[num_field] = str_field;
                        //            str_field = "";
                        //            num_field++;
                        //        }
                        //        else
                        //        { str_field = str_field + chart_c; }

                        //    }
                        //    //Reading and creating the array for all the clients
                        //    Purchase purchase_s = new Purchase
                        //    {
                        //        Order_number1 = Convert.ToInt32(client_added[0]),
                        //        Purchase_item1 = Convert.ToInt32(client_added[1]),
                        //        Purchase_amount1 = Convert.ToDouble(client_added[2]),
                        //        Shipping_item1 = Convert.ToInt32(client_added[3]),
                        //        Shipping_cost1 = Convert.ToDouble(client_added[4])
                        //    };
                        //    List_added[n_clients] = purchase_s;
                        //}
                        //        Menu.Purchase_list = List_added;
                        //        return List_added;
                       
               }
            }
            //create new files
            else
            {
                if (data_list == "Clients")
                {
                    List<Client> list_added = new List<Client>();
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
                    Dictionary<string, Transaction> list_added = new Dictionary<string, Transaction>();
                    return list_added;
                }
                else  
                {
                    SortedList<string, Transfer_Bank> list_added = new SortedList<string, Transfer_Bank>();
                    return list_added;
                }
            }


        }

        //Method to write a Client txt file
        public void Write_file(string text_file, List<Client> clients_saved, string filepath)
        {
            string path = filepath + text_file;
            using (TextWriter tw = new StreamWriter(path))
            { foreach (object client_s in clients_saved)
                { tw.WriteLine(value: client_s); }
            }

        }
        //Method to write a seller txt file
        public void Write_file(string text_file, List<Seller> seller_saved, string filepath)
        {
            string path = filepath + text_file;
            using (TextWriter tw = new StreamWriter(path))
            {
                foreach (object seller_s in seller_saved)
                { tw.WriteLine(seller_s); }
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
        public void Write_file(string text_file, SortedList<string, Transfer_Bank> transfers_saved, string filepath)
        {
            string path = filepath + text_file;
            using (TextWriter tw = new StreamWriter(path))
            {
                foreach (KeyValuePair<string, Transfer_Bank> trf_s in transfers_saved)
                { tw.WriteLine($"{trf_s.Key},{trf_s.Value}"); }
            }
        }

        //Method to write the Transactions txt file
        public void Write_file(string text_file, Dictionary<string, Transaction> trans_saved, string filepath)
        {
            string path = filepath + text_file;
            using (TextWriter tw = new StreamWriter(path))
            {
                foreach (KeyValuePair<string, Transaction> trans_s in trans_saved)
                { tw.WriteLine($"{trans_s.Key},{trans_s.Value}"); }
            }
        }



    }
}
