using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway_Console
{
    class Valpassword
    {
        public static bool ValidatePassword(string password)
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
