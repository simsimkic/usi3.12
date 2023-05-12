using HealthcareInstitution.Core.MedicalRecord.Controllers;
using HealthcareInstitution.Core.MedicalRecord.Models;
using HealthcareInstitution.Core.Users.Controllers;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.GUI.NurseView.PatientsView;
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

namespace HealthcareInstitution.GUI.PatientView.AppointmentsView
{
    /// <summary>
    /// Interaction logic for MedicalRecordWindow.xaml
    /// </summary>
    public partial class MedicalRecordWindow : Window
    {
        private User _user;
        private MedicalRecordController? _medicalRecordController;
        private PatientController? _patientController;
        private Patient? patient { get; set; }
        private MedicalRecord? medicalRecord { get; set; }
        public MedicalRecordWindow(Window window, User user)
        {
            InitializeComponent();
            DataContext = this;

            _user = user;
            _patientController = new PatientController();
            _medicalRecordController = new MedicalRecordController();

            patient = new Patient();
            medicalRecord = new MedicalRecord();

            patient = _patientController.GetPatientByUsername(_user.Username);
            medicalRecord = _medicalRecordController.GetMedicalRecordById(patient.MedicalRecordId);

            PatientName.Text = patient.Name;
            PatientSurname.Text = patient.Surname;
            PatientPassword.Text = patient.Password;
            PatientUsername.Text = patient.Username;
            PatientWeight.Text = medicalRecord.Weight.ToString();
            PatientHeight.Text = medicalRecord.Height.ToString();
            PatientMedicalHistory.Text = medicalRecord.MedicalHistory;
        }
    }
}
