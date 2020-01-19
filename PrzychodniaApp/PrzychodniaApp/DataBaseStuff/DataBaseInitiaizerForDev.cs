using PrzychodniaApp.DataBaseStuff.Models;
using PrzychodniaApp.DataBaseStuff.RelationshipManagers;
using PrzychodniaApp.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace PrzychodniaApp.DataBaseStuff
{
    class DataBaseInitiaizerForDev : DropCreateDatabaseIfModelChanges<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {
            try
            {
                var MedicalWorkers = new List<DbMedicalWorker>()
                {
                    new DbMedicalWorker()
                    {
                        FirstName = "FN1",
                        LastName = "LN1",
                        Specializations = new List<DbSpecialization>(),
                        Availabilities = new List<DbAvailability>(),
                        Visits = new List<DbVisit>()
                    },
                    new DbMedicalWorker()
                    {
                        FirstName = "FN2",
                        LastName = "LN2",
                        Specializations = new List<DbSpecialization>(),
                        Availabilities = new List<DbAvailability>(),
                        Visits = new List<DbVisit>()
                    }
                };
                MedicalWorkers.ForEach(mw => context.MedicalWorkers.AddOrUpdate(x => x.Id, mw));
                context.SaveChanges();

                var Users = new List<DbUser>()
                {
                    new DbUser()
                    {
                        Login = "Admin",
                        Password = "Admin",
                        UserAccess = UserAccess.Admin
                    },
                    new DbUser()
                    {
                        Login = "Recepcja",
                        Password = "Recepcja",
                        UserAccess = UserAccess.Reception
                    },
                    new DbUser()
                    {
                        Login = "PracownikMedyczny0",
                        Password = "PracownikMedyczny0",
                        UserAccess = UserAccess.MedicalWorker,
                        MedicalWorker = MedicalWorkers[0]
                    },
                    new DbUser()
                    {
                        Login = "PracownikMedyczny1",
                        Password = "PracownikMedyczny1",
                        UserAccess = UserAccess.MedicalWorker,
                        MedicalWorker = MedicalWorkers[1]
                    }

                };
                Users.ForEach(u => context.Users.AddOrUpdate(x => x.Id, u));
                context.SaveChanges();

                var Specializations = new List<DbSpecialization>()
                {
                    new DbSpecialization()
                    {
                        Type = "Family Physician",
                        Availabilities = new List<DbAvailability>(),
                        MedicalWorkers = new List<DbMedicalWorker>()
                    },
                    new DbSpecialization()
                    {
                        Type = "Internal Medicine Physician",
                        Availabilities = new List<DbAvailability>(),
                        MedicalWorkers = new List<DbMedicalWorker>()
                    },
                    new DbSpecialization()
                    {
                        Type = "Pediatrician",
                        Availabilities = new List<DbAvailability>(),
                        MedicalWorkers = new List<DbMedicalWorker>()
                    },
                    new DbSpecialization()
                    {
                        Type = "Psychiatrist",
                        Availabilities = new List<DbAvailability>(),
                        MedicalWorkers = new List<DbMedicalWorker>()
                    },
                    new DbSpecialization()
                    {
                        Type = "Cardiologist",
                        Availabilities = new List<DbAvailability>(),
                        MedicalWorkers = new List<DbMedicalWorker>()
                    }
                };
                Specializations.ForEach(s => context.Specializations.AddOrUpdate(x => x.Type, s));
                context.SaveChanges();

                DataBaseRelationshipManagerMedicalWorkerToSpecialization.AddSingleToManyRelationships(context, MedicalWorkers[0], Specializations.GetRange(0, 3)); 
                DataBaseRelationshipManagerMedicalWorkerToSpecialization.AddSingleToManyRelationships(context, MedicalWorkers[1], Specializations.GetRange(0, 3)); 
                DataBaseRelationshipManagerMedicalWorkerToSpecialization.AddSingleRelationship(context, MedicalWorkers[0], Specializations[3]); 
                DataBaseRelationshipManagerMedicalWorkerToSpecialization.AddSingleRelationship(context, MedicalWorkers[1], Specializations[4]); 
                context.SaveChanges();

                var Availabilities = new List<DbAvailability>()
                {
                    new DbAvailability()
                    {
                        Day = DayOfWeek.Monday,
                        TimeStart = new DateTime(2020, 01, 20, 7, 30, 0),
                        TimeEnd = new DateTime(2020, 01, 20, 15, 30, 0),
                        MaxAmountOfVisits = 30,
                        VisitPrice = 0,
                        MedicalWorker = MedicalWorkers[0],
                        Specialization = Specializations.Single(x => x.Type == "Family Physician")
                    },
                    new DbAvailability()
                    {
                        Day = DayOfWeek.Wednesday,
                        TimeStart = new DateTime(2020, 01, 22, 7, 30, 0),
                        TimeEnd = new DateTime(2020, 01, 22, 15, 30, 0),
                        MaxAmountOfVisits = 30,
                        VisitPrice = 0,
                        MedicalWorker = MedicalWorkers[0],
                        Specialization = Specializations.Single(x => x.Type == "Family Physician")
                    },
                    new DbAvailability()
                    {
                        Day = DayOfWeek.Friday,
                        TimeStart = new DateTime(2020, 01, 24, 7, 30, 0),
                        TimeEnd = new DateTime(2020, 01, 24, 15, 30, 0),
                        MaxAmountOfVisits = 30,
                        VisitPrice = 0,
                        MedicalWorker = MedicalWorkers[0],
                        Specialization = Specializations.Single(x => x.Type == "Family Physician")
                    },
                    new DbAvailability()
                    {
                        Day = DayOfWeek.Tuesday,
                        TimeStart = new DateTime(2020, 01, 21, 7, 30, 0),
                        TimeEnd = new DateTime(2020, 01, 21, 15, 30, 0),
                        MaxAmountOfVisits = 30,
                        VisitPrice = 0,
                        MedicalWorker = MedicalWorkers[1],
                        Specialization = Specializations.Single(x => x.Type == "Family Physician")
                    },
                    new DbAvailability()
                    {
                        Day = DayOfWeek.Thursday,
                        TimeStart = new DateTime(2020, 01, 23, 7, 30, 0),
                        TimeEnd = new DateTime(2020, 01, 23, 15, 30, 0),
                        MaxAmountOfVisits = 30,
                        VisitPrice = 0,
                        MedicalWorker = MedicalWorkers[1],
                        Specialization = Specializations.Single(x => x.Type == "Family Physician")
                    },
                    new DbAvailability()
                    {
                        Day = DayOfWeek.Monday,
                        TimeStart = new DateTime(2020, 01, 22, 11, 30, 0),
                        TimeEnd = new DateTime(2020, 01, 22, 15, 30, 0),
                        MaxAmountOfVisits = 14,
                        VisitPrice = 25,
                        MedicalWorker = MedicalWorkers[0],
                        Specialization = Specializations.Single(x => x.Type == "Psychiatrist")
                    },
                    new DbAvailability()
                    {
                        Day = DayOfWeek.Thursday,
                        TimeStart = new DateTime(2020, 01, 22, 11, 30, 0),
                        TimeEnd = new DateTime(2020, 01, 22, 15, 30, 0),
                        MaxAmountOfVisits = 14,
                        VisitPrice = 50,
                        MedicalWorker = MedicalWorkers[1],
                        Specialization = Specializations.Single(x => x.Type == "Cardiologist")
                    }
                };
                Availabilities.ForEach(a => context.Availabilities.AddOrUpdate(x => x.Id, a));
                context.SaveChanges();

                DataBaseRelationshipManagerSpecializationToAvailability.AddSingleToManyRelationships(context, Specializations[0], Availabilities.GetRange(0, 5));
                DataBaseRelationshipManagerSpecializationToAvailability.AddSingleRelationship(context, Specializations[3], Availabilities[5]);
                DataBaseRelationshipManagerSpecializationToAvailability.AddSingleRelationship(context, Specializations[4], Availabilities[6]);

                DataBaseRelationshipManagerMedicalWorkerToAvailability.AddSingleToManyRelationships(context, MedicalWorkers[0], Availabilities.GetRange(0, 3));
                DataBaseRelationshipManagerMedicalWorkerToAvailability.AddSingleToManyRelationships(context, MedicalWorkers[1], Availabilities.GetRange(3, 2));
                DataBaseRelationshipManagerMedicalWorkerToAvailability.AddSingleRelationship(context, MedicalWorkers[0], Availabilities[5]);
                DataBaseRelationshipManagerMedicalWorkerToAvailability.AddSingleRelationship(context, MedicalWorkers[1], Availabilities[6]);
                context.SaveChanges();

                var TreatmentHistories = new List<DbTreatmentHistory>()
                {
                    new DbTreatmentHistory()
                    {
                        Treatments = new List<DbTreatment>(),
                        PastVaccinations = new List<DbVaccination>(),
                        RequiredVaccinations = new List<DbVaccination>()
                    },
                    new DbTreatmentHistory()
                    {
                        Treatments = new List<DbTreatment>(),
                        PastVaccinations = new List<DbVaccination>(),
                        RequiredVaccinations = new List<DbVaccination>()
                    }
                };
                TreatmentHistories.ForEach(th => context.TreatmentHistories.AddOrUpdate(x => x.Id, th));
                context.SaveChanges();

                var Patients = new List<DbPatient>()
                {
                    new DbPatient()
                    {
                        PatientsPronounce = Pronouns.MR,
                        EmailContact = EmailContact.Full,
                        FirstName = "Patient1FN",
                        LastName = "Patient1LN",
                        EmailAdress = "testmail_1@testmail.com",
                        DateOfBirth = new DateTime(2010, 2, 21),
                        TreatmentHistory = TreatmentHistories[0],
                        Visits = new List<DbVisit>()
                    },
                    new DbPatient()
                    {
                        PatientsPronounce = Pronouns.MRS,
                        EmailContact = EmailContact.Full,
                        FirstName = "Patient2FN",
                        LastName = "Patient2LN",
                        EmailAdress = "testmail_2@testmail.com",
                        DateOfBirth = new DateTime(2008, 11, 11),
                        TreatmentHistory = TreatmentHistories[1],
                        Visits = new List<DbVisit>()
                    }
                };
                Patients.ForEach(p => context.Patients.AddOrUpdate(x => x.Id, p));
                context.SaveChanges();

                DataBaseRelationshipManagerTreatmentHistory.AddSingleRelationship(context, TreatmentHistories[0], Patients[0]);
                DataBaseRelationshipManagerTreatmentHistory.AddSingleRelationship(context, TreatmentHistories[1], Patients[1]);
                context.SaveChanges();

                var Visits = new List<DbVisit>()
                {
                    new DbVisit()
                    {
                        TimeStart = new DateTime(2020, 2, 12, 12, 0, 0), //Wednesday
                        OptionalDescription = "Test Visit1",
                        Specialization = Specializations[0],
                        MedicalWorker = MedicalWorkers[0],
                        Patient = Patients[0]
                    },
                    new DbVisit()
                    {
                        TimeStart = new DateTime(2020, 2, 13, 12, 0, 0), //Thursday
                        OptionalDescription = "Test Visit2",
                        Specialization = Specializations[0],
                        MedicalWorker = MedicalWorkers[1],
                        Patient = Patients[1]
                    }
                };
                Visits.ForEach(v => context.Visits.AddOrUpdate(x => x.Id, v));

                var PastVisits = new List<DbVisit>()
                {
                    new DbVisit()
                    {
                        TimeStart = new DateTime(2020, 1, 15, 12, 0, 0), //Wednesday
                        OptionalDescription = "Test PastVisit1",
                        Specialization = Specializations[0],
                        MedicalWorker = MedicalWorkers[0],
                        Patient = Patients[0]
                    },
                    new DbVisit()
                    {
                        TimeStart = new DateTime(2020, 1, 16, 12, 0, 0), //Thursday
                        OptionalDescription = "Test PastVisit2",
                        Specialization = Specializations[0],
                        MedicalWorker = MedicalWorkers[1],
                        Patient = Patients[1]
                    }
                };
                PastVisits.ForEach(pv => context.Visits.AddOrUpdate(x => x.Id, pv));
                context.SaveChanges();

                DatabaseRelationshipManagerVisit.AddSingleCompleteRelationships(context, Visits[0], MedicalWorkers[0], Patients[0], Specializations[0]);
                DatabaseRelationshipManagerVisit.AddSingleCompleteRelationships(context, Visits[1], MedicalWorkers[1], Patients[1], Specializations[0]);
                DatabaseRelationshipManagerVisit.AddSingleCompleteRelationships(context, PastVisits[0], MedicalWorkers[0], Patients[0], Specializations[0]);
                DatabaseRelationshipManagerVisit.AddSingleCompleteRelationships(context, PastVisits[1], MedicalWorkers[1], Patients[1], Specializations[0]);
                context.SaveChanges();

                var Treatments = new List<DbTreatment>()
                {
                    new DbTreatment()
                    {
                        IllnessName = "Cold",
                        SymptomsDescription = "Patient have symptoms of cold",
                        Prescription = new List<DbPrescribedMedications>(),
                        Visit = PastVisits[0]
                    },
                    new DbTreatment()
                    {
                        IllnessName = "Cold",
                        SymptomsDescription = "Patient have symptoms of cold",
                        Prescription = new List<DbPrescribedMedications>(),
                        Visit = PastVisits[1]
                    }
                };
                Treatments.ForEach(t => context.Treatments.AddOrUpdate(x => x.Id, t));
                context.SaveChanges();

                DataBaseRelationshipManagerTreatmentHistory.AddSingleRelationship(context, TreatmentHistories[0], Treatments[0]);
                DataBaseRelationshipManagerTreatmentHistory.AddSingleRelationship(context, TreatmentHistories[1], Treatments[1]);
                context.SaveChanges();

                var PrescribedMedications = new List<DbPrescribedMedications>()
                {
                    new DbPrescribedMedications()
                    {
                        Dosage = "3x10ml a day",
                        MedicinesName = "Antitussive syrup"
                    },
                    new DbPrescribedMedications()
                    {
                        Dosage = "3x10ml a day",
                        MedicinesName = "Antitussive syrup"
                    }
                };
                PrescribedMedications.ForEach(pm => context.PrescribedMedications.AddOrUpdate(x => x.Id, pm));
                context.SaveChanges();

                DataBaseRelationshipManagerTreatmentToPrescribedMedications.AddSingleRelationship(context, Treatments[0], PrescribedMedications[0]);
                DataBaseRelationshipManagerTreatmentToPrescribedMedications.AddSingleRelationship(context, Treatments[1], PrescribedMedications[1]);
                DatabaseRelationshipManagerVisit.AddSingleRelationship(context, PastVisits[0], Treatments[0]);
                DatabaseRelationshipManagerVisit.AddSingleRelationship(context, PastVisits[1], Treatments[1]);
                context.SaveChanges();

                var Vaccinations = new List<DbVaccination>()
                {
                    new DbVaccination()
                    {
                        VaccinesName = "Flu",
                        VaccineStatus = VaccineStatus.ObligatoryByDate,
                        ObligatoryBy = new DateTime(2020, 10, 10)
                    },
                    new DbVaccination()
                    {
                        VaccinesName = "Illness1",
                        VaccineStatus = VaccineStatus.Optional
                    },
                    new DbVaccination()
                    {
                        VaccinesName = "Illness2",
                        VaccineStatus = VaccineStatus.ObligatoryAlreadyVaccined,
                        ObligatoryBy = new DateTime(2020, 1 , 10),
                        VaccinationDate = new DateTime(2020, 1, 05)
                    },
                    new DbVaccination()
                    {
                        VaccinesName = "Flu",
                        VaccineStatus = VaccineStatus.ObligatoryByDate,
                        ObligatoryBy = new DateTime(2020, 10, 10)
                    },
                    new DbVaccination()
                    {
                        VaccinesName = "Illness1",
                        VaccineStatus = VaccineStatus.Optional
                    },
                    new DbVaccination()
                    {
                        VaccinesName = "Illness2",
                        VaccineStatus = VaccineStatus.ObligatoryAlreadyVaccined,
                        ObligatoryBy = new DateTime(2020, 1 , 10),
                        VaccinationDate = new DateTime(2020, 1, 05)
                    }
                };
                Vaccinations.ForEach(v => context.Vaccinations.AddOrUpdate(x => x.Id, v));
                context.SaveChanges();

                DataBaseRelationshipManagerTreatmentHistory.AddManyRelationshipsToRequiredVaccinations(context, TreatmentHistories[0], Vaccinations.GetRange(0, 3));
                DataBaseRelationshipManagerTreatmentHistory.AddManyRelationshipsToRequiredVaccinations(context, TreatmentHistories[1], Vaccinations.GetRange(3, 3));
                DataBaseRelationshipManagerTreatmentHistory.AddSingleRelationshipToPastVaccinations(context, TreatmentHistories[0], Vaccinations[2]);
                DataBaseRelationshipManagerTreatmentHistory.AddSingleRelationshipToPastVaccinations(context, TreatmentHistories[1], Vaccinations[5]);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
