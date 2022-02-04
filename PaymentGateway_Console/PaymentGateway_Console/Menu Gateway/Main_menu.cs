using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console

{   //Menu to display the options of the clients
    class Main_menu
    {
        //Collections to saved the information of clients, Sellers and purchases in txt iles
        public static object[] Gateway;
        public static List<Seller> sellers_list;
        public static List<Client> clients_list;
        public static Dictionary <string,Transaction>transaction_list;
        public static SortedList<string,Payment_method> paymethod_list;
        public static SortedList<string, Transfer_Bank> transfer_list;

        //menu of the client
        public static void Menu_clients()
        {
            Client_menu client_Menu = new Client_menu();
            client_Menu.Menu_clients();
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