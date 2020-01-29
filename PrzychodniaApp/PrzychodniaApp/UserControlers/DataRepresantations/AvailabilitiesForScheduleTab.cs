using PrzychodniaApp.DataBaseStuff;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.UserControlers.DataRepresantations
{
    public class AvailabilitiesForScheduleTab
    {
        public int Id { get; set; }
        public int MedicalWorkerId { get; set; }
        public int SpecializationId { get; set; }
        public string MedicalWorkerFullName { get; set; }
        public string SpecializationType { get; set; }
        public string Day { get; set; }
        public string TimeSpan { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string VisitPrice { get; set; }

        public static List<AvailabilitiesForScheduleTab> GetRepresentation()
        {
            using (var context = new DataBaseContext())
            {
                return context.Availabilities
                    .AsEnumerable()
                    .Select(x => new AvailabilitiesForScheduleTab()
                    {
                        Id = x.Id,
                        MedicalWorkerId = x.MedicalWorker.Id,
                        SpecializationId = x.Specialization.Id,
                        MedicalWorkerFullName = x.MedicalWorker.FirstName + " " + x.MedicalWorker.LastName,
                        SpecializationType = x.Specialization.Type,
                        Day = x.Day.ToString(),
                        TimeSpan = x.TimeStart.ToShortTimeString() + " - " + x.TimeEnd.ToShortTimeString(),
                        TimeStart = x.TimeStart,
                        TimeEnd = x.TimeEnd,
                        VisitPrice = x.VisitPrice.HasValue ? x.VisitPrice.Value.ToString("C", CultureInfo.CurrentCulture) : "NFZ"
                    })
                    .ToList();
            }
        }

        public static List<AvailabilitiesForScheduleTab> GetRepresentation(DayOfWeek dayOfWeek)
        {
            using (var context = new DataBaseContext())
            {
                return context.Availabilities
                    .Where(x => x.Day == dayOfWeek)
                    .AsEnumerable()
                    .Select(x => new AvailabilitiesForScheduleTab()
                    {
                        Id = x.Id,
                        MedicalWorkerId = x.MedicalWorker.Id,
                        SpecializationId = x.Specialization.Id,
                        MedicalWorkerFullName = x.MedicalWorker.FirstName + " " + x.MedicalWorker.LastName,
                        SpecializationType = x.Specialization.Type,
                        Day = x.Day.ToString(),
                        TimeSpan = x.TimeStart.ToShortTimeString() + " - " + x.TimeEnd.ToShortTimeString(),
                        TimeStart = x.TimeStart,
                        TimeEnd = x.TimeEnd,
                        VisitPrice = x.VisitPrice.HasValue ? x.VisitPrice.Value.ToString("C", CultureInfo.CurrentCulture) : "NFZ"
                    })
                    .ToList();
            }
        }
    }
}
