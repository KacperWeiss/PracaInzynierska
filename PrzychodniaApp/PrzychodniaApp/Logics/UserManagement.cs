using PrzychodniaApp.DataBaseStuff;
using PrzychodniaApp.DataBaseStuff.Models;
using PrzychodniaApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.Logics
{
    public static class UserManagement
    {
        static public UserAccess LoginAs(string login, string password)
        {
            using (var DbContext = new DataBaseContext())
            {
                DbUser User = DbContext.Users.SingleOrDefault(u => u.Login == login);
                if (User == null)
                {
                    throw new Exception("Wrong login!");
                }
                if (User.Password == password)
                {
                    return (UserAccess)User.UserAccess;
                }
                else
                {
                    throw new Exception("Wrong password!");
                }
            }
        }
    }
}
