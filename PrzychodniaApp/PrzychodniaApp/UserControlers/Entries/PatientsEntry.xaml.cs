using PrzychodniaApp.DataBaseStuff;
using PrzychodniaApp.Enums;
using PrzychodniaApp.UserControlers.DataRepresantations;
using PrzychodniaApp.UserControlers.Tabs;
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

namespace PrzychodniaApp.UserControlers.Entries
{
    /// <summary>
    /// Interaction logic for PatientsEntry.xaml
    /// </summary>
    public partial class PatientsEntry : UserControl, INotifyPropertyChanged
    {
        public PatientsEntry()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ChosePatientButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    MainWindow parentWindow = (window as MainWindow);
                    foreach (UserControl control in parentWindow.ContentGrid.Children)
                    {
                        if (control.GetType() == typeof(PatientsTab))
                        {
                            PatientsTab patientsTab = (control as PatientsTab);

                            patientsTab.ChosenPatient = LongPatientForPatientsTab.GetRepresentation(Convert.ToInt32(IdHolderHack.Text));
                            patientsTab.VaccinationsList = VaccinationForPatientsTab.GetRepresentation(patientsTab.ChosenPatient.TreatmentHistoryId);
                            patientsTab.VisitsList = VisitForVisitsEditorTab.GetRepresentation().Where(x => x.Patient == patientsTab.ChosenPatient.FullName).ToList();
                            DataHolderForMainWindow.PatientId = Convert.ToInt32(IdHolderHack.Text);
                            patientsTab.IsPatientSelected = true;
                        }
                    }
                }
            }
        }

        private void QuickEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (SaveEditButton.Visibility == Visibility.Collapsed)
            {
                SaveEditButton.Visibility = Visibility.Visible;
                FullNameTextBox.IsReadOnly = false;
                EmailTextBox.IsReadOnly = false;
            }
            else
            {
                SaveEditButton.Visibility = Visibility.Collapsed;
                FullNameTextBox.IsReadOnly = true;
                EmailTextBox.IsReadOnly = true;
            }
        }

        private void SaveEditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var names = FullNameTextBox.Text.Split(' ');
                    if (names.Length != 2)
                    {
                        throw new Exception("Both names must be provided seperated by single space!");
                    }
                    var currentPatient = context.Patients.SingleOrDefault(x => x.Id == Convert.ToInt32(IdHolderHack.Text));
                    currentPatient.FirstName = names[0];
                    currentPatient.LastName = names[1];
                    currentPatient.EmailAdress = EmailTextBox.Text;
                    context.SaveChanges();
                }

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        MainWindow parentWindow = (window as MainWindow);
                        foreach (UserControl control in parentWindow.ContentGrid.Children)
                        {
                            if (control.GetType() == typeof(PatientsTab))
                            {
                                PatientsTab patientsTab = (control as PatientsTab);

                                patientsTab.PatientsList = ShortPatientForPatientsTab.GetRepresentation();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GenderHolderHack_Loaded(object sender, RoutedEventArgs e)
        {
            switch (GenderHolderHack.Text)
            {
                case "MR":
                    FaceIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Face;
                    break;
                case "MRS":
                    FaceIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.FaceWoman;
                    break;
            }
        }
    }
}
