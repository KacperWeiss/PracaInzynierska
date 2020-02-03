namespace PrzychodniaApp.DataBaseStuff
{
    using PrzychodniaApp.DataBaseStuff.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("name=MainContext")
        {
            Database.SetInitializer(new DataBaseInitiaizerForDev());
        }

        public virtual DbSet<DbAvailability> Availabilities { get; set; }
        public virtual DbSet<DbMedicalWorker> MedicalWorkers { get; set; }
        public virtual DbSet<DbPatient> Patients { get; set; }
        public virtual DbSet<DbPrescribedMedications> PrescribedMedications { get; set; }
        public virtual DbSet<DbSpecialization> Specializations { get; set; }
        public virtual DbSet<DbTreatment> Treatments { get; set; }
        public virtual DbSet<DbTreatmentHistory> TreatmentHistories { get; set; }
        public virtual DbSet<DbVaccination> Vaccinations { get; set; }
        public virtual DbSet<DbVisit> Visits { get; set; }

        public virtual DbSet<DbUser> Users { get; set; }
    }
}