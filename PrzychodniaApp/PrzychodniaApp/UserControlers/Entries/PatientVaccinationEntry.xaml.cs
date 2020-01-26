using PrzychodniaApp.DataBaseStuff;
using PrzychodniaApp.Enums;
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
    /// Interaction logic for PatientVaccinationEntry.xaml
    /// </summary>
    public partial class PatientVaccinationEntry : UserControl, INotifyPropertyChanged
    {
        public PatientVaccinationEntry()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ChangeStatusButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DataBaseContext())
            {
                var vaccination = context.Vaccinations.Single(x => x.Id == Convert.ToInt32(IdHolderHack.Text));
                vaccination.VaccineStatus = GetVaccineStatus(StatusComboBox.SelectedIndex);
                context.SaveChanges();
            }
        }

        private VaccineStatus GetVaccineStatus(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    return VaccineStatus.Optional;
                case 1:
                    return VaccineStatus.ObligatoryByDate;
                case 2:
                    return VaccineStatus.ObligatoryAlreadyVaccined;
                default:
                    return VaccineStatus.Optional;
            }
        }
    }
}
