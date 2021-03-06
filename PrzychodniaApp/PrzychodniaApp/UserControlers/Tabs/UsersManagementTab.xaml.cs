﻿using PrzychodniaApp.DataBaseStuff;
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

        public String Instruction { get; } = 
            "W celu utworzenia konta użytkownika należy:\n" +
            "1. Wybrać jego funkcje z dostępnych opcji.\n" +
            "2. Wypełnić odpowiednie pola, opcjonalnie można wygenerować\n" +
            "losowe hasło.\n" +
            "3.Zakończyć proces klikając przycisk \"Dodaj\"";

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
            try
            {
                var userId = UserList[UsersListView.SelectedIndex].UserId;
                if (userId == DataHolderForMainWindow.User.Id)
                {
                    throw new Exception("You can't reset password for your own, use settings instead and choose your own password yourself!");
                }

                using (var context = new DataBaseContext())
                {
                    context.Users.Single(x => x.Id == userId).Password = PasswordTextBox.Password;
                    Clipboard.SetText("Login: " + context.Users.Single(x => x.Id == userId).Login + "\n Password: " + PasswordTextBox.Password);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditAccessLevelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AccessLevelChoiceComboBox.SelectedIndex == -1)
                {
                    throw new Exception("You need to select access level!");
                }

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
                            newUserAccess = UserAccess.Recepcjonista;
                            break;
                        case 2:
                            newUserAccess = UserAccess.PracownikMedyczny;
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
                    UserList = UserForUsersManagementTab.GetRepresentation();
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
                    context.SaveChanges();
                }
                UserList = UserForUsersManagementTab.GetRepresentation();
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
            PasswordTextBox.Password = password;
        }

        private void SaveNewUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LoginTextBox.Text == "" || PasswordTextBox.Password == "")
                {
                    throw new Exception("Name and surname must be provided!");
                }
                if (UserList.Exists(x => x.Username == LoginTextBox.Text))
                {
                    throw new Exception("User already exists!");
                }
                if (AccessLevelChoiceComboBox.SelectedIndex == -1)
                {
                    throw new Exception("You need to select access level!");
                }

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
                            newUserAccess = UserAccess.Recepcjonista;
                            break;
                        case 2:
                            newUserAccess = UserAccess.PracownikMedyczny;
                            break;
                        default:
                            break;
                    }

                    using (var context = new DataBaseContext())
                    {
                        if (newUserAccess == UserAccess.PracownikMedyczny)
                        {
                            if (FirstNameTextBox.Text == "" || LastNameTextBox.Text == "")
                            {
                                throw new Exception("Name and surname must be provided!");
                            }

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
                                Password = PasswordTextBox.Password,
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
                                Password = PasswordTextBox.Password,
                                UserAccess = newUserAccess
                            });
                            context.SaveChanges();
                        }
                    }
                    UserList = UserForUsersManagementTab.GetRepresentation();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void CopyLoginAndPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("Login: " + LoginTextBox.Text + "\n Password: " + PasswordTextBox.Password);
        }
    }
}
