using HealthcareInstitution.Core.MedicalRecord.Controllers;
using HealthcareInstitution.Core.MedicalRecord.DAOs;
using HealthcareInstitution.Core.MedicalRecord.Models;
using HealthcareInstitution.Core.Users.Controllers;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.GUI.ManagerView.EquipmentView;
using HealthcareInstitution.GUI.NurseView.PatientsView;
using HealthcareInstitution.Utilities.Observer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for MedicalRecordsTable.xaml
    /// </summary>
    public partial class PatientsTableWindow : Window, IObserver
    {
        private PatientController _patientController;
        private MedicalRecordController _medicalRecordController;

        public ObservableCollection<PatientsTableItem> Patients { get; set; }
        public PatientsTableItem SelectedPatient { get; set; }


        public PatientsTableWindow(User user)
        {
            InitializeComponent();
            DataContext = this;

            _patientController = new PatientController();
            _patientController.Subscribe(this);

            _medicalRecordController = new MedicalRecordController();
            _medicalRecordController.Subscribe(this);

            Patients = new ObservableCollection<PatientsTableItem>(_patientController.GetAllPatientsAndMedicalRecords());
        }

        private void DeletePatientButton_Click(Object sender, RoutedEventArgs e)
        {
            if (SelectedPatient != null)
            {
                Patient patient = SelectedPatient.Patient;
                MedicalRecord medicalRecord = SelectedPatient.MedicalRecord;
                MessageBoxResult result = ConfirmStudentDeletion();

                if (result == MessageBoxResult.Yes)
                {
                    _patientController.Delete(patient);
                    _medicalRecordController.Delete(medicalRecord);
                }
            }
            else
            {

                MessageBox.Show("Choose which patient you want to delete.");
            }
        }
        private void ShowUpdatePatientWindow_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient != null)
            {
                UpdatePatientWindow updatePatientWindow = new UpdatePatientWindow(_patientController, _medicalRecordController, SelectedPatient);
                updatePatientWindow.Show();
            }
            else
            {
                MessageBox.Show("Choose which patient you want to update.");
            }
        }
        private MessageBoxResult ConfirmStudentDeletion()
        {
            string sMessageBoxText = $"Are you sure that you want to delete this patient?\n";
            string sCaption = "Confirm";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            return result;
        }
        private void ShowCreatePatientWindow_Click(object sender, RoutedEventArgs e)
        {
            CreatePatientWindow createPatientWindow = new CreatePatientWindow(_patientController, _medicalRecordController);
            createPatientWindow.Show();
        }

        
        private void UpdatePatientsList()
        {
            Patients.Clear();
            foreach (PatientsTableItem patient in _patientController.GetAllPatientsAndMedicalRecords())
            {
                Patients.Add(patient);
            }
        }

        public void Update()
        {
            UpdatePatientsList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient != null)
            {
                CheckInPatientWindow checkInPatientWindow = new CheckInPatientWindow(_patientController, _medicalRecordController, SelectedPatient);
                checkInPatientWindow.Show();
            }
            else
            {
                CheckInPatientWindow checkInPatientWindow = new CheckInPatientWindow(_patientController, _medicalRecordController);
                checkInPatientWindow.Show();
            }
            
        }
    }
}
