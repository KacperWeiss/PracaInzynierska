using PrzychodniaApp.DataBaseStuff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.DataBaseStuff.RelationshipManagers
{
    public static class DataBaseRelationshipManagerTreatmentHistory
    {
        public static void AddSingleRelationship(DataBaseContext context, DbTreatmentHistory treatmentHistory, DbPatient patient)
        {
            context.Patients.SingleOrDefault(p => p.Id == patient.Id).TreatmentHistory = context.TreatmentHistories.SingleOrDefault(th => th.Id == treatmentHistory.Id);
        }

        public static void AddSingleRelationship(DataBaseContext context, DbTreatmentHistory treatmentHistory, DbTreatment treatment)
        {
            context.TreatmentHistories.SingleOrDefault(th => th.Id == treatmentHistory.Id).Treatments.Add(context.Treatments.SingleOrDefault(t => t.Id == treatment.Id));
        }

        public static void AddSingleRelationshipToRequiredVaccinations(DataBaseContext context, DbTreatmentHistory treatmentHistory, DbVaccination vaccination)
        {
            context.TreatmentHistories.SingleOrDefault(th => th.Id == treatmentHistory.Id).RequiredVaccinations.Add(context.Vaccinations.SingleOrDefault(v => v.Id == vaccination.Id));
        }

        public static void AddSingleRelationshipToPastVaccinations(DataBaseContext context, DbTreatmentHistory treatmentHistory, DbVaccination vaccination)
        {
            context.TreatmentHistories.SingleOrDefault(th => th.Id == treatmentHistory.Id).PastVaccinations.Add(context.Vaccinations.SingleOrDefault(v => v.Id == vaccination.Id));
        }

        public static void AddManyRelationshipsToRequiredVaccinations(DataBaseContext context, DbTreatmentHistory treatmentHistory, List<DbVaccination> vaccinations)
        {
            foreach (var vacination in vaccinations)
            {
                AddSingleRelationshipToRequiredVaccinations(context, treatmentHistory, vacination);
            }
        }

        public static void AddManyRelationshipsToPastVaccinations(DataBaseContext context, DbTreatmentHistory treatmentHistory, List<DbVaccination> vaccinations)
        {
            foreach (var vacination in vaccinations)
            {
                AddSingleRelationshipToPastVaccinations(context, treatmentHistory, vacination);
            }
        }
    }
}
