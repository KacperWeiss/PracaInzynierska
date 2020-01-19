using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.Logics
{
    public static class PasswordGenerator
    {
        public static string GetRandomPassword()
        {
            var chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var stringChars = new char[20];
            var random = new Random();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    stringChars[i * 7 + j] = chars[random.Next(chars.Length)];
                }
            }
            stringChars[6] = '-';
            stringChars[13] = '-';

            return new String(stringChars);
        }
    }
}
