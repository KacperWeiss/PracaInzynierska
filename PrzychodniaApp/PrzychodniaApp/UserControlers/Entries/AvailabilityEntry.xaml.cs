using PrzychodniaApp.DataBaseStuff;
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

namespace PrzychodniaApp.UserControlers.Entries
{
    /// <summary>
    /// Interaction logic for AvailabilityEntry.xaml
    /// </summary>
    public partial class AvailabilityEntry : UserControl, INotifyPropertyChanged
    {
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
            VisitTimeStartSelectionTextBox.Text = "07:45";
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            VisitTimeStartSelectionTextBox.Text = "07:30";
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

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
