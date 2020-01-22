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
                            patientsTab.IsPatientSelected = true;
                        }
                    }
                }
            }
        }

        private void QuickEditButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
