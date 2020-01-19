using PrzychodniaApp.DataBaseStuff;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.UserControlers.DataRepresantations
{
    public class UserForUsersManagementTab
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserAccess { get; set; }
        public string Specializations { get; set; }

        public static List<UserForUsersManagementTab> GetRepresentation()
        {
            using (var context = new DataBaseContext())
            {
                return context.Users
                    .Include(x => x.MedicalWorker)
                    .Include(x => x.MedicalWorker.Specializations)
                    .AsEnumerable()
                    .Select(x => new UserForUsersManagementTab()
                    {
                        UserId = x.Id,
                        Username = x.Login,
                        UserAccess = x.UserAccess.ToString(),
                        Specializations = x.MedicalWorker != null ? string.Join(", ", x.MedicalWorker.Specializations.Select(t => t.Type)) : x.UserAccess.ToString()
                    })
                    .ToList();
            }
        }
    }
}
