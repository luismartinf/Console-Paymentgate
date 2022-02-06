using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    class Payment_method 
    {


        //Card Information
        string user_type;
        string type_card;
        long card_N;
        DateTime exp_date;
        short cvv;
        long l4_digit;
        string user_name;

        //Billin adress
        
        string country;
        string add_1;
        string add_2;
        string city;
        string state;
        string cp;

        public Payment_method(string user_name, string user_type, string type_card, string card_N, string l4_digit,  string country, string add_1, string add_2, string state, string cp, string exp_date = "01/01/2022 01:00:00 a. m.", string cvv = "000", string city = "")
        {
            this.user_type = user_type;
            this.type_card = type_card;
            this.card_N = Convert.ToInt64(card_N);
            this.exp_date = DateTime.Parse(exp_date);
            this.cvv = Convert.ToInt16(cvv);
            this.l4_digit = Convert.ToInt64(l4_digit);
            this.user_name = user_name;
            this.country = country;
            this.add_1 = add_1;
            this.add_2 = add_2;
            this.city = city;
            this.state = state;
            this.cp = cp;
        }

        
            
           
        //Initial data for values that are not allways retrived in the case of city in some cases doesn´t exist,
        //Exp_date and CVV aren´t relevant for the seller to use the inheritance of the class they have dummy values
       


        public string Country1 { get => country; set => country = value; }
        public string Add_11 { get => add_1; set => add_1 = value; }
        public string Add_21 { get => add_2; set => add_2 = value; }
        public string City1 { get => city; set => city = value; }
        public string State1 { get => state; set => state = value; }
        public string CP1 { get => cp; set => cp = value; }
        public string Type_card1 { get => type_card; set => type_card = value; }
        public long Card_N1 { get => card_N; set => card_N = value; }
        public DateTime Exp_date1 { get => exp_date; set => exp_date = value; }
        public short CVV1 { get => cvv; set => cvv = value; }
        public long L4_digit1 { get => l4_digit; set => l4_digit = value; }
       
        public string User_type1 { get => user_type; set => user_type = value; }
        public string User_name1 { get => user_name; set => user_name = value; }

        //Override method to display the data of the Customer/Seller
        public override string ToString()
        {
            string writef = $"{User_name1},{User_type1},{Type_card1},{Card_N1},{L4_digit1},{Country1},{Add_11},{Add_21},{State1},{CP1},{Exp_date1},{CVV1},{City1}";
            return writef;
        }
        public string Display_inf()
        {
            string Display;
            if (City1 == "")
            { Display = $"Card Number **** **** **** **** {L4_digit1} \n Billing Adress: {Add_11}, {Add_21}, {CP1}, {State1}, {Country1}"; }
            else
            { Display = $"Card Number **** **** **** **** {L4_digit1} \n Billing Adress: {Add_11}, {Add_21}, {CP1}, {City1}, {State1}, {Country1}"; }
            return Display;
        }
    }
}
