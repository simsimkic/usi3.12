using HealthcareInstitution.Core.Examination.Controllers;
using HealthcareInstitution.Core.Examination.Models;
using HealthcareInstitution.Core.MedicalRecord.Controllers;
using HealthcareInstitution.Core.MedicalRecord.Models;
using HealthcareInstitution.Core.Users.Controllers;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.GUI.DoctorView.AppointmentsView;
using HealthcareInstitution.GUI.MedicalRecordView;
using HealthcareInstitution.GUI.NurseView.PatientsView;
using HealthcareInstitution.GUI.PatientView.AppointmentsView;
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
using System.Windows.Shapes;

namespace HealthcareInstitution.GUI.DoctorView.AppontmentsView
{
    /// <summary>
    /// Interaction logic for AppointmentExecutionWindow.xaml
    /// </summary>
    public partial class AppointmentExecutionWindow : Window
    {
        private MedicalRecordController _medicalRecordController;
        private User _user;
        
        public MedicalRecord MedicalRecord { get; set; }
        public Patient Patient { get; set; }
        public Appointment SelectedApointment { get; set; }
        public AppointmentExecutionWindow(Window window, User user, AppointmentController appointmentController, AppointmentHistoryController appointmentHistoryController, DoctorAppointmentsTableItem selectedAppointment)
        {
            InitializeComponent();
            DataContext = this;
            _user = user;
            _medicalRecordController = new MedicalRecordController();
            SelectedApointment = selectedAppointment.Appointment;
            Patient = selectedAppointment.Patient;

            MedicalRecord = _medicalRecordController.GetMedicalRecordById(selectedAppointment.Patient.MedicalRecordId);

            PatientName.Text = Patient.Name;
            PatientSurname.Text = Patient.Surname;
            MedicalCardWeight.Text = MedicalRecord.Weight.ToString();
            MedicalCardHeight.Text = MedicalRecord.Height.ToString();
            MedicalCardMedicalHistory.Text = MedicalRecord.MedicalHistory;
        }
        private void ShowEditMedicalCardWindow_Click(object sender, RoutedEventArgs e)
        {
            EditMedicalRecord editMedicalRecord = new EditMedicalRecord(this, MedicalRecord, _medicalRecordController);
            editMedicalRecord.Show();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void FinishExaminationButton_Click(object sender, RoutedEventArgs e)
        {
            
            AppointmentEndWindow appointmentEndWindow = new AppointmentEndWindow();
            appointmentEndWindow.Show();
            Close();
        }
        
    }
}
