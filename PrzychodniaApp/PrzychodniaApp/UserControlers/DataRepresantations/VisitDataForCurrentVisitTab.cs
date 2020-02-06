using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using PrzychodniaApp.DataBaseStuff;

namespace PrzychodniaApp.UserControlers.DataRepresantations
{
    public class VisitDataForCurrentVisitTab
    {
        public int VisitId { get; set; }
        public int TreatmentHistoryId { get; set; }
        public string PatientsName { get; set; }
        public string PatientsPESEL { get; set; }
        public string PatientsAge { get; set; }
        public string VisitType { get; set; }
        public string OptionalDescription { get; set; }

        public static VisitDataForCurrentVisitTab GetRepresentation()
        {
            using (var context = new DataBaseContext())
            {
                string ageString = GetAge(context.Visits
                    .Include(x => x.Patient)
                    .Single(x => x.Id == DataHolderForMainWindow.ChosenVisitId)
                    .Patient.DateOfBirth);

                return context.Visits
                    .Include(x => x.Patient)
                    .Include(x => x.Specialization)
                    .Include(x => x.Patient.TreatmentHistory)
                    .Where(x => x.Id == DataHolderForMainWindow.ChosenVisitId)
                    .Select(x => new VisitDataForCurrentVisitTab()
                    {
                        VisitId = x.Id,
                        TreatmentHistoryId = x.Patient.TreatmentHistory.Id,
                        PatientsName = x.Patient.FirstName + " " + x.Patient.LastName,
                        PatientsPESEL = x.Patient.NumberPesel.ToString(),
                        PatientsAge = ageString,
                        VisitType = x.Specialization.Type,
                        OptionalDescription = x.OptionalDescription
                    })
                    .SingleOrDefault();
            }
        }

        private static string GetAge(DateTime dateTime)
        {
            return (new DateTime(DateTime.Now.Subtract(dateTime).Ticks).Year - 1).ToString() + " Lat";
        }
    }
}
