using System.Data.Entity;

namespace PrzychodniaApp.DataBaseStuff
{
    class DataBaseInitiaizerForDev : DropCreateDatabaseIfModelChanges<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {
            try
            {

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
