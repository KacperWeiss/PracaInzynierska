using PrzychodniaApp.DataBaseStuff;
using PrzychodniaApp.DataBaseStuff.Models;
using PrzychodniaApp.Enums;
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
    /// Interaction logic for PatientsTab.xaml
    /// </summary>
    public partial class PatientsTab : UserControl, INotifyPropertyChanged
    {
        public bool IsPatientSelected = false;
        Button previousButton;

        #region Properties with DataRepresentations

        private List<ShortPatientForPatientsTab> patientsList = ShortPatientForPatientsTab.GetRepresentation();
        public List<ShortPatientForPatientsTab> PatientsList
        {
            get
            {
                return patientsList;
            }
            set
            {
                if (patientsList != value)
                {
                    patientsList = value;
                    OnPropertyChanged();
                }
            }
        }

        private LongPatientForPatientsTab chosenPatient = new LongPatientForPatientsTab();
        public LongPatientForPatientsTab ChosenPatient
        {
            get
            {
                return chosenPatient;
            }
            set
            {
                if (chosenPatient != value)
                {
                    chosenPatient = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<VaccinationForPatientsTab> vaccinationsList = new List<VaccinationForPatientsTab>();
        public List<VaccinationForPatientsTab> VaccinationsList
        {
            get
            {
                return vaccinationsList;
            }
            set
            {
                if (vaccinationsList != value)
                {
                    vaccinationsList = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<VisitForVisitsEditorTab> visitsList = new List<VisitForVisitsEditorTab>();
        public List<VisitForVisitsEditorTab> VisitsList
        {
            get
            {
                return visitsList;
            }
            set
            {
                if (visitsList != value)
                {
                    visitsList = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        public PatientsTab()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void ResetPatientsTab()
        {
            FirstNameSearchTextBox.Text = "";
            LastNameSearchTextBox.Text = "";
            IsPatientSelected = false;
            PatientsList = ShortPatientForPatientsTab.GetRepresentation();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FirstTab_Click(object sender, RoutedEventArgs e)
        {
            Tab_Click(sender, e);
            TabPointer.Margin = new Thickness(10, 0, 0, 0);
        }

        private void PatientTab_Click(object sender, RoutedEventArgs e)
        {
            Tab_Click(sender, e);
            EmailContactChoiceComboBox.SelectedIndex = (int)ChosenPatient.EmailContact;
        }

        private void Tab_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentButton = (Button)e.Source;
                int index = int.Parse(currentButton.Uid);

                if (!IsPatientSelected && !(index == 1 || index == 0))
                {
                    throw new Exception("Patient must be selected first!");
                }

                TabPointer.Margin = new Thickness(360 + ((index - 1) * 200), 0, 0, 0);
                currentButton.Background = Brushes.White;

                if (previousButton != null && previousButton != currentButton)
                {
                    previousButton.Background = null;
                }
                previousButton = (Button)e.Source;

                switch (index)
                {
                    case 0:
                        PatientsWithSearchGrid.Visibility = Visibility.Visible;
                        NewPatientGrid.Visibility = Visibility.Collapsed;
                        PatientView.Visibility = Visibility.Collapsed;
                        VaccinationListView.Visibility = Visibility.Collapsed;
                        break;

                    case 1:
                        PatientsWithSearchGrid.Visibility = Visibility.Collapsed;
                        NewPatientGrid.Visibility = Visibility.Visible;
                        PatientView.Visibility = Visibility.Collapsed;
                        VaccinationListView.Visibility = Visibility.Collapsed;
                        break;

                    case 2:
                        PatientsWithSearchGrid.Visibility = Visibility.Collapsed;
                        NewPatientGrid.Visibility = Visibility.Collapsed;
                        PatientView.Visibility = Visibility.Visible;
                        VaccinationListView.Visibility = Visibility.Collapsed;
                        break;

                    case 3:
                        PatientsWithSearchGrid.Visibility = Visibility.Collapsed;
                        NewPatientGrid.Visibility = Visibility.Collapsed;
                        PatientView.Visibility = Visibility.Collapsed;
                        VaccinationListView.Visibility = Visibility.Visible;
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ContentScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta / 2);
            e.Handled = true;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PatientsList = ShortPatientForPatientsTab.GetRepresentation().Where(x => x.FullName == $"{FirstNameSearchTextBox.Text} {LastNameSearchTextBox.Text}").ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(FirstNameSearchTextBox.Text + ", " + LastNameSearchTextBox.Text);
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetPatientsTab();
        }

        private void FaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (FaceIcon.Kind == MaterialDesignThemes.Wpf.PackIconKind.Face)
            {
                FaceIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.FaceWoman;
            }
            else
            {
                FaceIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Face;
            }
        }

        private void GenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GenderComboBox.SelectedIndex == 0)
            {
                FaceIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Face;
            }
            else
            {
                FaceIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.FaceWoman;
            }
        }

        private void AddPatientButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DataBaseContext())
            {
                var treatmentHistory = new DbTreatmentHistory()
                {
                    PastVaccinations = new List<DbVaccination>(),
                    RequiredVaccinations = new List<DbVaccination>(),
                    Treatments = new List<DbTreatment>()
                };
                context.TreatmentHistories.Add(treatmentHistory);
                context.SaveChanges();

                var newPatient = new DbPatient()
                {
                    FirstName = NameTextBox.Text,
                    LastName = SurnameTextBox.Text,
                    EmailAdress = EmailTextBox.Text,
                    DateOfBirth = BirthdayDatePicker.SelectedDate.Value,
                    PatientsPronounce = GenderComboBox.SelectedIndex == 0 ? Pronouns.MR : Pronouns.MRS,
                    EmailContact = GetEmailContact(AdressComboBox.SelectedIndex),
                    TreatmentHistory = treatmentHistory,
                    Visits = new List<DbVisit>()
                };
                context.Patients.Add(newPatient);
                context.SaveChanges();
            }
        }

        private EmailContact GetEmailContact(int index)
        {
            switch (index)
            {
                case 0:
                    return EmailContact.Full;
                case 1:
                    return EmailContact.ConfirmationsAndReminders;
                case 2:
                    return EmailContact.OnlyForConfirmations;
                case 3:
                    return EmailContact.No;
                default:
                    return EmailContact.No;
            }
        }

        private void SelectedPatientFaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPatientFaceIcon.Kind == MaterialDesignThemes.Wpf.PackIconKind.Face)
            {
                SelectedPatientFaceIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.FaceWoman;
            }
            else
            {
                SelectedPatientFaceIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Face;
            }
        }

        private void DateOfBirthDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateOfBirthTextBlock.Text = DateOfBirthDatePicker.SelectedDate.Value.ToLongDateString();
        }

        private void RegisterVisitButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    MainWindow parentWindow = (window as MainWindow);

                    parentWindow.ChangeTabToScheduleTab();
                }
            }
        }

        private void QuickEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (SaveEditButton.Visibility == Visibility.Collapsed)
            {
                SaveEditButton.Visibility = Visibility.Visible;
                SelectedPatientFullNameTextBox.IsReadOnly = false;
                SelectedPatientEmailTextBox.IsReadOnly = false;
            }
            else
            {
                SaveEditButton.Visibility = Visibility.Collapsed;
                SelectedPatientFullNameTextBox.IsReadOnly = true;
                SelectedPatientEmailTextBox.IsReadOnly = true;
            }
        }

        private void SaveEditButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DataBaseContext())
            {
                var names = SelectedPatientFullNameTextBox.Text.Split(' ');
                if (names.Length != 2)
                {
                    throw new Exception("Both names must be provided seperated by single space!");
                }
                var currentPatient = context.Patients.SingleOrDefault(x => x.Id == ChosenPatient.PatientId);
                currentPatient.FirstName = names[0];
                currentPatient.LastName = names[1];
                currentPatient.EmailAdress = EmailTextBox.Text;
                currentPatient.DateOfBirth = DateOfBirthDatePicker.SelectedDate.HasValue ? DateOfBirthDatePicker.SelectedDate.Value : currentPatient.DateOfBirth;
                currentPatient.PatientsPronounce = SelectedPatientFaceIcon.Kind == MaterialDesignThemes.Wpf.PackIconKind.Face ? Pronouns.MR : Pronouns.MRS;
                currentPatient.EmailContact = GetEmailContact(EmailContactChoiceComboBox.SelectedIndex);
                context.SaveChanges();
            }
        }

        private void VaccinationDoneDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            VaccinationDoneDateTextBlock.Content = VaccinationDoneDateDatePicker.SelectedDate.HasValue ? VaccinationDoneDateDatePicker.SelectedDate.Value.ToLongDateString() : "-";
        }

        private void VaccinationObligatoryByDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            VaccinationObligatoryByDateTextBlock.Content = VaccinationObligatoryByDateDatePicker.SelectedDate.HasValue ? VaccinationObligatoryByDateDatePicker.SelectedDate.Value.ToLongDateString() : "-";
        }

        private void AddVacinationButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DataBaseContext())
            {
                var vaccination = new DbVaccination()
                {
                    VaccinesName = VaccineNameTextBox.Text,
                    VaccineStatus = StatusComboBox.SelectedIndex == 0 ? VaccineStatus.Optional : VaccineStatus.ObligatoryByDate
                };
                if (VaccinationDoneDateDatePicker.SelectedDate.HasValue)
                {
                    vaccination.VaccinationDate = VaccinationDoneDateDatePicker.SelectedDate.Value;
                }
                if (VaccinationObligatoryByDateDatePicker.SelectedDate.HasValue)
                {
                    vaccination.ObligatoryBy = VaccinationObligatoryByDateDatePicker.SelectedDate.Value;
                }

                context.Vaccinations.Add(vaccination);
                context.SaveChanges();

                context.TreatmentHistories.Single(x => x.Id == ChosenPatient.TreatmentHistoryId).RequiredVaccinations.Add(vaccination);
                context.SaveChanges();
            }
        }
    }
}
