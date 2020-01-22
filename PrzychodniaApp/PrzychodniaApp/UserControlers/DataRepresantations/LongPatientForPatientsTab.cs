using PrzychodniaApp.DataBaseStuff;
using PrzychodniaApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.UserControlers.DataRepresantations
{
    public class LongPatientForPatientsTab
    {
        public int PatientId { get; set; }
        public int TreatmentHistoryId { get; set; }
        public Pronouns PatientsGender { get; set; }
        public EmailContact EmailContact { get; set; }
        public string FullName { get; set; }
        public string EmailAdress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LastPrescription { get; set; }
        public string NextVisit { get; set; }
        public string NextVaccination { get; set; }

        public static LongPatientForPatientsTab GetRepresentation(int patientId)
        {
            string lastPrescription = GetLastPrescrition(patientId);
            using (var context = new DataBaseContext())
            {
                return context.Patients
                    .Include(x => x.Visits)
                    .Include(x => x.TreatmentHistory)
                    .AsEnumerable()
                    .Where(x => x.Id == patientId)
                    .Select(x => new LongPatientForPatientsTab()
                    {
                        PatientId = x.Id,
                        TreatmentHistoryId = x.TreatmentHistory.Id,
                        PatientsGender = x.PatientsPronounce,
                        EmailContact = x.EmailContact,
                        FullName = x.FirstName + " " + x.LastName,
                        EmailAdress = x.EmailAdress,
                        DateOfBirth = x.DateOfBirth,
                        LastPrescription = lastPrescription,
                        NextVisit = x.Visits.OrderBy(t => t.TimeStart).SingleOrDefault(t => t.TimeStart > DateTime.Now).TimeStart.ToString(),
                        NextVaccination = x.TreatmentHistory.RequiredVaccinations.OrderBy(t => t.ObligatoryBy).SingleOrDefault(t => t.ObligatoryBy > DateTime.Now).ObligatoryBy.Value.ToShortDateString()
                    })
                    .SingleOrDefault();
            }
        }

        private static string GetLastPrescrition(int patientId)
        {
            using (var context = new DataBaseContext())
            {
                var treatmentId = context.Patients
                    .Include(x => x.Visits)
                    .Include(x => x.TreatmentHistory)
                    .Include(x => x.TreatmentHistory.Treatments)
                    .Single(x => x.Id == patientId)
                    .TreatmentHistory
                    .Treatments
                    .OrderByDescending(x => x.Visit.TimeStart)
                    .Single(x => x.Visit.TimeStart < DateTime.Now).Id;

                var lastTreatment = context.Treatments
                    .Include(x => x.Prescription)
                    .Single(x => x.Id == treatmentId);

                var prescription = lastTreatment
                    .Prescription
                    .Select(x => new
                    {
                        Medicines = x.MedicinesName + ": " + x.Dosage
                    })
                    .ToList();

                return string.Join(",\n", prescription);
            }
        }
    }
}
