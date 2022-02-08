using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    class Transaction_BO : IActions_trans
    {
        public object Add_trans(long item_tr, string type_user, string user_n)
        {
            int item_trans = Convert.ToInt32(item_tr);
            if (type_user == "Customer")
            {
                string type_trans = "Purchase", amount = "0.00", time_start = "01/01/2022 01:00:00 a. m.";
                Transaction transaction = new Transaction(user_n, type_trans, amount, item_trans.ToString(), time_start); 
                Console.WriteLine("Enter the amount of the purchase ");
                transaction.Amount1 = Convert.ToDouble(Console.ReadLine());
                transaction.Addtime();
                return transaction;

            }
            else
            {
                string type_trans = "Shipping", amount = "0.00", time_start = "01/01/2022 01:00:00 a. m.", item_t = "0", status ="In progress";
                Transaction transaction = new Transaction(user_n, type_trans, amount, item_t, time_start,status);
                Console.WriteLine("Enter the cost of the shipping ");
                transaction.Amount1 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter the shipping item ");
                transaction.Item1 = Convert.ToInt32(Console.ReadLine());
                transaction.Addtime();
                return transaction;
            }
            
        }

        public void Cancel_trans(object id_trans, string type)
        {
            long id = Convert.ToInt64(id_trans);
            if (type == "Customer")
            { 
                Transaction cus_uptrans = Main_menu.transaction_list[id];
                Transaction sell_uptrans = Main_menu.transaction_list[key: id + 1000000000];
                Console.WriteLine("Why you want to cancel the trasaction");
                cus_uptrans.Notes = Console.ReadLine();
                sell_uptrans.Notes = $"Customer: {cus_uptrans.Notes}";
                cus_uptrans.Status = "Canceled";
                sell_uptrans.Status = "Returned";
                cus_uptrans.T_finish = DateTime.Now;
                sell_uptrans.T_finish = DateTime.Now;
                Main_menu.transaction_list[id] = cus_uptrans;
                Main_menu.transaction_list[id + 1000000000] = sell_uptrans;
            }
            else if (type == "Seller")
            {
                Transaction sell_uptrans = Main_menu.transaction_list[id];
                Transaction cus_uptrans = Main_menu.transaction_list[key:id - 1000000000];
                Console.WriteLine("Why you want to cancel the trasaction");
                sell_uptrans.Notes = Console.ReadLine();
                cus_uptrans.Notes = $"Seller: {sell_uptrans.Notes}";
                cus_uptrans.Status = "Canceled";
                sell_uptrans.Status = "Returned";
                cus_uptrans.T_finish = DateTime.Now;
                sell_uptrans.T_finish = DateTime.Now;
                Main_menu.transaction_list[id] = sell_uptrans;
                Main_menu.transaction_list[id - 1000000000] = cus_uptrans;
            }
        }

        public void Show_trans(Transaction transaction_data)
        {
            
            Console.WriteLine(transaction_data.Display_inf());
        }

        public void Show_trans(object id_transaction)
        {

            long id = Convert.ToInt64(id_transaction);
            var transaction = Main_menu.transaction_list[id];
            Console.WriteLine(transaction.Display_inf());
        }

        public void Update_trans(object id_trans, string phase)
        {
            long id = Convert.ToInt64(id_trans);
            if (phase == "shipment")
            {
                Transaction cus_uptrans = Main_menu.transaction_list[key:id - 1000000000];
                cus_uptrans.Status = "Shipped";
                Main_menu.transaction_list[key: id - 1000000000] = cus_uptrans;
            }
            else if (phase == "Recd")
            {
                Transaction cus_uptrans = Main_menu.transaction_list[id];
                Transaction sell_uptrans = Main_menu.transaction_list[key: id + 1000000000];
                cus_uptrans.Status = "Delivered";
                sell_uptrans.Status = "Finished";
                cus_uptrans.T_finish = DateTime.Now;
                sell_uptrans.T_finish = DateTime.Now;
                Main_menu.transaction_list[id] = cus_uptrans;
                Main_menu.transaction_list[key: id + 1000000000] = sell_uptrans;
            }

        }   
    }
}
