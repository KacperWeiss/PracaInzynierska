using PrzychodniaApp.DataBaseStuff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.DataBaseStuff.RelationshipManagers
{
    public static class DatabaseRelationshipManagerVisit
    {
        public static void AddSingleCompleteRelationships(DataBaseContext context, DbVisit visit, DbMedicalWorker medicalWorker, DbPatient patient, DbSpecialization specialization)
        {
            if (!medicalWorker.Specializations.Contains(context.Specializations.SingleOrDefault(x => x.Id == specialization.Id)))
            {
                throw new Exception("In function AddSingleCompleteRelationships medicalWorker doesn't have required specialization");
            }
            AddSingleRelationship(context, visit, medicalWorker);
            AddSingleRelationship(context, visit, patient);
            AddSingleRelationship(context, visit, specialization);
        }

        public static void AddSingleRelationship(DataBaseContext context, DbVisit visit, DbTreatment treatment)
        {
            context.Treatments.SingleOrDefault(t => t.Id == treatment.Id).Visit = context.Visits.SingleOrDefault(v => v.Id == visit.Id);
        }

        private static void AddSingleRelationship(DataBaseContext context, DbVisit visit, DbMedicalWorker medicalWorker)
        {
            context.Visits.SingleOrDefault(v => v.Id == visit.Id).MedicalWorker = context.MedicalWorkers.SingleOrDefault(mw => mw.Id == medicalWorker.Id);
            context.MedicalWorkers.SingleOrDefault(mw => mw.Id == medicalWorker.Id).Visits.Add(context.Visits.SingleOrDefault(v => v.Id == visit.Id));
        }

        private static void AddSingleRelationship(DataBaseContext context, DbVisit visit, DbPatient patient)
        {
            context.Visits.SingleOrDefault(v => v.Id == visit.Id).Patient = context.Patients.SingleOrDefault(p => p.Id == patient.Id);
            context.Patients.SingleOrDefault(p => p.Id == patient.Id).Visits.Add(context.Visits.SingleOrDefault(v => v.Id == visit.Id));
        }

        private static void AddSingleRelationship(DataBaseContext context, DbVisit visit, DbSpecialization specialization)
        {
            context.Visits.SingleOrDefault(v => v.Id == visit.Id).Specialization = context.Specializations.SingleOrDefault(s => s.Id == specialization.Id);
        }
    }
}
