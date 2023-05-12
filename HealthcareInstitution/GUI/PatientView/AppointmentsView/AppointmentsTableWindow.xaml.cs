using HealthcareInstitution.Core.Examination.Controllers;
using HealthcareInstitution.Core.Examination.Models;
using HealthcareInstitution.Core.Users.Controllers;
using HealthcareInstitution.Core.Users.Models;
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
using System.Windows.Shapes;

namespace HealthcareInstitution.GUI.PatientView.AppointmentsView
{
    /// <summary>
    /// Interaction logic for AppointmentTableWindow.xaml
    /// </summary>
    public partial class AppointmentsTableWindow : Window, IObserver
    {
        private AppointmentController _appointmentController;
        private AppointmentHistoryController _appointmentHistoryController;
        private PatientController _patientController;
        private User _user;

        public ObservableCollection<AppointmentsTableItem> Appointments { get; set; }
        public AppointmentsTableItem? SelectedAppointment { get; set; }

        public AppointmentsTableWindow(User user)
        {
            InitializeComponent();
            DataContext = this;

            _appointmentController = new AppointmentController();
            _appointmentHistoryController = new AppointmentHistoryController();
            _appointmentController.Subscribe(this);
            _patientController = new PatientController();
            _user = user;

            Appointments = new ObservableCollection<AppointmentsTableItem>(_patientController.GetAppointmentsForPatient(user.Username));

            SelectedAppointment = null;
        }

        private void ShowCreateAppointmentWindow_Click(object sender, RoutedEventArgs e)
        {
            CreateAppointmentWindow createAppointmentWindow = new CreateAppointmentWindow(this, _user, _appointmentController, _appointmentHistoryController);
            createAppointmentWindow.Show();
        }

        private void ShowUpdateAppointmentWindow_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment == null)
            { 
                MessageBox.Show("Choose appointment to update.");
            }
            else if (SelectedAppointment.Appointment.GetDateTime() < DateTime.Now.AddDays(1))
            {
                MessageBox.Show("It's too late to change appointment.");
            }
            else
            {
                UpdateAppointmentWindow updateAppointmentWindow = new UpdateAppointmentWindow(this, _user, _appointmentController, _appointmentHistoryController, SelectedAppointment);
                updateAppointmentWindow.Show();
            }
        }

        private void DeleteAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment == null)
            {
                MessageBox.Show("Choose appointment to delete.");
            }
            else if (SelectedAppointment.Appointment.GetDateTime() < DateTime.Now.AddDays(1))
            {
                MessageBox.Show("It's too late to change appointment.");
            }
            else
            {
                MessageBoxResult result = ConfirmAppointmentDeletion();

                if (result == MessageBoxResult.Yes)
                {
                    int appointmentId = SelectedAppointment.Appointment.Id;
                    _appointmentController.Delete(_appointmentController.GetAllAppointments().Find(a => a.Id == SelectedAppointment.Appointment.Id));
                    _appointmentHistoryController.Create(new AppointmentHistory(-1, appointmentId, _user.Username, Utilities.Enums.AppointmentStatus.Deleted, DateTime.Now));
                    if (_appointmentHistoryController.CheckToBlockPatient(_user.Username))
                    {
                        MessageBox.Show("You have been blocked.");
                        Close();
                    }
                }
            }
        }

        private MessageBoxResult ConfirmAppointmentDeletion()
        {
            string sMessageBoxText = $"Are you sure that you want to delete this appointment?";
            string sCaption = "Confirm";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            return result;
        }

        private void UpdateAppointmentsList()
        {
            Appointments.Clear();
            foreach (AppointmentsTableItem appointment in _patientController.GetAppointmentsForPatient(_user.Username))
            {
                Appointments.Add(appointment);
            }
        }

        public void Update()
        {
            UpdateAppointmentsList();
        }

        private void RecommendTimeWindow_Click(object sender, RoutedEventArgs e)
        {
            RecommendTimeWindow recommendTimeWindow = new RecommendTimeWindow(this, _user, _appointmentController, _appointmentHistoryController);
            recommendTimeWindow.Show();
        }

        private void ExaminationHistoryWindow_Click(object sender, RoutedEventArgs e)
        {
            ExaminationHistoryWindow examinationHistoryWindow = new ExaminationHistoryWindow(this, _user);
            examinationHistoryWindow.Show();
        }
    }
}
