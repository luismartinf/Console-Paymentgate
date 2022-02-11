using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEbox_example2
{
    class Program_example2
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a password to be validated");
            string password = Console.ReadLine();
            if (validatePassword(password))
            {
                Console.WriteLine($"{password} \n Password is valid");
            }
            else
            {
                Console.WriteLine($"{password} \n Password is invalid");
            }
            _ = Console.ReadKey();
        }

        static bool validatePassword(string password)
        {
            bool validatepassword, onelower=true, oneupper = true, onenumber = true, onesymbol = true;
            int checks = 0;
            if (password.Length > 7)
            { checks++; }
            else { { checks += 0; } }
            foreach (char pas in password)
            {
                if (Char.IsLower(pas) && onelower == true)
                {
                    checks++;
                    onelower = false;
                }
                else if (Char.IsUpper(pas) && oneupper == true)
                {
                    checks++;
                    oneupper = false;
                }
                else if (Char.IsNumber(pas) && onenumber == true)
                {
                    checks++;
                    onenumber = false;
                }
                else if (Char.IsSymbol(pas) && onesymbol == true)
                {
                    checks++;
                    onesymbol = false;
                }
                else if (Char.IsWhiteSpace(pas))
                {
                    checks--;
                }

                else
                { checks += 0; }
            }
            if (checks > 4)
            { validatepassword = true; }
            else { validatepassword = false; }
            return validatepassword;
        }


    }
}
