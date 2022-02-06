using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    //Class to save the personal information of the Customers or Sellers
    class Customer

    {   //Fields with the data of the Customer/seller
        string userName;
        string first_N;
        string last_N;
        DateTime add_date;
        string password;
        string email;
       

        //Add date is assigned automatically and their property is for read from the txt file
        public  Customer(string userName, string first_N, string last_N, string add_date , string password, string email)
        {
            this.userName = userName;
            this.first_N = first_N;
            this.last_N = last_N;
            this.add_date = DateTime.Parse(add_date);
            this.password = password;
            this.email = email;
        }

        public void Addtime()
        { Add_date1 = DateTime.Now;}


        //Properties for the fields
        public string First_N1 { get => first_N; set => first_N = value; }
        public string Last_N1 { get => last_N; set => last_N = value; }
        public DateTime Add_date1 { get => add_date; set => add_date = value; }
        public string Password1 { get => password; set => password = value; }
        public string UserName1 { get => userName; set => userName = value; }
        public string Email1 { get => email; set => email = value; }

        //Override method to display the data of the Customer/Seller
        public override string ToString( )
        {
            string writef = $"{UserName1},{First_N1},{Last_N1},{Add_date1},{Password1},{Email1}";
            return writef;
        }

        public virtual string Display_inf()
        {
            string Display;
            Display = $"Username:{UserName1}, Name: {First_N1} {Last_N1}, Email:{Email1}";
            return Display;
        }
        
    }
}
