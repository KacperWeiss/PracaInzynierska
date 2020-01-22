using PrzychodniaApp.DataBaseStuff;
using PrzychodniaApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.UserControlers.DataRepresantations
{
    public class ShortPatientForPatientsTab
    {
        public int PatientId { get; set; }
        public Pronouns PatientsGender { get; set; }
        public string EmailContact { get; set; }
        public string FullName { get; set; }
        public string EmailAdress { get; set; }
        public string DateOfBirth { get; set; }

        public static List<ShortPatientForPatientsTab> GetRepresentation()
        {
            using (var context = new DataBaseContext())
            {
                return context.Patients
                    .AsEnumerable()
                    .OrderBy(x => x.LastName)
                    .Select(x => new ShortPatientForPatientsTab()
                    {
                        PatientId = x.Id,
                        PatientsGender = x.PatientsPronounce,
                        EmailContact = CustomEnumToString.GetEmailContactText(x.EmailContact),
                        FullName = x.FirstName + " " + x.LastName,
                        EmailAdress = x.EmailAdress,
                        DateOfBirth = x.DateOfBirth.ToLongDateString()
                    })
                    .ToList();
            }
        }
    }
}
