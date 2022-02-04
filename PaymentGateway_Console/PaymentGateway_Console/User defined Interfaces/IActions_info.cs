using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    interface IActions_info
    {
        object Add_info(object obj, string type);
        void Delete_info(object obj);
        void Show_info(object obj);
       
   

    }
}
