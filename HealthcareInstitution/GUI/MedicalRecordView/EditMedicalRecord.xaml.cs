using HealthcareInstitution.Core.MedicalRecord.Controllers;
using HealthcareInstitution.Core.MedicalRecord.Models;
using HealthcareInstitution.Core.Users.Controllers;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.GUI.NurseView.PatientsView;
using HealthcareInstitution.Utilities.Observer;
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

namespace HealthcareInstitution.GUI.MedicalRecordView
{
    /// <summary>
    /// Interaction logic for ReadMedicalRecord.xaml
    /// </summary>
    public partial class EditMedicalRecord : Window
    {
        private MedicalRecordController _medicalRecordController;
        public MedicalRecord MedicalRecord { get; set; }
        public MedicalRecord SelectedMedicalRecord { get; set; }
        public EditMedicalRecord(Window window, MedicalRecord medicalRecord, MedicalRecordController medicalRecordController)
        {
            InitializeComponent();
            DataContext = this;
            _medicalRecordController = medicalRecordController;
            SelectedMedicalRecord = medicalRecord;
            
            MedicalCardWeight.Text = medicalRecord.Weight.ToString();
            MedicalCardHeight.Text = medicalRecord.Height.ToString();
            MedicalCardMedicalHistory.Text = medicalRecord.MedicalHistory;
            MedicalRecord = new MedicalRecord();

        }
        private void UpdateMedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            if (MedicalRecord.IsValid)
            {
                MedicalRecord.Id = SelectedMedicalRecord.Id;
                _medicalRecordController.Change(MedicalRecord);

                Close();
            }
            else
            {
                MessageBox.Show("Medical record couldn't be updated, because not all fields are valid!");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
