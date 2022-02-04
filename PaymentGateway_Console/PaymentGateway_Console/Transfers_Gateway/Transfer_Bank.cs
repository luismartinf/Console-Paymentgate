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
        int id_transfer;
        double amount;
        string type_transfer;
        DateTime day_transfer;
        public Transfer_Bank()
        {
            
            DateTime day_transfer = DateTime.Now;
        }

        //Assign id for transaction
        public int Id_transfer1(int item, string type)
        {
            if (type == "Sellor")
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

                this.id_transfer = random_id;
                Type_transfer = "Deposit";
                return id_transfer;
            }
            else
            {
                foreach (Transaction trans in Menu.transaction_list.Values)
                {
                    if (trans.Item1 == item)
                    { this.id_transfer = trans.Id_transaction; }

                }
                Type_transfer = "Devolution";
                return id_transfer;
            }
        }

        public int Id_transfer { get => id_transfer; set => id_transfer = value; }
        public double Amount { get => amount; set => amount = value; }
        public string Type_transfer { get => type_transfer; set => type_transfer = value; }

        //Properties

        //Methods
    }
}
