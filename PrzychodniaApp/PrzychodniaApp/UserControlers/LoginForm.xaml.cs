﻿using PrzychodniaApp.DataBaseStuff.Models;
using PrzychodniaApp.Enums;
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
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : UserControl
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    MainWindow parentWindow = (window as MainWindow);
                    // Obejście na czas testów
                    switch (LoginTextBox.Text)
                    {
                        case "a":
                            parentWindow.LogInType(UserAccess.Admin);
                            return;
                        case "r":
                            parentWindow.LogInType(UserAccess.Reception);
                            return;
                        case "mw":
                            parentWindow.LogInType(UserAccess.MedicalWorker);
                            return;

                    }

                    if (!String.IsNullOrEmpty(LoginTextBox.Text) && !String.IsNullOrEmpty(PasswordBox.Password))
                    {
                        try
                        {
                            DataHolderForMainWindow.User = LoginManagement.LoginAs(LoginTextBox.Text, PasswordBox.Password);

                            parentWindow.LogInType(DataHolderForMainWindow.User.UserAccess);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Aby się zalogować należy podać login i hasło");
                    }
                }
            }
        }
    }
}
