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
        long id_transaction;
        DateTime day_transfer;
       

        public Transfer_Bank(string user_n, string amount, string type_transfer, string day_transfer, string id_transaction)
        {
            this.username = user_n;
            this.amount = Convert.ToDouble(amount);
            this.type_transfer = type_transfer;
            this.day_transfer = DateTime.Parse( day_transfer);
            this.id_transaction = Convert.ToInt64(id_transaction);
        }

        //Methods
        public void Addtime()
        { Day_transfer = DateTime.Now; }

        //Assign id for transaction
        public long Id_transfer1(int item, string type)
        {
            Random rnd = new Random();
            long random_id = rnd.Next(100000000, 999999999);
            List<long> id_used = new List<long>();
            foreach (long trans in Main_menu.transfer_list.Keys)
            { id_used.Add(trans); }
            bool validid = false;
            while (!validid)
            {
                if (id_used.Contains(random_id))
                {
                        random_id = rnd.Next(100000000, 999999999);
                        validid = false;
                }
                else { validid = true; }
            }
            return random_id;
        }

        public override string ToString()
        {
            string writef = $"{Username},{Amount},{Type_transfer},{Day_transfer },{Id_transaction}";
            return writef;
        }

        public string Display_inf()
        {
            string Display;
            Display = $"Username:{Username}, Type of transfer: {Type_transfer}\n The transaction was made on {Day_transfer} for an amount of  {Amount} USD  \n" +
                      $"[Notes: the Id_transaction{Id_transaction} is required to search a transfer id_transfer is only for internal control]";
            return Display;
        }

        //Properties
        public double Amount { get => amount; set => amount = value; }
        public string Type_transfer { get => type_transfer; set => type_transfer = value; }
        public DateTime Day_transfer { get => day_transfer; set => day_transfer = value; }
        public string Username { get => username; set => username = value; }
        public long Id_transaction { get => id_transaction; set => id_transaction = value; }
    }
}
