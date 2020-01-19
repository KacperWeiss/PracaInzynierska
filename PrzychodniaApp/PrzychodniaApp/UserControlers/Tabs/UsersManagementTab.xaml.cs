using PrzychodniaApp.DataBaseStuff;
using PrzychodniaApp.DataBaseStuff.Models;
using PrzychodniaApp.Enums;
using PrzychodniaApp.Logics;
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
    /// Interaction logic for UsersManagementTab.xaml
    /// </summary>
    public partial class UsersManagementTab : UserControl, INotifyPropertyChanged
    {
        private List<UserForUsersManagementTab> userList = UserForUsersManagementTab.GetRepresentation();
        public List<UserForUsersManagementTab> UserList
        {
            get
            {
                return userList;
            }
            set
            {
                if (userList != value)
                {
                    userList = value;
                    OnPropertyChanged();
                }
            }
        }

        public UsersManagementTab()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UsersScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta / 2);
            e.Handled = true;
        }

        private void ResetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var userId = UserList[UsersListView.SelectedIndex].UserId;
        }

        private void EditAccessLevelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userId = UserList[UsersListView.SelectedIndex].UserId;
                UserAccess newUserAccess = UserAccess.Admin;

                var index = AccessLevelChoiceComboBox.SelectedIndex;
                if (index != -1)
                {
                    switch (index)
                    {
                        case 0:
                            newUserAccess = UserAccess.Admin;
                            break;
                        case 1:
                            newUserAccess = UserAccess.Reception;
                            break;
                        case 2:
                            newUserAccess = UserAccess.MedicalWorker;
                            break;
                        default:
                            break;
                    }

                    using (var context = new DataBaseContext())
                    {
                        DbUser user = context.Users.SingleOrDefault(x => x.Id == userId);
                        if (user.UserAccess == UserAccess.Admin && context.Users.Count(x => x.UserAccess == UserAccess.Admin) == 1)
                        {
                            throw new Exception("You can't change access level for the only user with Admin status!");
                        }

                        user.UserAccess = newUserAccess;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userId = UserList[UsersListView.SelectedIndex].UserId;
                using (var context = new DataBaseContext())
                {
                    DbUser user = context.Users.SingleOrDefault(x => x.Id == userId);
                    if (user.UserAccess == UserAccess.Admin && context.Users.Count(x => x.UserAccess == UserAccess.Admin) == 1)
                    {
                        throw new Exception("You can't delete the only user with Admin status!");
                    }
                    context.Users.Remove(user);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AccessLevelChoiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AccessLevelChoiceComboBox.SelectedIndex == 2)
            {
                MedicalWorkerNameGrid.Visibility = Visibility.Visible;
            }
            else
            {
                MedicalWorkerNameGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void GeneratePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordGenerator.GetRandomPassword();
            PasswordTextBox.Text = password;
        }

        private void SaveNewUserButton_Click(object sender, RoutedEventArgs e)
        {
            UserAccess newUserAccess = UserAccess.Admin;

            var index = AccessLevelChoiceComboBox.SelectedIndex;
            if (index != -1)
            {
                switch (index)
                {
                    case 0:
                        newUserAccess = UserAccess.Admin;
                        break;
                    case 1:
                        newUserAccess = UserAccess.Reception;
                        break;
                    case 2:
                        newUserAccess = UserAccess.MedicalWorker;
                        break;
                    default:
                        break;
                }

                using (var context = new DataBaseContext())
                {
                    if (newUserAccess == UserAccess.MedicalWorker)
                    {
                        var NewMedicalWorker = new DbMedicalWorker()
                        {
                            FirstName = FirstNameTextBox.Text,
                            LastName = LastNameTextBox.Text,
                            Availabilities = new List<DbAvailability>(),
                            Specializations = new List<DbSpecialization>(),
                            Visits = new List<DbVisit>()
                        };
                        context.MedicalWorkers.Add(NewMedicalWorker);
                        context.SaveChanges();

                        context.Users.Add(new DbUser()
                        {
                            Login = LoginTextBox.Text,
                            Password = PasswordTextBox.Text,
                            UserAccess = newUserAccess,
                            MedicalWorker = NewMedicalWorker
                        });
                        context.SaveChanges();
                    }
                    else
                    {
                        context.Users.Add(new DbUser()
                        {
                            Login = LoginTextBox.Text,
                            Password = PasswordTextBox.Text,
                            UserAccess = newUserAccess
                        });
                        context.SaveChanges();
                    }
                }
            }
        }

        private void CopyLoginAndPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("Login: " + LoginTextBox.Text + "\n Password: " + PasswordTextBox.Text);
        }
    }
}
