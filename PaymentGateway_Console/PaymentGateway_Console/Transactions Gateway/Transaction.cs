using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    //Class to saved the transactions of all the customers and Sellers  
    class Transaction
    {
        //Fields of each purchase
        string username;
        string type_trans;
        double Amount;
        int Item;
        DateTime time_start;
        string status;
        DateTime time_finish;

        
        public Transaction(string username, string type_trans, string amount, string item, string time_start, string status = "Payed", string time_finish = "01/01/2022 01:00:00 a. m.")
        {
            this.username = username;
            this.type_trans = type_trans;
            Amount = Convert.ToDouble(amount);
            Item = Convert.ToInt32(item);
            this.time_start = DateTime.Parse(time_start);
            this.status = status;
            this.time_finish = DateTime.Parse(time_finish);
        }

        public void Addtime()
        { T_start = DateTime.Now; }

        //Assign id for transaction
        public int Id_transaction1 (int item, string type)
        {
            Random rnd = new Random();
            int random_id = rnd.Next(100000, 999999);
            if (type == "Customer")
            {
                List<int> id_used = new List<int>();
                foreach (int trans in Main_menu.transaction_list.Keys)
                { id_used.Add(trans); }
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

                Type_trans = "Purchase";
            }
            else
            {
                foreach (KeyValuePair<int, Transaction> trans in Main_menu.transaction_list)
                {
                    if (trans.Value.Item == item)
                    { random_id = trans.Key; }

                }
                Type_trans = "Shipping";
            }
            return random_id;
        }

        public override string ToString()
        {

            string writef = $"{Username},{Type_trans},{Amount1},{Item1 },{ T_start},{Status},{T_finish}";
            return writef;
        }

        //properties of the transactions
        public string Type_trans { get => type_trans; set => type_trans = value; }
        public double Amount1 { get => Amount; set => Amount = value; }
        public int Item1 { get => Item; set => Item = value; }
        public DateTime T_start { get => time_start; set => time_start = value; }
        public string Status { get => status; set => status = value; }
        public DateTime T_finish { get => time_finish; set => time_finish = value; }
        public string Username { get => username; set => username = value; }
    }
}
