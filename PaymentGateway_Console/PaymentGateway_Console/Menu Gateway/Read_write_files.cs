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
            //path to store the file
            //string path = filepath + text_file;
            //string[] Clients_file;
            //// Validate if exist each file acording to the data provide in the string (Clients/ Sellers/ Purchase)
            //// If the file exist read the data in the file and return an array with the corresponding objects
            //// else create a new array with the corresponding class
            //if (File.Exists(path))
            //{
            ////    // array of strings with all the clients Sellers or purchases saved each one in one line
            //    Clients_file = File.ReadAllLines(path);
            //    int Len_clients = Clients_file.Length;
            //    if (text_file == "Clients Gateway")
            //    {

            //        Client[] List_added = new Client[Len_clients];
            //        for (int n_clients = 0; n_clients < Len_clients; n_clients++)
            //        {
            //            //string array with each value for all the fields of a client
            //            string[] client_added = new string[14];
            //            string str_field = "";
            //            int num_field = 0;
            //            foreach (char chart_c in Clients_file[n_clients])
            //            {
            //                if (chart_c == ',')
            //                {
            //                    client_added[num_field] = str_field;
            //                    str_field = "";
            //                    num_field++;
            //                }
            //                else
            //                { str_field = str_field + chart_c; }

            //            }
            //            //Reading and creating the array for all the clients
            //            Client client_s = new Client
            //            {
            //                Country1 = client_added[0],
            //                Type_Card1 = client_added[1],
            //                Card_N1 = Convert.ToInt64(client_added[2]),
            //                Exp_date1 = DateTime.Parse(client_added[3]),
            //                CVV1 = Convert.ToInt16(client_added[4]),
            //                First_N1 = client_added[5],
            //                Last_N1 = client_added[6],
            //                Add_11 = client_added[7],
            //                Add_21 = client_added[8],
            //                City1 = client_added[9],
            //                State1 = client_added[10],
            //                CP1 = client_added[11],
            //                Add_date1 = DateTime.Parse(client_added[12]),
            //                Password1 = client_added[13]
            //            };
            //            client_s.Item1 = client_s.Item_num();
            //            List_added[n_clients] = client_s;
            //        }
            //        Menu.Clients_list = List_added;
            //        return List_added;
            //    }
            //    else if (text_file == "Sellers Gateway")
            //    {
            //        // string array with each value for all the fields of a client
            //        seller[] List_added = new seller[Len_clients];
            //        for (int n_clients = 0; n_clients < Len_clients; n_clients++)
            //        {
            //            string[] client_added = new string[12];
            //            string str_field = "";
            //            int num_field = 0;
            //            foreach (char chart_c in Clients_file[n_clients])
            //            {
            //                if (chart_c == ',')
            //                {
            //                    client_added[num_field] = str_field;
            //                    str_field = "";
            //                    num_field++;
            //                }
            //                else
            //                { str_field = str_field + chart_c; }

            //            }
            //            //Reading and creating the array for all the Sellers
            //            seller sellor_s = new seller
            //            {
            //                Country1 = client_added[0],
            //                Type_Card1 = client_added[1],
            //                Card_N1 = Convert.ToInt64(client_added[2]),
            //                First_N1 = client_added[3],
            //                Last_N1 = client_added[4],
            //                Add_11 = client_added[5],
            //                Add_21 = client_added[6],
            //                City1 = client_added[7],
            //                State1 = client_added[8],
            //                CP1 = client_added[9],
            //                Add_date1 = DateTime.Parse(client_added[10]),
            //                Password1 = client_added[11]
            //            };
            //            sellor_s.Item1 = sellor_s.Item_num();
            //            List_added[n_clients] = sellor_s;
            //        }
            //        Menu.Sellers_list = List_added;
            //        return List_added;
            //    }
            //    else
            //    {
            //        Purchase[] List_added = new Purchase[Len_clients];
            //        for (int n_clients = 0; n_clients < Len_clients; n_clients++)
            //        {
            //            string[] client_added = new string[5];
            //            string str_field = "";
            //            int num_field = 0;
            //            foreach (char chart_c in Clients_file[n_clients])
            //            {
            //                if (chart_c == ',')
            //                {
            //                    client_added[num_field] = str_field;
            //                    str_field = "";
            //                    num_field++;
            //                }
            //                else
            //                { str_field = str_field + chart_c; }

            //            }
            //            //Reading and creating the array for all the clients
            //            Purchase purchase_s = new Purchase
            //            {
            //                Order_number1 = Convert.ToInt32(client_added[0]),
            //                Purchase_item1 = Convert.ToInt32(client_added[1]),
            //                Purchase_amount1 = Convert.ToDouble(client_added[2]),
            //                Shipping_item1 = Convert.ToInt32(client_added[3]),
            //                Shipping_cost1 = Convert.ToDouble(client_added[4])
            //            };
            //            List_added[n_clients] = purchase_s;
            //        }
            //        Menu.Purchase_list = List_added;
            //        return List_added;
            //    }
            //}
            ////create a ne client, seller or purchase array
            //else
            {
                if (data_list == "Clients")
                {
                    List<Client> List_added = new List<Client>();
                    return List_added;
                }
                else if (data_list == "Sellers")
                {
                    List<Seller> List_added = new List<Seller>();
                    return List_added;
                }
                else
                {
                    SortedList<string, Payment_method> List_added = new SortedList<string, Payment_method>() ;
                    return List_added;
                }
            }


        }
        //Method to write a Client txt file
        public void Write_file(string text_file, List<Client> clients_saved, string filepath)
        {
            string path = filepath + text_file;
            if (!File.Exists(path)) { File.Create(path); }
            using (TextWriter tw = new StreamWriter(text_file))
            { foreach (var client_s in clients_saved)
                { tw.WriteLine(value: client_s); }
            }

        }
        //Method to write a seller txt file
        public void Write_file(string text_file, List<Seller> seller_saved, string filepath)
        {
            string path = filepath + text_file;
            File.Create(path);
            using (TextWriter tw = new StreamWriter(text_file))
            {
                foreach (var client_s in seller_saved)
                { tw.WriteLine(client_s); }
            }

        }
        //Method to write a Purchase txt file
        public void Write_file(string text_file, SortedList<string,Payment_method> payment_saved, string filepath)
        {
            string path = filepath + text_file;
            File.Create(path);
            using (TextWriter tw = new StreamWriter(text_file))
            {
                foreach (var client_s in payment_saved)
                { tw.WriteLine(client_s); }
            }


        }



    }
}
