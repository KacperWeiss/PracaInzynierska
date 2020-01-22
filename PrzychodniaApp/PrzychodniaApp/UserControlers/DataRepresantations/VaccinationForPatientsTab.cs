using PrzychodniaApp.DataBaseStuff;
using PrzychodniaApp.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.UserControlers.DataRepresantations
{
    public class VaccinationForPatientsTab
    {
        public int Id { get; set; }
        public VaccineStatus Status { get; set; }
        public string StatusText { get; set; }
        public string Name { get; set; }
        public string VaccinationDoneDate { get; set; }
        public string VaccinationObligatoryByDate { get; set; }

        public static List<VaccinationForPatientsTab> GetRepresentation(int treatmentHistoryId)
        {
            using (var context = new DataBaseContext())
            {
                var TreatmentHistory = context.TreatmentHistories
                    .Include(x => x.PastVaccinations)
                    .Include(x => x.RequiredVaccinations)
                    .SingleOrDefault(x => x.Id == treatmentHistoryId);

                return TreatmentHistory.PastVaccinations
                    .Select(x => new VaccinationForPatientsTab()
                    {
                        Id = x.Id,
                        Status = x.VaccineStatus,
                        StatusText = CustomEnumToString.GetVaccineStatusText(x.VaccineStatus),
                        Name = x.VaccinesName,
                        VaccinationDoneDate = x.VaccinationDate.HasValue ? x.VaccinationDate.Value.ToShortDateString() : "-",
                        VaccinationObligatoryByDate = x.ObligatoryBy.HasValue ? x.ObligatoryBy.Value.ToShortDateString() : "Nie wymagane"
                    })
                    .ToList()
                    .Concat(TreatmentHistory.RequiredVaccinations
                    .Select(x => new VaccinationForPatientsTab()
                    {
                        Id = x.Id,
                        Status = x.VaccineStatus,
                        StatusText = CustomEnumToString.GetVaccineStatusText(x.VaccineStatus),
                        Name = x.VaccinesName,
                        VaccinationDoneDate = x.VaccinationDate.HasValue ? x.VaccinationDate.Value.ToShortDateString() : "-",
                        VaccinationObligatoryByDate = x.ObligatoryBy.HasValue ? x.ObligatoryBy.Value.ToShortDateString() : "Nie wymagane"
                    })
                    .ToList())
                    .ToList();
            }
        }
    }
}
