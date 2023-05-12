using HealthcareInstitution.Core.Examination.Controllers;
using HealthcareInstitution.Core.MedicalRecord.Controllers;
using HealthcareInstitution.Core.MedicalRecord.Models;
using HealthcareInstitution.Core.Users.Controllers;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.GUI.DoctorView.AppontmentsView;
using HealthcareInstitution.GUI.MedicalRecordView;
using HealthcareInstitution.GUI.NurseView.PatientsView;
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

namespace HealthcareInstitution.GUI.DoctorView.PatientsView
{
    /// <summary>
    /// Interaction logic for DoctorPatientsWindow.xaml
    /// </summary>
    public partial class DoctorPatientsWindow : Window, IObserver
    {
        private PatientController _patientController;
        private DoctorController _doctorController;
        private MedicalRecordController _medicalRecordController;
        private User _user;

        public ObservableCollection<PatientsTableItem> Patients { get; set; }
        public PatientsTableItem SelectedPatient { get; set; }
        public DoctorPatientsWindow(Window window, User user)
        {
            InitializeComponent();
            DataContext = this;
            _medicalRecordController = new MedicalRecordController();
            _medicalRecordController.Subscribe(this);

            _patientController = new PatientController();

            _user = user;
            Patients = new ObservableCollection<PatientsTableItem>(_patientController.GetAllPatientsAndMedicalRecordsByDoctor(user.Username));


        }

        private void ShowMedicalRecordWindow_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient == null)
            {
                MessageBox.Show("Choose patient to show his medical record.");
            }
            else
            {
                EditMedicalRecord readMedicalRecord = new EditMedicalRecord(this, SelectedPatient.MedicalRecord, _medicalRecordController);
                readMedicalRecord.Show();
            }
        }

        private void UpdateMedicalRecordsList()
        {
            Patients.Clear();
            foreach (PatientsTableItem patient in _patientController.GetAllPatientsAndMedicalRecordsByDoctor(_user.Username)) 
            {
                Patients.Add(patient);
            }
        }

        public void Update()
        {
            UpdateMedicalRecordsList();
        }
    }
}
