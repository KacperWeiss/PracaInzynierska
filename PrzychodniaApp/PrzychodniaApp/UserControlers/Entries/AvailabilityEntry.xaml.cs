using PrzychodniaApp.DataBaseStuff;
using PrzychodniaApp.DataBaseStuff.Models;
using PrzychodniaApp.UserControlers.DataRepresantations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrzychodniaApp.UserControlers.Entries
{
    /// <summary>
    /// Interaction logic for AvailabilityEntry.xaml
    /// </summary>
    public partial class AvailabilityEntry : UserControl, INotifyPropertyChanged
    {
        DateTime TimeForNewVisit { get; set; } = new DateTime(2020, 2, 6, 7, 30, 0);

        public AvailabilityEntry()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            TimeForNewVisit = TimeForNewVisit.AddMinutes(15);
            VisitTimeStartSelectionTextBox.Text = TimeForNewVisit.Hour.ToString("00.#") + ":" + TimeForNewVisit.Minute.ToString("00.#");
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            TimeForNewVisit = TimeForNewVisit.AddMinutes(-15);
            VisitTimeStartSelectionTextBox.Text = TimeForNewVisit.Hour.ToString("00.#") + ":" + TimeForNewVisit.Minute.ToString("00.#");
        }

        private void RegisterVisitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataHolderForMainWindow.PatientId == -1)
                {
                    throw new Exception("You need to select patient first!");
                }
                using (var context = new DataBaseContext())
                {
                    DateTime dateTime = DateTime.Now;
                    for (int i = 1; i <= 7; i++)
                    {
                        if (DayOfWeekTextBlock.Text == DateTime.Now.AddDays(i).DayOfWeek.ToString())
                        {
                            dateTime = DateTime.Now.AddDays(i);
                            dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, TimeForNewVisit.Hour, TimeForNewVisit.Minute, 0);
                        }
                    }
                    int localMedicalWorkerId = Convert.ToInt32(MedicalWorkerIdHolderHack.Text);
                    var localMedicalWorker = context.MedicalWorkers.Single(x => x.Id == localMedicalWorkerId);
                    int localSpecializationId = Convert.ToInt32(SpecializationIdHolderHack.Text);
                    var localSpecialization = context.Specializations.Single(x => x.Id == localSpecializationId);

                    if (context.Visits.Any(x => x.MedicalWorker.Id == localMedicalWorkerId && x.TimeStart == TimeForNewVisit))
                    {
                        throw new Exception("Nie mogą odbywać się 2 wizyty w tym samym czasie. Proszę wybrać inną godzinę.");
                    }

                    DbVisit visit = new DbVisit()
                    {
                        MedicalWorker = localMedicalWorker,
                        Specialization = localSpecialization,
                        OptionalDescription = "",
                        Patient = context.Patients.Single(x => x.Id == DataHolderForMainWindow.PatientId),
                        TimeStart = TimeForNewVisit
                    };
                    context.Visits.AddOrUpdate(x => x.Id, visit);
                    context.SaveChanges();

                    context.MedicalWorkers.Single(x => x.Id == localMedicalWorkerId).Visits.Add(visit);
                    context.Patients.Single(x => x.Id == DataHolderForMainWindow.PatientId).Visits.Add(visit);
                    context.SaveChanges();

                    MessageBox.Show("Wizyta odbędzie się: " + dateTime.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
