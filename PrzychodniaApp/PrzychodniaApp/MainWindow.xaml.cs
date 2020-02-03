using PrzychodniaApp.Enums;
using PrzychodniaApp.UserControlers;
using PrzychodniaApp.UserControlers.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PrzychodniaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool menuExtended = true;
        int menuPointerOffset = 112;
        LoginForm loginForm = new LoginForm();

        public MainWindow()
        {
            InitializeComponent();
            ShowLoginForm();
        }

        private void ShowLoginForm()
        {
            MenuGrid.Visibility = Visibility.Collapsed;
            ContentGrid.Visibility = Visibility.Collapsed;
            MiddleScreenPopupGrid.Children.Clear();
            MiddleScreenPopupGrid.Children.Add(loginForm);
        }

        public void LogInType(UserAccess accountType)
        {
            AdministratorListViewMenu.Visibility = Visibility.Collapsed;
            ReceptionListViewMenu.Visibility = Visibility.Collapsed;
            MedicalWorkerListViewMenu.Visibility = Visibility.Collapsed;
            ContentGrid.Children.Clear();

            switch (accountType)
            {
                case UserAccess.Admin:
                    AdministratorListViewMenu.Visibility = Visibility.Visible;
                    AdministratorListViewMenu.SelectedValue = 0;
                    break;
                case UserAccess.Reception:
                    ReceptionListViewMenu.Visibility = Visibility.Visible;
                    ReceptionListViewMenu.SelectedValue = 0;
                    break;
                case UserAccess.MedicalWorker:
                    MedicalWorkerListViewMenu.Visibility = Visibility.Visible;
                    MedicalWorkerListViewMenu.SelectedValue = 0;
                    break;
                default:
                    break;
            }

            MiddleScreenPopupGrid.Children.Clear();
            MenuGrid.Visibility = Visibility.Visible;
            ContentGrid.Visibility = Visibility.Visible;
            menuPointer.Margin = new Thickness(0, menuPointerOffset, 0, 0);
        }

        private void MainWindowBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (menuExtended)
            {
                MenuGrid.Width = 60;
                menuExtended = false;
            }
            else
            {
                MenuGrid.Width = 250;
                menuExtended = true;
            }
        }

        private void MenuButton_MouseEnter(object sender, MouseEventArgs e)
        {
            MenuButtonColor.Background = new SolidColorBrush(Color.FromArgb(252, 75, 75, 75));
        }

        private void MenuButton_MouseLeave(object sender, MouseEventArgs e)
        {
            MenuButtonColor.Background = null;
        }

        private void AdministratorListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = AdministratorListViewMenu.SelectedIndex;
            transitioningContentSlide.OnApplyTemplate();
            menuPointer.Margin = new Thickness(0, menuPointerOffset + (60 * index), 0, 0);

            switch (index)
            {
                case 0:
                    var userManagementTab = new UsersManagementTab();
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(userManagementTab);
                    break;
                //case 1:
                //    var addServicesTab = new AddServicesTab();
                //    ContentGrid.Children.Clear();
                //    ContentGrid.Children.Add(addServicesTab);
                //    break;
                case 2:
                    ContentGrid.Children.Clear();
                    ShowLoginForm();
                    break;
                default:
                    break;
            }
        }

        private void ReceptionListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ReceptionListViewMenu.SelectedIndex;
            transitioningContentSlide.OnApplyTemplate();
            menuPointer.Margin = new Thickness(0, menuPointerOffset + (60 * index), 0, 0);

            switch (index)
            {
                case 0:
                    var registrationTab = new PatientsTab();
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(registrationTab);
                    break;
                case 1:
                    var visitsEditorTab = new VisitsEditorTab();
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(visitsEditorTab);
                    break;
                case 2:
                    var scheduleTab = new ScheduleTab();
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(scheduleTab);
                    break;
                case 3:
                    ContentGrid.Children.Clear();
                    ShowLoginForm();
                    break;
                default:
                    break;
            }
        }

        private void MedicalWorkerListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = MedicalWorkerListViewMenu.SelectedIndex;
            transitioningContentSlide.OnApplyTemplate();
            menuPointer.Margin = new Thickness(0, menuPointerOffset + (60 * index), 0, 0);

            switch (index)
            {
                //case 0:
                //    var visitsListTab = new VisitsListTab();
                //    ContentGrid.Children.Clear();
                //    ContentGrid.Children.Add(visitsListTab);
                //    break;
                //case 1:
                //    var currentVisitListTab = new CurrentVisitListTab();
                //    ContentGrid.Children.Clear();
                //    ContentGrid.Children.Add(currentVisitListTab);
                //    break;
                case 2:
                    ContentGrid.Children.Clear();
                    ShowLoginForm();
                    break;
                default:
                    break;
            }
        }
    }
}
