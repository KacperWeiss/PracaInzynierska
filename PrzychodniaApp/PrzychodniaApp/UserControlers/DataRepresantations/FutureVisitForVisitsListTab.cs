using PrzychodniaApp.DataBaseStuff;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.UserControlers.DataRepresantations
{
    public class FutureVisitForVisitsListTab
    {
        public int VisitId { get; set; }
        public string Patient { get; set; }
        public string VisitWhen { get; set; }
        public string Specialization { get; set; }
        public string Description { get; set; }

        public static List<FutureVisitForVisitsListTab> GetRepresentation()
        {
            using (var context = new DataBaseContext())
            {
                int medicalWorkerId = context.Users
                    .Include(x => x.MedicalWorker)
                    .Single(x => x.Id == DataHolderForMainWindow.User.Id)
                    .MedicalWorker
                    .Id;

                return context.Visits
                    .Include(x => x.Patient)
                    .Include(x => x.MedicalWorker)
                    .Include(x => x.Specialization)
                    .Where(x => x.MedicalWorker.Id == medicalWorkerId)
                    .Where(x => x.TimeStart > DateTime.Now)
                    .AsEnumerable()
                    .OrderBy(x => x.TimeStart)
                    .Select(x => new FutureVisitForVisitsListTab()
                    {
                        VisitId = x.Id,
                        Patient = x.Patient.FirstName + " " + x.Patient.LastName,
                        VisitWhen = x.TimeStart.ToString(),
                        Specialization = x.Specialization.Type,
                        Description = x.OptionalDescription
                    })
                    .ToList();
            }
        }
    }
}
