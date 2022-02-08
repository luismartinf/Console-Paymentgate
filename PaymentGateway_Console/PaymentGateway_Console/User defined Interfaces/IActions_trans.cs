using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    interface IActions_trans
    {
        object Add_trans(long item, string str, string user_name);
        void Update_trans(object obj, string phase);
        void Show_trans(object obj);
        void Cancel_trans(object obj, string type_user);
    }
}
