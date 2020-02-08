using PrzychodniaApp.Enums;
using PrzychodniaApp.UserControlers;
using PrzychodniaApp.UserControlers.DataRepresantations;
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
        public LoginForm loginForm = new LoginForm();
        SettingsForm settingsForm = new SettingsForm();
        bool areSettingsOpen = false;

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
                case 1:
                    DataHolderForMainWindow.IsUserLogedIn = false;
                    ContentGrid.Children.Clear();
                    ShowLoginForm();
                    break;
                default:
                    break;
            }
        }

        public void ChangeTabToScheduleTab()
        {
            ReceptionListViewMenu.SelectedIndex = 2;
            transitioningContentSlide.OnApplyTemplate();
            menuPointer.Margin = new Thickness(0, menuPointerOffset + 120, 0, 0);

            var scheduleTab = new ScheduleTab();
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(scheduleTab);
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
                    DataHolderForMainWindow.IsUserLogedIn = false;
                    ContentGrid.Children.Clear();
                    ShowLoginForm();
                    break;
                default:
                    break;
            }
        }

        public void ChangeTabToCurrentVisitTab()
        {
            MedicalWorkerListViewMenu.SelectedIndex = 1;
            transitioningContentSlide.OnApplyTemplate();
            menuPointer.Margin = new Thickness(0, menuPointerOffset + 60, 0, 0);

            var currentVisitTab = new CurrentVisitTab();
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(currentVisitTab);
        }

        private void MedicalWorkerListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int index = MedicalWorkerListViewMenu.SelectedIndex;
                if (index == 1 && DataHolderForMainWindow.ChosenVisitId == -1)
                {
                    MedicalWorkerListViewMenu.SelectedIndex = 0;
                    throw new Exception("Musisz najpierw wybrać wizytę!");
                }
                transitioningContentSlide.OnApplyTemplate();
                menuPointer.Margin = new Thickness(0, menuPointerOffset + (60 * index), 0, 0);

                switch (index)
                {
                    case 0:
                        var visitsListTab = new VisitsListTab();
                        ContentGrid.Children.Clear();
                        ContentGrid.Children.Add(visitsListTab);
                        break;
                    case 1:
                        var currentVisitTab = new CurrentVisitTab();
                        ContentGrid.Children.Clear();
                        ContentGrid.Children.Add(currentVisitTab);
                        break;
                    case 2:
                        DataHolderForMainWindow.IsUserLogedIn = false;
                        ContentGrid.Children.Clear();
                        ShowLoginForm();
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

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!areSettingsOpen)
            {
                areSettingsOpen = !areSettingsOpen;
                MenuGrid.Visibility = Visibility.Collapsed;
                ContentGrid.Visibility = Visibility.Collapsed;
                MiddleScreenPopupGrid.Children.Clear();
                MiddleScreenPopupGrid.Children.Add(settingsForm);
            }
            else
            {
                areSettingsOpen = !areSettingsOpen;
                settingsForm.CloseSettings();
            }
        }
    }
}
