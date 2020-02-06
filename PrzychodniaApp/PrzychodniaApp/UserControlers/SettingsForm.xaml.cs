using PrzychodniaApp.DataBaseStuff;
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
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
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
}
