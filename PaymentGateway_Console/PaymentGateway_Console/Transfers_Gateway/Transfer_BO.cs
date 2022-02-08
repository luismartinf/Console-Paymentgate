using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    class Transfer_BO : IActions_trans
    {
        public object Add_trans(long id_trans, string type_user, string user_n)
        {
           
            string type_trans = "Deposit", amount = "0.00", day_transfer = "01/01/2022 01:00:00 a. m.";
            Transfer_Bank transfer = new Transfer_Bank(user_n, amount, type_trans, day_transfer, id_trans.ToString());
            Console.WriteLine("Enter the amount of the sale ");
            transfer.Amount = Convert.ToDouble(Console.ReadLine());
            transfer.Addtime();
            return transfer;
        }

        public void Cancel_trans(object id_transf, string type_user)
        {

            long id = Convert.ToInt64(id_transf);
            Transfer_Bank transfer = Main_menu.transfer_list[id];
            transfer.Type_transfer = "Devolution";
            transfer.Day_transfer = DateTime.Now;
            Main_menu.transfer_list[id] = transfer;
        }       
        
        public void Show_trans(Transfer_Bank transfer_data)
        {
             Console.WriteLine(transfer_data.Display_inf());
        }
        public void Show_trans(object id_transaction)
        {
            long id = Convert.ToInt64(id_transaction);

            foreach (var transfer in Main_menu.transfer_list)
            {
                if (transfer.Value.Id_transaction == id)
                { Console.WriteLine($"Transfer id {transfer.Key}: \n {transfer.Value.Display_inf()}"); }
            }
        }
        public void Update_trans(object obj, string phase)
        {
            throw new NotImplementedException();
        }
    }
}
