using PrzychodniaApp.DataBaseStuff.Models;
using PrzychodniaApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.DataBaseStuff.RelationshipManagers
{
    public static class DataBaseRelationshipManagerMedicalWorkerToAvailability
    {
        public static void AddSingleRelationship(DataBaseContext context, DbMedicalWorker medicalWorker, DbAvailability availability)
        {
            context.MedicalWorkers.SingleOrDefault(mw => mw.Id == medicalWorker.Id).Availabilities.Add(context.Availabilities.SingleOrDefault(a => a.Id == availability.Id));
            context.Availabilities.SingleOrDefault(a => a.Id == availability.Id).MedicalWorker = context.MedicalWorkers.SingleOrDefault(mw => mw.Id == medicalWorker.Id);
        }

        public static void AddSingleToManyRelationships(DataBaseContext context, DbMedicalWorker medicalWorker, List<DbAvailability> availabilities)
        {
            foreach (var availability in availabilities)
            {
                AddSingleRelationship(context, medicalWorker, availability);
            }
        }

        public static void AddManySingleToManyRelationships(DataBaseContext context, List<DbMedicalWorker> medicalWorker, List<List<DbAvailability>> availabilities)
        {
            if (medicalWorker.Count != availabilities.Count)
            {
                throw new UnequalCollectionsException("Method AddManySingleToManyRelationships has 2 unequal Collections to link!");
            }
            for (int i = 0; i < medicalWorker.Count; i++)
            {
                AddSingleToManyRelationships(context, medicalWorker[i], availabilities[i]);
            }
        }
    }
}
