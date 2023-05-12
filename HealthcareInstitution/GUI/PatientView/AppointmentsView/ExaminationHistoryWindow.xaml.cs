using HealthcareInstitution.Core.Equipment.Controllers;
using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.Core.Examination.Controllers;
using HealthcareInstitution.Core.Users.Controllers;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.GUI.ManagerView.EquipmentView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
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
    /// Interaction logic for ExaminationHistoryWindow.xaml
    /// </summary>
    public partial class ExaminationHistoryWindow : Window
    {
        private User _user;
        private PatientController _patientController;
        public ObservableCollection<AppointmentsTableItem> Appointments { get; set; }
        public string SearchText { get; set; }
        public ExaminationHistoryWindow(Window window, User user)
        {
            InitializeComponent();
            DataContext = this;
            _user = user;
            _patientController = new PatientController();

            Appointments = new ObservableCollection<AppointmentsTableItem>(_patientController.GetAppointmentsForPatient(user.Username));

            LoadComboBox();
        }
        private void LoadComboBox()
        {
        }
        
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {/*
            if(SortByComboBox.SelectedItem == "Date")
            {

            }
            else if(SortByComboBox.SelectedItem == "Doctor")
            {

            }*/
        }

        private void UpdateAppointmentsList()
        {
            Appointments.Clear();
            foreach (AppointmentsTableItem appointmentsTableItem in _patientController.GetFilteredAppointmentsForPatient(_user.Username,SearchText))
            {
                Appointments.Add(appointmentsTableItem);
            }
        }

        private void SearchChanged(object sender, RoutedEventArgs e)
        {
            UpdateAppointmentsList();
        }
        private void ShowMedicalRecordWindow_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecordWindow medicalRecordWindow = new MedicalRecordWindow(this,_user);
            medicalRecordWindow.Show();
        }
    }
}
