using PrzychodniaApp.DataBaseStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.UserControlers.DataRepresantations
{
    public class SpecializationsForScheduleTab
    {
        public int SpecializationId { get; set; }
        public string SpecializationType { get; set; }

        public static List<SpecializationsForScheduleTab> GetRepresentation()
        {
            using (var context = new DataBaseContext())
            {
                return context.Specializations
                    .AsEnumerable()
                    .Select(x => new SpecializationsForScheduleTab()
                    {
                        SpecializationId = x.Id,
                        SpecializationType = x.Type
                    })
                    .ToList();
            }
        }
        public static List<SpecializationsForScheduleTab> GetRepresentation(int medicalWorkerId)
        {
            using (var context = new DataBaseContext())
            {
                return context.MedicalWorkers
                    .Include(x => x.Specializations)
                    .Single(x => x.Id == medicalWorkerId)
                    .Specializations
                    .AsEnumerable()
                    .Select(x => new SpecializationsForScheduleTab()
                    {
                        SpecializationId = x.Id,
                        SpecializationType = x.Type
                    })
                    .ToList();
            }
        }
    }
}
