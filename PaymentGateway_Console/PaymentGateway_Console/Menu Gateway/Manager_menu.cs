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
            //   Read and Display all the information of the three databases Client, Sellers and Transactions in a object array
            Read_write_files files_rw = new Read_write_files();
            Main_menu.Gateway = new object[4][];
            List<Client> clients = (List<Client>)files_rw.Read_list("Clients Gateway", @"C:\Users\luis.martin\Downloads\", "Clients");
            Main_menu.Gateway[0] = clients;
            List<Seller> sellers = (List<Seller>)files_rw.Read_list("Sellers Gateway", @"C: \Users\luis.martin\Downloads\", "Sellers");
            Main_menu.Gateway[1] = sellers;
            Dictionary<string, Transaction> transactions = (Dictionary<string, Transaction>)files_rw.Read_list("Transactions Gateway", @"C: \Users\luis.martin\Downloads\", "Transactions");
            Main_menu.Gateway[2] = transactions;
            SortedList<string, Payment_method> payments = (SortedList<string, Payment_method>)files_rw.Read_list("Paymethod Gateway", @"C:\Users\luis.martin\Downloads\", "Paymethod");
            Main_menu.Gateway[3] = payments;
            SortedList<string, Transfer_Bank> transfers = (SortedList<string, Transfer_Bank>)files_rw.Read_list("Transfers Gateway", @"C:\Users\luis.martin\Downloads\", "Transfers");
            Main_menu.Gateway[4] = transfers;
        }
    }
}
