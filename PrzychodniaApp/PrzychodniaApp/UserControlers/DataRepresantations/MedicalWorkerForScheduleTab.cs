using PrzychodniaApp.DataBaseStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.UserControlers.DataRepresantations
{
    public class MedicalWorkerForScheduleTab
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public static List<MedicalWorkerForScheduleTab> GetRepresentation()
        {
            using (var context = new DataBaseContext())
            {
                return context.MedicalWorkers
                    .AsEnumerable()
                    .Select(x => new MedicalWorkerForScheduleTab()
                    {
                        Id = x.Id,
                        FullName = x.FirstName + " " + x.LastName
                    })
                    .ToList();
            }
        }

        public static List<MedicalWorkerForScheduleTab> GetRepresentation(int specializationId)
        {
            using (var context = new DataBaseContext())
            {
                return context.Specializations
                    .Include(x => x.MedicalWorkers)
                    .Single(x => x.Id == specializationId)
                    .MedicalWorkers
                    .AsEnumerable()
                    .Select(x => new MedicalWorkerForScheduleTab()
                    {
                        Id = x.Id,
                        FullName = x.FirstName + " " + x.LastName
                    })
                    .ToList();
            }
        }
    }
}
