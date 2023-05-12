using HealthcareInstitution.Core.Examination.Controllers;
using HealthcareInstitution.Core.Users.Controllers;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.GUI.DoctorView.PatientsView;
using HealthcareInstitution.GUI.PatientView.AppointmentsView;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HealthcareInstitution.GUI.DoctorView.AppontmentsView
{
    /// <summary>
    /// Interaction logic for AppointmentsTableWindow.xaml
    /// </summary>
    public partial class DoctorAppointmentsTableWindow : Window, IObserver
    {
        private AppointmentController _appointmentController;
        private AppointmentHistoryController _appointmentHistoryController;
        private DoctorController _doctorController;
        private User _user;
        public ObservableCollection<DoctorAppointmentsTableItem> Appointments { get; set; }
        public DoctorAppointmentsTableItem SelectedAppointment { get; set; }

        public DoctorAppointmentsTableWindow(User user)
        {
            InitializeComponent();
            DataContext = this;

            _appointmentController = new AppointmentController();
            _appointmentHistoryController = new AppointmentHistoryController();
            _appointmentController.Subscribe(this);
            _doctorController = new DoctorController();
            _user = user;

            Appointments = new ObservableCollection<DoctorAppointmentsTableItem>(_doctorController.GetDoctorAppointmentsForDoctor(user.Username));
        }

        private void StartAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment == null)
            {
                MessageBox.Show("Choose appointment to start.");
            }
            else if (SelectedAppointment.Appointment.GetDateTime().AddMinutes(15) < DateTime.Now)
            {
                MessageBox.Show("Appointment schedule expired.");
            }
            else
            {
                AppointmentExecutionWindow appointmentExecutionWindow = new AppointmentExecutionWindow(this, _user, _appointmentController, _appointmentHistoryController, SelectedAppointment);
                appointmentExecutionWindow.Show();
            }
        }

        private void DoctorPatientsButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorPatientsWindow doctorPatientsWindow = new DoctorPatientsWindow(this, _user);
            doctorPatientsWindow.Show();
        }

        private void ShowCreateAppointmentWindow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowUpdateAppointmentWindow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteAppointmentButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Update()
        {
            UpdateAppointmentsList();
        }

        private void UpdateAppointmentsList()
        {
            Appointments.Clear();
            foreach (DoctorAppointmentsTableItem appointment in _doctorController.GetDoctorAppointmentsForDoctor(_user.Username))
            {
                Appointments.Add(appointment);
            }
        }
    }
}
