using PrzychodniaApp.DataBaseStuff;
using PrzychodniaApp.Logics;
using PrzychodniaApp.UserControlers.DataRepresantations;
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

namespace PrzychodniaApp.UserControlers
{
    /// <summary>
    /// Interaction logic for SettingsForm.xaml
    /// </summary>
    public partial class SettingsForm : UserControl
    {
        bool isComboBoxLoaded = false;
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!DataHolderForMainWindow.IsUserLogedIn)
                {
                    throw new Exception("Żaden użytkownik nie jest zalogowany.");
                }
                if (NewPasswordBox.Password != ConfirmPasswordBox.Password)
                {
                    throw new Exception("Hasła nie są identyczne!");
                }
                using (var context = new DataBaseContext())
                {
                    if (context.Users.Single(x => x.Id == DataHolderForMainWindow.User.Id).Password == CurrentPasswordBox.Password)
                    {
                        throw new Exception("Wpisano błędne hasło! Aby zmienić hasło należy wpisać poprawne hasło, a dopiero wtedy wpisać nowe.");
                    }
                    context.Users.Single(x => x.Id == DataHolderForMainWindow.User.Id).Password = NewPasswordBox.Password;
                    context.SaveChanges();

                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            MainWindow parentWindow = (window as MainWindow);
                            parentWindow.LogInType(DataHolderForMainWindow.User.UserAccess);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CloseSettings()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    if (DataHolderForMainWindow.IsUserLogedIn)
                    {
                        MainWindow parentWindow = (window as MainWindow);
                        parentWindow.LogInType(DataHolderForMainWindow.User.UserAccess);
                    }
                    else
                    {
                        MainWindow parentWindow = (window as MainWindow);
                        parentWindow.MiddleScreenPopupGrid.Children.Clear();
                        parentWindow.MiddleScreenPopupGrid.Children.Add(parentWindow.loginForm);
                    }
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseSettings();
        }

        private void ServerComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isComboBoxLoaded)
            {
                isComboBoxLoaded = !isComboBoxLoaded;
                ServerComboBox.Items.Add(@".\BAZYDANYCHMSSQL");
                ServerComboBox.Items.Add(String.Format(@"{0}\BAZYDANYCHMSSQL", Environment.MachineName));
                ServerComboBox.Items.Add(String.Format(@"{0}\SQLDEVELOPER", Environment.MachineName));
                ServerComboBox.Items.Add(String.Format(@"{0}\SQLEXPRESS", Environment.MachineName));
                ServerComboBox.Items.Add(new TextBox().Text = "Inne");
            }
        }

        private void ServerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServerComboBox.SelectedIndex == 4)
            {
                CustomServerLabel.Visibility = Visibility.Visible;
                CustomServerTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                CustomServerLabel.Visibility = Visibility.Collapsed;
                CustomServerTextBox.Visibility = Visibility.Collapsed;
            }
        }

        private void CheckConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            string serverString = (CustomServerTextBox.Visibility == Visibility.Visible) ? CustomServerTextBox.Text : (string)ServerComboBox.SelectedItem;
            string connectionString = string.Format("data source={0};initial catalog=PrzychodniaApp.Models.MainContext;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework", serverString);

            try
            {
                SqlConnectionHelper sqlHelper = new SqlConnectionHelper(connectionString);
                if (sqlHelper.IsConnection)
                {
                    MessageBox.Show("Połączenie jest poprawne!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            string serverString = (CustomServerTextBox.Visibility == Visibility.Visible) ? CustomServerTextBox.Text : (string)ServerComboBox.SelectedItem;
            string connectionString = string.Format("data source={0};initial catalog=PrzychodniaApp.Models.MainContext;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework", serverString);

            try
            {
                SqlConnectionHelper sqlHelper = new SqlConnectionHelper(connectionString);
                if (sqlHelper.IsConnection)
                {
                    AppSettings settings = new AppSettings();
                    settings.SaveConnectionString("MainContext", connectionString);
                    MessageBox.Show("Twoje połączenie zostało poprawnie skonfigurowane!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
