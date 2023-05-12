using HealthcareInstitution.Core.MedicalRecord.Controllers;
using HealthcareInstitution.Core.MedicalRecord.Models;
using HealthcareInstitution.Core.Users.Controllers;
using HealthcareInstitution.Core.Users.Models;
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
using System.Windows.Shapes;

namespace HealthcareInstitution.GUI.NurseView.PatientsView
{
    /// <summary>
    /// Interaction logic for CreatePatientWindow.xaml
    /// </summary>
    public partial class CreatePatientWindow : Window
    {

        public Patient Patient { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        private MedicalRecordController _medicalRecordController;
        private PatientController _patientController;


        public CreatePatientWindow(PatientController patientController, MedicalRecordController medicalRecordController)
        {
            InitializeComponent();
            DataContext = this;
            _medicalRecordController = medicalRecordController;  
            _patientController = patientController;   
            Patient = new Patient();
            MedicalRecord = new MedicalRecord();
        }

        private void CreatePatient_Click(object sender, RoutedEventArgs e)
        {
            if (Patient.IsValid && MedicalRecord.IsValid)
            {
                _medicalRecordController.Create(MedicalRecord);
                Patient.MedicalRecordId = MedicalRecord.Id;
                _patientController.Create(Patient);
                Close();
            }
            else
            {
                
                MessageBox.Show("Patient couldn't be created, because not all fields are valid!");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
