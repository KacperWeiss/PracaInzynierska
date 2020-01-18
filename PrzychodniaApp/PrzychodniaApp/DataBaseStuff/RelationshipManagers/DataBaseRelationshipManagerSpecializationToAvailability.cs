using PrzychodniaApp.DataBaseStuff.Models;
using PrzychodniaApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.DataBaseStuff.RelationshipManagers
{
    public static class DataBaseRelationshipManagerSpecializationToAvailability
    {
        public static void AddSingleRelationship(DataBaseContext context, DbSpecialization specialization, DbAvailability availability)
        {
            context.Specializations.SingleOrDefault(s => s.Id == specialization.Id).Availabilities.Add(context.Availabilities.SingleOrDefault(a => a.Id == availability.Id));
            context.Availabilities.SingleOrDefault(a => a.Id == availability.Id).Specialization = context.Specializations.SingleOrDefault(s => s.Id == specialization.Id);
        }

        public static void AddSingleToManyRelationships(DataBaseContext context, DbSpecialization specialization, List<DbAvailability> availabilities)
        {
            foreach (var availability in availabilities)
            {
                AddSingleRelationship(context, specialization, availability);
            }
        }

        public static void AddManySingleToManyRelationships(DataBaseContext context, List<DbSpecialization> specializations, List<List<DbAvailability>> availabilities)
        {
            if (specializations.Count != availabilities.Count)
            {
                throw new UnequalCollectionsException("Method AddManySingleToManyRelationships has 2 unequal Collections to link!");
            }
            for (int i = 0; i < specializations.Count; i++)
            {
                AddSingleToManyRelationships(context, specializations[i], availabilities[i]);
            }
        }
    }
}
