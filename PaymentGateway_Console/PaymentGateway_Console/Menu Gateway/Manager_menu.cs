using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    class Manager_menu
    {
        public void Menu_managers()
        {  //   Menu for the managers
            //   Read and Display all the information of the three databases Customer, Sellers and Transactions in a object array
            Read_write_files files_rw = new Read_write_files();
            List<Customer> customers = (List<Customer>)files_rw.Read_list("Customers Gateway", Main_menu.fullpath, "Customers");
            Main_menu.Gateway.Add(customers);
            List<Seller> sellers = (List<Seller>)files_rw.Read_list("Sellers Gateway", @"C: \Users\luis.martin\Downloads\", "Sellers");
            Main_menu.Gateway.Add(sellers);
            Dictionary<long, Transaction> transactions = (Dictionary<long, Transaction>)files_rw.Read_list("Transactions Gateway", @"C: \Users\luis.martin\Downloads\", "Transactions");
            Main_menu.Gateway.Add(transactions);
            SortedList<string, Payment_method> payments = (SortedList<string, Payment_method>)files_rw.Read_list("Paymethod Gateway", Main_menu.fullpath, "Paymethod");
            Main_menu.Gateway.Add(payments);
            SortedList<long, Transfer_Bank> transfers = (SortedList<long, Transfer_Bank>)files_rw.Read_list("Transfers Gateway", Main_menu.fullpath, "Transfers");
            Main_menu.Gateway.Add(transfers);
        }
    }
}
