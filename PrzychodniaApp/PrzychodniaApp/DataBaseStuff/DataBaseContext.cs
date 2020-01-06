namespace PrzychodniaApp.DataBaseStuff
{
    using PrzychodniaApp.DataBaseStuff.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DataBaseContext : DbContext
    {
        // Your context has been configured to use a 'MainContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'PrzychodniaApp.Models.MainContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MainContext' 
        // connection string in the application configuration file.
        public DataBaseContext() : base("name=MainContext")
        {
            Database.SetInitializer(new DataBaseInitiaizerForDev());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<MedicalWorker> MedicalWorkers
    }
}