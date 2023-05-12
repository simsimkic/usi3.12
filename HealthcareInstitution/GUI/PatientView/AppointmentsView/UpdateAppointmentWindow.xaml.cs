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
    /// Interaction logic for UpdateAppointmentWindow.xaml
    /// </summary>
    public partial class UpdateAppointmentWindow : Window
    {
        private DoctorController _doctorController;
        private AppointmentController _appointmentController;
        private AppointmentHistoryController _appointmentHistoryController;
        private User _user;
        public ObservableCollection<Doctor> Doctors { get; set; }
        public AppointmentsTableItem Item { get; set; }
        public Appointment Appointment { get; set; }
        public Doctor Doctor { get; set; }
        public Window ParentWindow { get; set; }
        public UpdateAppointmentWindow(Window window, User user, AppointmentController appointmentController, AppointmentHistoryController appointmentHistoryController, AppointmentsTableItem selectedAppointment)
        {
            InitializeComponent();
            DataContext = this;

            _doctorController = new DoctorController();
            _appointmentHistoryController = appointmentHistoryController;
            _appointmentController = appointmentController;
            _user = user;

            Doctors = new ObservableCollection<Doctor>(_doctorController.GetAllDoctors());
            ParentWindow = window;
            Item = selectedAppointment;
            AppointmentDate.Text = Item.Appointment.Date.ToString();
            AppointmentTime.Text = Item.Appointment.Time;
            Appointment = new Appointment();
            Doctor = new Doctor();

            LoadComboBox();
        }

        private void LoadComboBox()
        {
            int currentIndex = 0;
            int selectedDoctorIndex = 0;
            foreach (Doctor doctor in Doctors)
            {
                DoctorsComboBox.Items.Add(doctor.Name + " " + doctor.Surname);
                if (doctor.Username == Item.Doctor.Username)
                    selectedDoctorIndex = currentIndex;
                currentIndex++;
            }
            DoctorsComboBox.SelectedIndex = selectedDoctorIndex;
        }

        private void UpdateAppointment_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = Appointment;
            appointment.Id = Item.Appointment.Id;
            if (appointment.IsValid)
            {
                string doctorUsername = Doctors[DoctorsComboBox.SelectedIndex].Username;

                bool doctorIsAvailable = true;
                foreach (Appointment a in _doctorController.GetAppointmentsForDoctor(doctorUsername))
                {
                    if (appointment.Id == a.Id)
                        continue;
                    if (appointment.GetDateTime().AddMinutes(15) > a.GetDateTime() && appointment.GetDateTime() < a.GetDateTime().AddMinutes(15))
                        doctorIsAvailable = false;
                }
                if (doctorIsAvailable)
                { 
                    Appointment updatedAppointment = new Appointment(appointment.Id, appointment.Date, appointment.Time, appointment.PatientUsername, doctorUsername,appointment.Anamnesis,appointment.IsFinished);
                    _appointmentController.Change(updatedAppointment);
                    _appointmentHistoryController.Create(new AppointmentHistory(-1, appointment.Id, _user.Username, Utilities.Enums.AppointmentStatus.Changed, DateTime.Now));

                    if (_appointmentHistoryController.CheckToBlockPatient(_user.Username))
                    {
                        MessageBox.Show("You have been blocked.");
                        ParentWindow.Close();
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show("Doctor is not available at chosen time.");
                }
            }
            else
            {
                MessageBox.Show("Appointment couldn't be update, because not all fields are valid!");
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
