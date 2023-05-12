using HealthcareInstitution.Core.Examination.Controllers;
using HealthcareInstitution.Core.Examination.Models;
using HealthcareInstitution.Core.MedicalRecord.Controllers;
using HealthcareInstitution.Core.MedicalRecord.Models;
using HealthcareInstitution.Core.Users.Controllers;
using HealthcareInstitution.Core.Users.Models;
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

namespace HealthcareInstitution.GUI.NurseView.PatientsView
{
    /// <summary>
    /// Interaction logic for CheckInPatientWindow.xaml
    /// </summary>
    public partial class CheckInPatientWindow : Window
    {


        private PatientController _patientController;
        public PatientsTableItem SelectedPatient { get; set; }
        private MedicalRecordController _medicalRecordController;
        public MedicalRecord MedicalRecord { get; set; }
        private Core.Examination.Controllers.AppointmentController _appointmentController { get; set; }
        public Patient Patient { get; set; }
        public Appointment Appointment { get; set; }

        public CheckInPatientWindow(PatientController patientController, MedicalRecordController medicalRecordController)
        {
            InitializeComponent();
            DataContext = this;
            _medicalRecordController = medicalRecordController;
            _patientController = patientController;
            Patient = new Patient();
            MedicalRecord = new MedicalRecord();
            
            grid1.IsEnabled = false;
        }

        public CheckInPatientWindow(PatientController patientController, MedicalRecordController medicalRecordController, PatientsTableItem selectedPatient)
        {
            InitializeComponent();
            DataContext = this;
            _medicalRecordController = medicalRecordController;

            _patientController = patientController;
            SelectedPatient = selectedPatient;
            
            PatientUsername.Text = SelectedPatient.Patient.Username;
            //PatientMedicalHistory.Text = SelectedPatient.MedicalRecord.MedicalHistory;
            _appointmentController = new AppointmentController();

            //_appointmentController.Subscribe(this);

            MedicalRecord = new MedicalRecord();
            Patient = new Patient();
        }
        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreatePatientWindow createPatientWindow = new CreatePatientWindow(_patientController, _medicalRecordController);
            createPatientWindow.Show();
            
            

        }
        string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                rtb.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                rtb.Document.ContentEnd
            );

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
            return textRange.Text;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Appointment = new Appointment();
            

            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("MM/dd/yyyy");
            List<Appointment> appointments = new List<Appointment>();
            bool ifAppointmentExist = false;

            foreach (Appointment appointment in _appointmentController.GetAllAppointments())
            {
                
                if(appointment.GetDateTime() > currentDateTime && appointment.GetDateTime().AddMinutes(-15) < currentDateTime
                    && appointment.PatientUsername.Equals(SelectedPatient.Patient.Username) )                {
                    appointment.Anamnesis = Syndroms.Text + "|" + Allergies.Text + "|" + MedicalHistory.Text;
                    
                    _appointmentController.Change(appointment);
                    ifAppointmentExist = true;
                    break;

                }
            }
            if (ifAppointmentExist)
            {
                MessageBox.Show("We have updated the appointment succesfully");
            }
            else
            {
                MessageBox.Show("There are no recent appointments for this patient.");
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
