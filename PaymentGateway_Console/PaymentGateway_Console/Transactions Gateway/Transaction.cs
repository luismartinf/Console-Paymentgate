using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    //Class to saved the transactions of all the clients and Sellers  
    class Transaction
    {
        //Fields of each purchase
        int id_transaction;
        string type_trans;
        double Amount;
        int Item;
        DateTime time_start;
        string status;
        DateTime time_finish;

        public Transaction()
        {
            DateTime time_start = DateTime.Now;
        }

        //Assign id for transaction
        public int Id_transaction1 (int item, string type)
        {   if (type == "Client")
            {
                List<int> id_used = new List<int>();
                foreach (Transaction trans in Menu.transaction_list.Values)
                { id_used.Add(trans.Id_transaction); }
                Random rnd = new Random();
                int random_id = rnd.Next(100000, 999999);
                bool validid = false;
                while (!validid)
                {
                    if (id_used.Contains(random_id))
                    {
                        random_id = rnd.Next(100000, 999999);
                        validid = false;
                    }
                    else { validid = true; }
                }

                this.id_transaction = random_id;
                Type_trans = "Purchase";
                return id_transaction;
            }
            else
            {
                foreach (Transaction trans in Menu.transaction_list.Values)
                {
                    if (trans.Item1 == item)
                    { this.id_transaction = trans.Id_transaction; }
                   
                }
                Type_trans = "Shipping";
                return id_transaction;
            }
        }

        //properties of the transactions
        public int Id_transaction { get => id_transaction; set => id_transaction = value; }
        public string Type_trans { get => type_trans; set => type_trans = value; }
        public double Amount1 { get => Amount; set => Amount = value; }
        public int Item1 { get => Item; set => Item = value; }
        public DateTime T_start { get => time_start; set => time_start = value; }
        public string Status { get => status; set => status = value; }
        public DateTime T_finish { get => time_finish; set => time_finish = value; }
    }
}
