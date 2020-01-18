using PrzychodniaApp.DataBaseStuff.Models;
using PrzychodniaApp.DataBaseStuff.RelationshipManagers;
using PrzychodniaApp.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace PrzychodniaApp.DataBaseStuff
{
    class DataBaseInitiaizerForDev : DropCreateDatabaseIfModelChanges<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {
            try
            {
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
                        Login = "PracownikMedyczny",
                        Password = "PracownikMedyczny",
                        UserAccess = UserAccess.MedicalWorker
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
                        MedicalWorker = new DbMedicalWorker(),
                        Specialization = new DbSpecialization()
                    },
                    new DbAvailability()
                    {
                        Day = DayOfWeek.Wednesday,
                        TimeStart = new DateTime(2020, 01, 22, 7, 30, 0),
                        TimeEnd = new DateTime(2020, 01, 22, 15, 30, 0),
                        MaxAmountOfVisits = 30,
                        VisitPrice = 0,
                        MedicalWorker = new DbMedicalWorker(),
                        Specialization = new DbSpecialization()
                    },
                    new DbAvailability()
                    {
                        Day = DayOfWeek.Friday,
                        TimeStart = new DateTime(2020, 01, 24, 7, 30, 0),
                        TimeEnd = new DateTime(2020, 01, 24, 15, 30, 0),
                        MaxAmountOfVisits = 30,
                        VisitPrice = 0,
                        MedicalWorker = new DbMedicalWorker(),
                        Specialization = new DbSpecialization()
                    },
                    new DbAvailability()
                    {
                        Day = DayOfWeek.Tuesday,
                        TimeStart = new DateTime(2020, 01, 21, 7, 30, 0),
                        TimeEnd = new DateTime(2020, 01, 21, 15, 30, 0),
                        MaxAmountOfVisits = 30,
                        VisitPrice = 0,
                        MedicalWorker = new DbMedicalWorker(),
                        Specialization = new DbSpecialization()
                    },
                    new DbAvailability()
                    {
                        Day = DayOfWeek.Thursday,
                        TimeStart = new DateTime(2020, 01, 23, 7, 30, 0),
                        TimeEnd = new DateTime(2020, 01, 23, 15, 30, 0),
                        MaxAmountOfVisits = 30,
                        VisitPrice = 0,
                        MedicalWorker = new DbMedicalWorker(),
                        Specialization = new DbSpecialization()
                    },
                    new DbAvailability()
                    {
                        Day = DayOfWeek.Monday,
                        TimeStart = new DateTime(2020, 01, 22, 11, 30, 0),
                        TimeEnd = new DateTime(2020, 01, 22, 15, 30, 0),
                        MaxAmountOfVisits = 14,
                        VisitPrice = 25,
                        MedicalWorker = new DbMedicalWorker(),
                        Specialization = new DbSpecialization()
                    },
                    new DbAvailability()
                    {
                        Day = DayOfWeek.Thursday,
                        TimeStart = new DateTime(2020, 01, 22, 11, 30, 0),
                        TimeEnd = new DateTime(2020, 01, 22, 15, 30, 0),
                        MaxAmountOfVisits = 14,
                        VisitPrice = 50,
                        MedicalWorker = new DbMedicalWorker(),
                        Specialization = new DbSpecialization()
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

                var Patients = new List<DbPatient>()
                {
                    new DbPatient()
                    {
                        PatientsPronounce = Pronouns.MR,
                        EmailContact = EmailContact.Full,
                        FirstName = "Patient1FN",
                        LastName = "Patient1LN",
                        EmailAdress = "testmail_1@testmail.com",
                        DateOfBirth = new DateTime(2010, 02, 21),
                        TreatmentHistory = new DbTreatmentHistory(),
                        Visits = new List<DbVisit>()
                    },
                    new DbPatient()
                    {
                        PatientsPronounce = Pronouns.MRS,
                        EmailContact = EmailContact.Full,
                        FirstName = "Patient2FN",
                        LastName = "Patient2LN",
                        EmailAdress = "testmail_2@testmail.com",
                        DateOfBirth = new DateTime(2008, 12, 11),
                        TreatmentHistory = new DbTreatmentHistory(),
                        Visits = new List<DbVisit>()
                    }
                };
                Patients.ForEach(p => context.Patients.AddOrUpdate(x => x.Id, p));
                context.SaveChanges();

                var Visits = new List<DbVisit>()
                {
                    new DbVisit()
                    {
                        TimeStart = new DateTime(2020, 02, 12, 12, 00, 00), //Wednesday
                        OptionalDescription = "Test Visit1",
                        Specialization = new DbSpecialization(),
                        MedicalWorker = new DbMedicalWorker(),
                        Patient = new DbPatient()
                    },
                    new DbVisit()
                    {
                        TimeStart = new DateTime(2020, 02, 13, 12, 00, 00), //Thursday
                        OptionalDescription = "Test Visit2",
                        Specialization = new DbSpecialization(),
                        MedicalWorker = new DbMedicalWorker(),
                        Patient = new DbPatient()
                    }
                };
                Visits.ForEach(v => context.Visits.AddOrUpdate(x => x.Id, v));
                context.SaveChanges();

                DatabaseRelationshipManagerVisit.AddSingleCompleteRelationships(context, Visits[0], MedicalWorkers[0], Patients[0], Specializations[0]);
                DatabaseRelationshipManagerVisit.AddSingleCompleteRelationships(context, Visits[1], MedicalWorkers[1], Patients[1], Specializations[0]);
                context.SaveChanges();

                var TreatmentHistories = new List<DbTreatmentHistory>()
                {
                    new DbTreatmentHistory()
                    {
                        LastVisit = new DbVisit(),
                        Treatments = new List<DbTreatment>(),
                        PastVaccinations = new List<DbVaccination>(),
                        RequiredVaccinations = new List<DbVaccination>()
                    },
                    new DbTreatmentHistory()
                    {
                        LastVisit = new DbVisit(),
                        Treatments = new List<DbTreatment>(),
                        PastVaccinations = new List<DbVaccination>(),
                        RequiredVaccinations = new List<DbVaccination>()
                    }
                };
                TreatmentHistories.ForEach(th => context.TreatmentHistories.AddOrUpdate(x => x.Id, th));
                context.SaveChanges();

                DataBaseRelationshipManagerTreatmentHistory.AddSingleRelationship(context, TreatmentHistories[0], Patients[0]);
                DataBaseRelationshipManagerTreatmentHistory.AddSingleRelationship(context, TreatmentHistories[1], Patients[1]);
                context.SaveChanges();

                var Treatments = new List<DbTreatment>()
                {
                    new DbTreatment()
                    {
                        IllnessName = "Cold",
                        SymptomsDescription = "Patient have symptoms of cold",
                        Prescription = new List<DbPrescribedMedications>(),
                        Visit = new DbVisit()
                    },
                    new DbTreatment()
                    {
                        IllnessName = "Cold",
                        SymptomsDescription = "Patient have symptoms of cold",
                        Prescription = new List<DbPrescribedMedications>(),
                        Visit = new DbVisit()
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
                context.SaveChanges();

                var PastVisits = new List<DbVisit>()
                {
                    new DbVisit()
                    {
                        TimeStart = new DateTime(2020, 01, 15, 12, 00, 00), //Wednesday
                        OptionalDescription = "Test PastVisit1",
                        Specialization = new DbSpecialization(),
                        MedicalWorker = new DbMedicalWorker(),
                        Patient = new DbPatient()
                    },
                    new DbVisit()
                    {
                        TimeStart = new DateTime(2020, 01, 16, 12, 00, 00), //Thursday
                        OptionalDescription = "Test PastVisit2",
                        Specialization = new DbSpecialization(),
                        MedicalWorker = new DbMedicalWorker(),
                        Patient = new DbPatient()
                    }
                };
                PastVisits.ForEach(pv => context.Visits.AddOrUpdate(x => x.Id, pv));
                context.SaveChanges();

                DatabaseRelationshipManagerVisit.AddSingleCompleteRelationships(context, PastVisits[0], MedicalWorkers[0], Patients[0], Specializations[0]);
                DatabaseRelationshipManagerVisit.AddSingleCompleteRelationships(context, PastVisits[1], MedicalWorkers[1], Patients[1], Specializations[0]);
                DatabaseRelationshipManagerVisit.AddSingleRelationship(context, PastVisits[0], TreatmentHistories[0]);
                DatabaseRelationshipManagerVisit.AddSingleRelationship(context, PastVisits[1], TreatmentHistories[1]);
                DatabaseRelationshipManagerVisit.AddSingleRelationship(context, PastVisits[0], Treatments[0]);
                DatabaseRelationshipManagerVisit.AddSingleRelationship(context, PastVisits[1], Treatments[1]);
                context.SaveChanges();

                var Vaccinations = new List<DbVaccination>()
                {
                    new DbVaccination()
                    {
                        VaccinesName = "Flu",
                        VaccineStatus = VaccineStatus.ObligatoryByDate,
                        ObligatoryBy = new DateTime(2020, 10, 10),
                        VaccinationDate = new DateTime()
                    },
                    new DbVaccination()
                    {
                        VaccinesName = "Illness1",
                        VaccineStatus = VaccineStatus.Optional,
                        ObligatoryBy = new DateTime(),
                        VaccinationDate = new DateTime()
                    },
                    new DbVaccination()
                    {
                        VaccinesName = "Illness2",
                        VaccineStatus = VaccineStatus.ObligatoryAlreadyVaccined,
                        ObligatoryBy = new DateTime(2020, 01 , 10),
                        VaccinationDate = new DateTime(2020, 01, 05)
                    },
                    new DbVaccination()
                    {
                        VaccinesName = "Flu",
                        VaccineStatus = VaccineStatus.ObligatoryByDate,
                        ObligatoryBy = new DateTime(2020, 10, 10),
                        VaccinationDate = new DateTime()
                    },
                    new DbVaccination()
                    {
                        VaccinesName = "Illness1",
                        VaccineStatus = VaccineStatus.Optional,
                        ObligatoryBy = new DateTime(),
                        VaccinationDate = new DateTime()
                    },
                    new DbVaccination()
                    {
                        VaccinesName = "Illness2",
                        VaccineStatus = VaccineStatus.ObligatoryAlreadyVaccined,
                        ObligatoryBy = new DateTime(2020, 01 , 10),
                        VaccinationDate = new DateTime(2020, 01, 05)
                    }
                };
                Vaccinations.ForEach(v => context.Vaccinations.AddOrUpdate(x => x.Id, v));
                context.SaveChanges();

                DataBaseRelationshipManagerTreatmentHistory.AddManyRelationshipsToRequiredVaccinations(context, TreatmentHistories[0], Vaccinations.GetRange(0, 3));
                DataBaseRelationshipManagerTreatmentHistory.AddManyRelationshipsToRequiredVaccinations(context, TreatmentHistories[1], Vaccinations.GetRange(3, 3));
                DataBaseRelationshipManagerTreatmentHistory.AddSingleRelationshipToPastVaccinations(context, TreatmentHistories[0], Vaccinations[3]);
                DataBaseRelationshipManagerTreatmentHistory.AddSingleRelationshipToPastVaccinations(context, TreatmentHistories[1], Vaccinations[6]);
                context.SaveChanges();

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
