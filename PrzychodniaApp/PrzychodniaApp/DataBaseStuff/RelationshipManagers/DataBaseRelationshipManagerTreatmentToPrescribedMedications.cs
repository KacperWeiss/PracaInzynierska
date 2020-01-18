using PrzychodniaApp.DataBaseStuff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.DataBaseStuff.RelationshipManagers
{
    public static class DataBaseRelationshipManagerTreatmentToPrescribedMedications
    {
        public static void AddSingleRelationship(DataBaseContext context, DbTreatment treatment, DbPrescribedMedications prescribedMedications)
        {
            context.Treatments.SingleOrDefault(t => t.Id == treatment.Id).Prescription.Add(context.PrescribedMedications.SingleOrDefault(t => t.Id == treatment.Id));
        }
    }
}
