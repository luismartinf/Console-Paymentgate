using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console

{   //Menu to display the options of the customers
    class Main_menu
    {
        //Fields---Collections to saved the information of customers, Sellers and transactions in txt files
        public static ArrayList Gateway = new ArrayList();
        public static List<Seller> sellers_list;
        public static List<Customer> customers_list;
        public static Dictionary<long, Transaction> transaction_list;
        public static SortedList<string, Payment_method> paymethod_list;
        public static SortedList<long, Transfer_Bank> transfer_list;

        //path where is saved the file  (@"C:\Users\luis.martin\Downloads)
        public static string fullpath = @"C:\Users\luis.martin\Downloads\";

        public static void Read_collection()
        {
            Read_write_files files_rw = new Read_write_files();
            sellers_list = (List<Seller>)files_rw.Read_list("Sellers Gateway", fullpath, "Sellers");
            customers_list= (List<Customer>)files_rw.Read_list("Customers Gateway", fullpath, "Customers");
            transaction_list = (Dictionary<long, Transaction>)files_rw.Read_list("Transaction Gateway", fullpath, "Transaction");
            paymethod_list = (SortedList<string, Payment_method>)files_rw.Read_list("Paymethod Gateway", fullpath, "Paymethod");
            transfer_list= (SortedList<long, Transfer_Bank>)files_rw.Read_list("Transfer Gateway", Main_menu.fullpath, "Transfer");
        }

        //Menu of the customer
        public static void Menu_customers()
        {
            Read_collection();
            Customer_menu customer_Menu = new Customer_menu();
            customer_Menu.Menu_customers();
        }

        //Menu for the seller 
        public static void Menu_sellers()
        {
            Read_collection();
            Sellers_menu seller_Menu = new Sellers_menu();
            seller_Menu.Menu_sellers();

        }
        public static void Menu_managers()
        {

            Read_collection(); 
            Manager_menu managers_Menu = new Manager_menu();
            managers_Menu.Menu_managers();

        }



    }
}