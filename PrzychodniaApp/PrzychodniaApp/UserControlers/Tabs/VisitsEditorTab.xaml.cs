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
    /// Interaction logic for VisitsEditorTab.xaml
    /// </summary>
    public partial class VisitsEditorTab : UserControl, INotifyPropertyChanged
    {
        private List<VisitForVisitsEditorTab> visitsList = VisitForVisitsEditorTab.GetRepresentation();
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

        public VisitsEditorTab()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void ResetTab()
        {
            NameTextBox.Text = "";
            SurnameTextBox.Text = "";
            DescriptionTextBox.Text = "";
            VisitsList = VisitForVisitsEditorTab.GetRepresentation();
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

        private void RescheduleButton_Click(object sender, RoutedEventArgs e)
        {
            // TO DO
            ResetTab();
        }

        private void EditDescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var visitId = VisitsList[VisitsListView.SelectedIndex].VisitId;

                using (var context = new DataBaseContext())
                {
                    DbVisit visit = context.Visits.SingleOrDefault(x => x.Id == visitId);
                    if (visit.TimeStart < DateTime.Now)
                    {
                        throw new Exception("You cannot change visits that have already taken place!");
                    }
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

        private void DeleteVisitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var visitId = VisitsList[VisitsListView.SelectedIndex].VisitId;

                using (var context = new DataBaseContext())
                {
                    DbVisit visit = context.Visits.SingleOrDefault(x => x.Id == visitId);
                    if (visit.TimeStart < DateTime.Now)
                    {
                        throw new Exception("You cannot delete visits that have already taken place!");
                    }
                    context.Visits.Remove(visit);
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
                VisitsList = VisitForVisitsEditorTab.GetRepresentation().Where(x => x.Patient == $"{NameTextBox.Text} {SurnameTextBox.Text}").ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(NameTextBox.Text + ", " + SurnameTextBox.Text);
            }
        }
    }
}
