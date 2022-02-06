using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console

{   //Menu to display the options of the customers
    class Main_menu
    {
        //Collections to saved the information of customers, Sellers and purchases in txt iles
        public static object[] Gateway;
        public static List<Seller> sellers_list;
        public static List<Customer> customers_list;
        public static Dictionary <int,Transaction>transaction_list;
        public static SortedList<string,Payment_method> paymethod_list;
        public static SortedList<int, Transfer_Bank> transfer_list;

        //menu of the customer
        public static void Menu_customers()
        {
            Customer_menu customer_Menu = new Customer_menu();
            customer_Menu.Menu_customers();
        }

        //Menu for the seller 
        public static void Menu_sellors()
        {
            Sellers_menu seller_Menu = new Sellers_menu();
            seller_Menu.Menu_sellers();

        }
        public static void Menu_managers()
        {
            Manager_menu managers_Menu = new Manager_menu();
            managers_Menu.Menu_managers();

        }



    }
}