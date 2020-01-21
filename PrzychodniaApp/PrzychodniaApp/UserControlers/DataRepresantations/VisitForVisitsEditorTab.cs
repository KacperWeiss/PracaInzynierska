using PrzychodniaApp.DataBaseStuff;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.UserControlers.DataRepresantations
{
    public class VisitForVisitsEditorTab
    {
        public int VisitId { get; set; }
        public string Patient { get; set; }
        public string MedicalWorker { get; set; }
        public string VisitWhen { get; set; }
        public string Specialization { get; set; }
        public string Description { get; set; }

        public static List<VisitForVisitsEditorTab> GetRepresentation()
        {
            using (var context = new DataBaseContext())
            {
                return context.Visits
                    .Include(x => x.Patient)
                    .Include(x => x.MedicalWorker)
                    .Include(x => x.Specialization)
                    .AsEnumerable()
                    .OrderBy(x => x.TimeStart)
                    .Select(x => new VisitForVisitsEditorTab()
                    {
                        VisitId = x.Id,
                        Patient = x.Patient.FirstName + " " + x.Patient.LastName,
                        MedicalWorker = x.MedicalWorker.FirstName + " " + x.MedicalWorker.LastName,
                        VisitWhen = x.TimeStart.ToString(),
                        Specialization = x.Specialization.Type,
                        Description = x.OptionalDescription
                    })
                    .ToList();
            }
        }
    }
}
