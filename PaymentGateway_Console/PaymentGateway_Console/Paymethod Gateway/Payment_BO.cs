using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace PaymentGateway_Console
{
    class Payment_BO : IActions_info
    {
        public object Add_info(object user_name, string type )
        {
            string user = user_name as string;
            Payment_method payment = new Payment_method();
            payment.User_type1 = type;
            Console.WriteLine("Country");
            payment.Country1 = Console.ReadLine();
            Console.WriteLine("Type Card");
            payment.Type_card1 = Console.ReadLine();
            Console.WriteLine("16 digits of Card Number");
            payment.Card_N1 = Convert.ToInt64(Console.ReadLine());
            int l_Card = Convert.ToString(payment.Card_N1).Length;

            //validate the card contains 16 digits exactly
            bool Validcard = l_Card == 16;
            while (!Validcard)
            {
                Console.WriteLine("Card Number is Wrong, reenter the value");
                payment.Card_N1 = Convert.ToInt64(Console.ReadLine());
                l_Card = Convert.ToString(payment.Card_N1).Length;
                Validcard = l_Card == 16;
            }

            //retrieve security data
            Console.WriteLine("Expired Date MM/YY");
            payment.Exp_date1 = DateTime.ParseExact(Console.ReadLine(), "MM/yy", CultureInfo.InvariantCulture);
            Console.WriteLine("CVV");

            //Information for the billing adress
            payment.L4_digit1 = payment.Card_N1 % 10000;
            payment.CVV1 = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Address Line 1");
            payment.Add_11 = Console.ReadLine();
            Console.WriteLine("Address Line 2");
            payment.Add_21 = Console.ReadLine();
            Console.WriteLine("City if that is the case");
            payment.City1 = Console.ReadLine();
            Console.WriteLine("State");
            payment.State1 = Console.ReadLine();
            Console.WriteLine("Postal Code");
            payment.CP1 = Console.ReadLine();
            payment.User_name1 = user;
            return payment;
        }

        //Method to delete the information saved in the payment method list from a client or sellor
        public void Delete_info(object id_payment)
        {
            SortedList<string, Payment_method> paymethods = Main_menu.paymethod_list;
            string id = id_payment as string;
            paymethods.Remove(id);
            Main_menu.paymethod_list = paymethods;
        }

        public void Show_info(object id_payment)
        {
            string id = id_payment as string;
            var payment = Main_menu.paymethod_list[id];
            Console.WriteLine(payment.Display_inf()); 
            
        }

        public void Show_info(Payment_method payment)
        {
           Console.WriteLine(payment.Display_inf());
        }
    }
}
