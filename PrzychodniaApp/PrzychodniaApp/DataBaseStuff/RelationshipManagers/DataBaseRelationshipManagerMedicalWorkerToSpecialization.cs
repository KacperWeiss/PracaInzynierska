using PrzychodniaApp.DataBaseStuff.Models;
using PrzychodniaApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.DataBaseStuff
{
    public static class DataBaseRelationshipManagerMedicalWorkerToSpecialization
    {
        public static void AddSingleRelationship(DataBaseContext context, DbMedicalWorker medicalWorker, DbSpecialization specialization)
        {
            context.MedicalWorkers.SingleOrDefault(mw => mw.Id == medicalWorker.Id).Specializations.Add(context.Specializations.SingleOrDefault(s => s.Id == specialization.Id));
            context.Specializations.SingleOrDefault(s => s.Id == specialization.Id).MedicalWorkers.Add(context.MedicalWorkers.SingleOrDefault(mw => mw.Id == medicalWorker.Id));
        }

        public static void AddSingleToManyRelationships(DataBaseContext context, DbMedicalWorker medicalWorkers, List<DbSpecialization> specializations)
        {
            foreach (var specialization in specializations)
            {
                AddSingleRelationship(context, medicalWorkers, specialization);
            }
        }

        public static void AddManyToManyRelationships(DataBaseContext context, List<DbMedicalWorker> medicalWorkers, List<DbSpecialization> specializations)
        {
            if (specializations.Count != medicalWorkers.Count)
            {
                throw new UnequalCollectionsException("Method AddManyToManyRelationships has 2 unequal Collections to link!");
            }
            for (int i = 0; i < specializations.Count; i++)
            {
                AddSingleRelationship(context, medicalWorkers[i], specializations[i]);
            }
        }

        public static void AddManySingleToManyRelationships(DataBaseContext context, List<DbMedicalWorker> medicalWorkers, List<List<DbSpecialization>> specializations)
        {
            if (medicalWorkers.Count != specializations.Count)
            {
                throw new UnequalCollectionsException("Method AddManySingleToManyRelationships has 2 unequal Collections to link!");
            }
            for (int i = 0; i < specializations.Count; i++)
            {
                AddSingleToManyRelationships(context, medicalWorkers[i], specializations[i]);
            }
        }
    }
}
