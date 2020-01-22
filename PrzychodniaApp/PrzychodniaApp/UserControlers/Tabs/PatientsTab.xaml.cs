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

        private void Tab_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsPatientSelected)
                {
                    throw new Exception("Patient must be selected first!");
                }

                var currentButton = (Button)e.Source;
                int index = int.Parse(currentButton.Uid);
                TabPointer.Margin = new Thickness(310 + ((index - 1) * 200), 0, 0, 0);
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
                        NewPatientView.Visibility = Visibility.Collapsed;
                        PatientView.Visibility = Visibility.Collapsed;
                        VaccinationListView.Visibility = Visibility.Collapsed;
                        UpcomingVisitsListView.Visibility = Visibility.Collapsed;
                        break;

                    case 1:
                        PatientsWithSearchGrid.Visibility = Visibility.Collapsed;
                        NewPatientView.Visibility = Visibility.Visible;
                        PatientView.Visibility = Visibility.Collapsed;
                        VaccinationListView.Visibility = Visibility.Collapsed;
                        UpcomingVisitsListView.Visibility = Visibility.Collapsed;
                        break;

                    case 2:
                        PatientsWithSearchGrid.Visibility = Visibility.Collapsed;
                        NewPatientView.Visibility = Visibility.Collapsed;
                        PatientView.Visibility = Visibility.Visible;
                        VaccinationListView.Visibility = Visibility.Collapsed;
                        UpcomingVisitsListView.Visibility = Visibility.Collapsed;
                        break;

                    case 3:
                        PatientsWithSearchGrid.Visibility = Visibility.Collapsed;
                        NewPatientView.Visibility = Visibility.Collapsed;
                        PatientView.Visibility = Visibility.Collapsed;
                        VaccinationListView.Visibility = Visibility.Visible;
                        UpcomingVisitsListView.Visibility = Visibility.Collapsed;
                        break;

                    case 4:
                        PatientsWithSearchGrid.Visibility = Visibility.Collapsed;
                        NewPatientView.Visibility = Visibility.Collapsed;
                        PatientView.Visibility = Visibility.Collapsed;
                        VaccinationListView.Visibility = Visibility.Collapsed;
                        UpcomingVisitsListView.Visibility = Visibility.Visible;
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
    }
}
