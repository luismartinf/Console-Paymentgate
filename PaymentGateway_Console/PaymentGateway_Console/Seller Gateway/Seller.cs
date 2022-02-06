using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    //Field and Properties for the Sellers inheritace from the class Customer
    class Seller : Customer
    {
        //Field
        string os_Url;

        public Seller(string userName, string first_N, string last_N, string add_date, string password, string email,string os_Url):base(userName, first_N, last_N,add_date, password, email)
        {
            this.os_Url = os_Url;
           
        }

        public string OS_Url1 { get => os_Url; set => os_Url = value; }

        public override string ToString()
        {
            string writef = $"{UserName1},{First_N1},{Last_N1},{Add_date1},{Password1},{Email1},{OS_Url1}";
            return writef;
        }
        public override string Display_inf()
        {
            string Display;
            Display = $"Username:{UserName1}, Name: {First_N1} {Last_N1} \n Online Shopping Url:{OS_Url1} ";
            return Display;
        }
    }
}
