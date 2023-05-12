using HealthcareInstitution.Core.MedicalRecord.Controllers;
using HealthcareInstitution.Core.MedicalRecord.Models;
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

namespace HealthcareInstitution.GUI.NurseView.PatientsView
{
    /// <summary>
    /// Interaction logic for UpdatePatientWindow.xaml
    /// </summary>
    public partial class UpdatePatientWindow : Window
    {

        private MedicalRecordController _medicalRecordController;
        private PatientController _patientController;
        
        public PatientsTableItem SelectedPatient{ get; set; }
        public Patient Patient { get; set; }
        public MedicalRecord MedicalRecord { get; set; }    

        public UpdatePatientWindow(PatientController patientController,MedicalRecordController medicalRecordController, PatientsTableItem selectedPatient)
        {
            InitializeComponent();
            DataContext = this;
            _medicalRecordController = medicalRecordController;
            
            _patientController = patientController;
            SelectedPatient = selectedPatient;

            PatientName.Text = SelectedPatient.Patient.Name;
            PatientSurname.Text = SelectedPatient.Patient.Surname;
            PatientPassword.Text = SelectedPatient.Patient.Password;
            PatientUsername.Text = SelectedPatient.Patient.Username;
            PatientWeight.Text = SelectedPatient.MedicalRecord.Weight.ToString();
            PatientHeight.Text = SelectedPatient.MedicalRecord.Height.ToString();
            PatientMedicalHistory.Text = SelectedPatient.MedicalRecord.MedicalHistory;

            MedicalRecord = new MedicalRecord();
            Patient = new Patient();
        }

        private void UpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            if (Patient.IsValidWithouUsername && MedicalRecord.IsValid)
            {
                MedicalRecord.Id = SelectedPatient.MedicalRecord.Id;
                _patientController.Change(Patient);
                _medicalRecordController.Change(MedicalRecord);

                Close();
            }
            else
            {

                MessageBox.Show("Patient couldn't be updated, because not all fields are valid!");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
