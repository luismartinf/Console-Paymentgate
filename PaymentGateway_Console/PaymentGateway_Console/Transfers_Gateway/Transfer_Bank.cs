using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    class Transfer_Bank
    {
        //Fields
        string username;
        double amount;
        string type_transfer;
        DateTime day_transfer;
        public Transfer_Bank()
        {
            
           
        }

        public Transfer_Bank(string user_n, string amount, string type_transfer, string day_transfer)
        {
            this.username = user_n;
            this.amount = Convert.ToDouble(amount);
            this.type_transfer = type_transfer;
            this.day_transfer = DateTime.Parse( day_transfer);
        }

        //Methods
        public void Addtime()
        { Day_transfer = DateTime.Now; }

        //Assign id for transaction
        public int Id_transfer1(int item, string type)
        {
            Random rnd = new Random();
            int random_id = rnd.Next(100000, 999999);
            if (type == "Sellor")
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
                Type_transfer = "Deposit";
            }
            else
            {
                foreach (KeyValuePair<int, Transaction> trans in Main_menu.transaction_list)
                {
                    if (trans.Value.Item1 == item)
                    { random_id = trans.Key; }

                }
                Type_transfer = "Devolution";
            }
            return random_id;
        }

        public override string ToString()
        {
            string writef = $"{Username},{Amount},{Type_transfer},{Day_transfer }";
            return writef;
        }

        //Properties
        public double Amount { get => amount; set => amount = value; }
        public string Type_transfer { get => type_transfer; set => type_transfer = value; }
        public DateTime Day_transfer { get => day_transfer; set => day_transfer = value; }
        public string Username { get => username; set => username = value; }

        

       
    }
}
