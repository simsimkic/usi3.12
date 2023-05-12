using HealthcareInstitution.Core.Examination.Controllers;
using HealthcareInstitution.Core.Examination.Models;
using HealthcareInstitution.Core.Users.Controllers;
using HealthcareInstitution.Core.Users.Models;
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
    /// Interaction logic for RecommendTimeWindow.xaml
    /// </summary>
    public partial class RecommendTimeWindow : Window
    {
        private DoctorController _doctorController;
        private AppointmentController _appointmentController;
        private AppointmentHistoryController _appointmentHistoryController;
        private User _user;
        public ObservableCollection<Doctor> Doctors { get; set; }
        public Appointment Appointment { get; set; }
        public Window ParentWindow { get; set; }
        public RecommendTimeWindow(Window window, User user, AppointmentController appointmentController, AppointmentHistoryController appointmentHistoryController)
        {
            InitializeComponent();
            DataContext = this;

            _doctorController = new DoctorController();
            _appointmentHistoryController = appointmentHistoryController;
            _appointmentController = appointmentController;
            _user = user;

            Doctors = new ObservableCollection<Doctor>(_doctorController.GetAllDoctors());
            ParentWindow = window;
            Appointment = new Appointment();

            LoadComboBox();
        }

        private void LoadComboBox()
        {
            foreach (Doctor doctor in Doctors)
            {
                DoctorsComboBox.Items.Add(doctor.Name + " " + doctor.Surname);
            }
            DoctorsComboBox.SelectedValue = DoctorsComboBox.Items[0];
        }

        private void RecommendAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            string doctorUsername = Doctors[DoctorsComboBox.SelectedIndex].Username;

            bool doctorIsAvailable = true;
            foreach (Appointment appointment in _doctorController.GetAppointmentsForDoctor(doctorUsername))
            {
                if (Appointment.GetDateTime().AddMinutes(15) > appointment.GetDateTime() && Appointment.GetDateTime() < appointment.GetDateTime().AddMinutes(15))
                    doctorIsAvailable = false;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
