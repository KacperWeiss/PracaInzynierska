using PrzychodniaApp.DataBaseStuff;
using PrzychodniaApp.DataBaseStuff.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.UserControlers.DataRepresantations
{
    public class SicnessForCurrentVisitTab
    {
        public int TreatmentId { get; set; }
        public string Name { get; set; }
        public string When { get; set; }

        public static List<SicnessForCurrentVisitTab> GetRepresentation(int treatmentHistoryId)
        {
            using (var context = new DataBaseContext())
            {
                List<DbVisit> visits = context.Visits.ToList();

                return context.TreatmentHistories
                    .Include(x => x.Treatments)
                    .Single(x => x.Id == treatmentHistoryId)
                    .Treatments
                    .Select(x => new SicnessForCurrentVisitTab()
                    {
                        TreatmentId = x.Id,
                        Name = x.IllnessName,
                        When = visits.Single(i => i.Id == x.Visit.Id).TimeStart.ToShortDateString()
                    })
                    .ToList();
            }
        }
    }
}
