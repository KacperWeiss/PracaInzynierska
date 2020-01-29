using PrzychodniaApp.UserControlers.DataRepresantations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ScheduleTab.xaml
    /// </summary>
    public partial class ScheduleTab : UserControl, INotifyPropertyChanged
    {
        public DayOfWeek? dayOfWeek;
        #region DataRepresentations

        private List<SpecializationsForScheduleTab> specializationsList = SpecializationsForScheduleTab.GetRepresentation();
        public List<SpecializationsForScheduleTab> SpecializationsList
        {
            get
            {
                return specializationsList;
            }
            set
            {
                if (specializationsList != value)
                {
                    specializationsList = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<MedicalWorkerForScheduleTab> medicalWorkersList = MedicalWorkerForScheduleTab.GetRepresentation();
        public List<MedicalWorkerForScheduleTab> MedicalWorkersList
        {
            get
            {
                return medicalWorkersList;
            }
            set
            {
                if (medicalWorkersList != value)
                {
                    medicalWorkersList = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<AvailabilitiesForScheduleTab> availabilitiesList = AvailabilitiesForScheduleTab.GetRepresentation();
        public List<AvailabilitiesForScheduleTab> AvailabilitiesList
        {
            get
            {
                return availabilitiesList;
            }
            set
            {
                if (availabilitiesList != value)
                {
                    availabilitiesList = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        public ScheduleTab()
        {
            InitializeComponent();
            DataContext = this;

            SpecializationSearchComboBox.ItemsSource = SpecializationsList;
            MedicalWorkerSearchComboBox.ItemsSource = MedicalWorkersList;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ContentScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta / 2);
            e.Handled = true;
        }

        private void VisitDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dayOfWeek = VisitDatePicker.SelectedDate.Value.DayOfWeek;
            AvailabilitiesList = AvailabilitiesForScheduleTab.GetRepresentation(dayOfWeek.Value);
        }

        private void SpecializationSearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SpecializationsForScheduleTab specialization = SpecializationsList[SpecializationSearchComboBox.SelectedIndex];
            MedicalWorkersList = MedicalWorkerForScheduleTab.GetRepresentation(specialization.SpecializationId);
            MedicalWorkerSearchComboBox.ItemsSource = MedicalWorkersList;
        }

        private void MedicalWorkerSearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MedicalWorkerForScheduleTab medicalWorker = MedicalWorkersList[MedicalWorkerSearchComboBox.SelectedIndex];
            SpecializationsList = SpecializationsForScheduleTab.GetRepresentation(medicalWorker.Id);
            SpecializationSearchComboBox.ItemsSource = SpecializationsList;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            AvailabilitiesList = AvailabilitiesForScheduleTab.GetRepresentation(dayOfWeek.Value)
                .Where(x => x.MedicalWorkerId == MedicalWorkersList[MedicalWorkerSearchComboBox.SelectedIndex].Id)
                .Where(x => x.SpecializationId == SpecializationsList[SpecializationSearchComboBox.SelectedIndex].SpecializationId)
                .ToList();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SpecializationsList = SpecializationsForScheduleTab.GetRepresentation();
            MedicalWorkersList = MedicalWorkerForScheduleTab.GetRepresentation();
            AvailabilitiesList = AvailabilitiesForScheduleTab.GetRepresentation();
        }
    }
}
