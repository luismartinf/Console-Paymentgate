using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    class Program
    {


        static void Main(string[] args)
        {
            //Display Options to select the role and activate the corresponding menu
            Console.WriteLine("Hello , Welcome to the payment gateway ");
            Console.WriteLine("Type if you are Client, Seller or Manager");
            string role = Console.ReadLine();
            bool ValidRole = false;
            while (!ValidRole)
            {
                switch (role)
                {
                    case "Client":
                        Console.WriteLine("Select if you want to do actions for a client write C or payment methods of existing client write PMC");
                        string menu_CorPMC =Console.ReadLine();
                        if (menu_CorPMC == "C")
                        { Menu.Menu_clients(); }
                        else if (menu_CorPMC == "PMC")
                        { Menu_paymethod.Menu_clients(); }    
                        ValidRole = true;
                        break;
                    case "Seller":
                        Console.WriteLine("Select if you want to do actions for a sellor write S or payment methods of existing sellor write PMS");
                        string menu_SorPMS = Console.ReadLine();
                        if (menu_SorPMS == "S")
                        { Menu.Menu_sellors(); }
                        else if (menu_SorPMS == "PMS")
                        { Menu_paymethod.Menu_sellors(); }
                        ValidRole = true;
                        break;
                    case "Manager":
                        Menu.Menu_managers();
                        ValidRole = true;
                        break;
                    default:
                        Console.WriteLine("Role is Wrong, reenter the value");
                        Console.WriteLine("Write if you are Client, Seller or Manager");
                        role = Console.ReadLine();
                        ValidRole = false;
                        break;
                }

            }
            Console.WriteLine("Thank you to use the Gateway");
            _ = Console.ReadKey();

        }
    }
}
