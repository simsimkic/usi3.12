using HealthcareInstitution.Core.Examination.Controllers;
using HealthcareInstitution.Core.Users.Controllers;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.GUI.DoctorView.AppontmentsView;
using HealthcareInstitution.GUI.ManagerView.EquipmentView;
using HealthcareInstitution.GUI.NurseView.PatientsView;
using HealthcareInstitution.GUI.PatientView.AppointmentsView;
using HealthcareInstitution.Users.Controllers;
using HealthcareInstitution.Users.Models;
using HealthcareInstitution.Utilities.Enums;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HealthcareInstitution
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserController _controller;
        public string Username { get; set; }
        public string Password { get; set; }  

        public LoginWindow()
        {
            InitializeComponent();
            UsernameTextBox.Focus();
            DataContext = this;

            Username = string.Empty;
            Password = string.Empty;

            _controller = new UserController();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            User? user = _controller.TryLogin(Username, Password);
            if (user == null)
            {
                MessageBox.Show("Incorrect username or password. Please, try again.", "Error");
                return;
            }
            switch (user.Type)
            {
                case UserType.Manager:
                    {
                        EquipmentTableWindow equipmentTableWindow = new EquipmentTableWindow();
                        equipmentTableWindow.Show();
                        break;
                    }
                case UserType.Nurse:
                    {
                        PatientsTableWindow patientsTableWindow = new PatientsTableWindow(user);
                        patientsTableWindow.Show();
                        break;
                    }
                case UserType.Patient:
                    {
                        PatientController controller = new PatientController();
                        if (controller.GetPatientByUsername(user.Username).IsBlocked)
                        {
                            MessageBox.Show("Sorry, you've been blocked.");
                        }
                        else
                        {
                            AppointmentsTableWindow appointmentTableWindow = new AppointmentsTableWindow(user);
                            appointmentTableWindow.Show();
                        }
                        break;
                    }
                case UserType.Doctor:
                    {
                        DoctorAppointmentsTableWindow doctorAppointmentsTableWindow = new DoctorAppointmentsTableWindow(user);
                        doctorAppointmentsTableWindow.Show();
                        break;
                    }
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                LoginButton_Click(sender, e);
        }
    }
}
