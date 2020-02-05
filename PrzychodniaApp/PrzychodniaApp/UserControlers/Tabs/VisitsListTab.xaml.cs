using PrzychodniaApp.DataBaseStuff;
using PrzychodniaApp.DataBaseStuff.Models;
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
    /// Interaction logic for VisitsListTab.xaml
    /// </summary>
    public partial class VisitsListTab : UserControl, INotifyPropertyChanged
    {
        private List<FutureVisitForVisitsListTab> visitsList = FutureVisitForVisitsListTab.GetRepresentation();
        public List<FutureVisitForVisitsListTab> VisitsList
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

        public VisitsListTab()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void ResetTab()
        {
            NameTextBox.Text = "";
            SurnameTextBox.Text = "";
            DescriptionTextBox.Text = "";
            VisitsList = FutureVisitForVisitsListTab.GetRepresentation();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void VisitsScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta / 2);
            e.Handled = true;
        }

        private void ChooseButton_Click(object sender, RoutedEventArgs e)
        {
            DataHolderForMainWindow.ChosenVisitId = VisitsList[VisitsListView.SelectedIndex].VisitId;

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    MainWindow parentWindow = (window as MainWindow);

                    parentWindow.ChangeTabToCurrentVisitTab();
                }
            }
        }

        private void EditDescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var visitId = VisitsList[VisitsListView.SelectedIndex].VisitId;

                using (var context = new DataBaseContext())
                {
                    DbVisit visit = context.Visits.SingleOrDefault(x => x.Id == visitId);
                    visit.OptionalDescription = DescriptionTextBox.Text;
                    context.SaveChanges();
                }
                ResetTab();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FilterPatientsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VisitsList = FutureVisitForVisitsListTab.GetRepresentation().Where(x => x.Patient == $"{NameTextBox.Text} {SurnameTextBox.Text}").ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(NameTextBox.Text + ", " + SurnameTextBox.Text);
            }
        }
    }
}
