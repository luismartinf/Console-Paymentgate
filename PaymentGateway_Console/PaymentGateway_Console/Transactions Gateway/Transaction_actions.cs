﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    class Transaction_actions : IActions_trans
    {
        public object Add_trans(int i, string str)
        {
            throw new NotImplementedException();
        }

        public void Cancel_trans(object obj)
        {
            throw new NotImplementedException();
        }

        public void Show_trans(Transaction transaction_data)
        {
            
            Console.WriteLine(transaction_data.Display_inf());
        }

        public void Show_trans(object id_transaction)
        {

            string id = id_transaction as string;
            var transaction = Main_menu.transaction_list[id];
            Console.WriteLine(transaction.Display_inf());
        }

        public object Update_trans(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
