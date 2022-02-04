using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    interface IActions_trans
    {
        object Add_trans(int i, string str);
        object Update_trans(object obj);
        void Show_trans(object obj);
        void Cancel_trans(object obj);
    }
}
