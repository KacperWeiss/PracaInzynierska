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

namespace PrzychodniaApp.UserControlers.Tabs
{
    /// <summary>
    /// Interaction logic for CurrentVisitTab.xaml
    /// </summary>
    public partial class CurrentVisitTab : UserControl, INotifyPropertyChanged
    {
        private VisitDataForCurrentVisitTab currentVisit;
        public VisitDataForCurrentVisitTab CurrentVisit
        {
            get
            {
                return currentVisit;
            }
            set
            {
                if (currentVisit != value)
                {
                    currentVisit = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<SicnessForCurrentVisitTab> pastSicknessList;
        public List<SicnessForCurrentVisitTab> PastSicknessList
        {
            get
            {
                return pastSicknessList;
            }
            set
            {
                if (pastSicknessList != value)
                {
                    pastSicknessList = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<DbPrescribedMedications> prescriptionList;
        public List<DbPrescribedMedications> PrescriptionList
        {
            get
            {
                return prescriptionList;
            }
            set
            {
                if(prescriptionList != value)
                {
                    prescriptionList = value;
                    OnPropertyChanged();
                }
            }
        }

        public CurrentVisitTab()
        {
            InitializeComponent();
            DataContext = this;
            CurrentVisit = VisitDataForCurrentVisitTab.GetRepresentation();
            PastSicknessList = SicnessForCurrentVisitTab.GetRepresentation(CurrentVisit.TreatmentHistoryId);
            PrescriptionList = new List<DbPrescribedMedications>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SicknessScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta / 2);
            e.Handled = true;
        }

        private void PrescriptionScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta / 2);
            e.Handled = true;
        }

        private void AddMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            PrescriptionList = new List<DbPrescribedMedications>(PrescriptionList)
            {
                new DbPrescribedMedications()
                {
                    MedicinesName = MedicinesNameTextBox.Text,
                    Dosage = MedicinesDosageTextBox.Text
                }
            };
        }

        private void RemoveMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PrescriptionListView.Items.IsEmpty)
                {
                    throw new Exception("Nie można usunąć leku - Lista leków jest już pusta!");
                }
                PrescriptionList.RemoveAt(PrescriptionListView.SelectedIndex);
                PrescriptionList = new List<DbPrescribedMedications>(PrescriptionList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PrintPrescriptionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateDoctorsReferalButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FinishVisitButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DataBaseContext())
            {
                PrescriptionList.ForEach(x => context.PrescribedMedications.AddOrUpdate(i => i.Id, x));
                context.SaveChanges();

                var treatment = new DbTreatment()
                {
                    IllnessName = DiagnosedSicknessTextBox.Text,
                    SymptomsDescription = SymptomsDescriptionTextBox.Text,
                    Prescription = PrescriptionList,
                    Visit = context.Visits.Single(x => x.Id == DataHolderForMainWindow.ChosenVisitId)
                };
                context.Treatments.AddOrUpdate(x => x.Id, treatment);
                context.SaveChanges();

                context.TreatmentHistories.Single(x => x.Id == CurrentVisit.TreatmentHistoryId).Treatments.Add(treatment);
                context.SaveChanges();
            }
        }
    }
}
