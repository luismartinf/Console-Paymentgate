using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    //Field and Properties for the Sellers inheritace from the class Client
    class Seller : Client
    {
        //Field
        string os_Url;

        public string OS_Url1 { get => os_Url; set => os_Url = value; }

        public override string ToString()
        {
            string writef = $"{UserName1},{First_N1},{Last_N1},{Add_date1},{Password1},{Email1},{OS_Url1}";
            return writef;
        }
        public override string Display_inf()
        {
            string Display;
            Display = $"Username:{UserName1}, Name: {First_N1} {Last_N1} \n{OS_Url1} ";
            return Display;
        }
    }
}
