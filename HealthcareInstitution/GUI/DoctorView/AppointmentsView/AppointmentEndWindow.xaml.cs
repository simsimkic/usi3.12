using HealthcareInstitution.Core.Equipment.Controllers;
using HealthcareInstitution.Core.Equipment.Models;
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

namespace HealthcareInstitution.GUI.DoctorView.AppointmentsView
{
    /// <summary>
    /// Interaction logic for AppointmentEndWindow.xaml
    /// </summary>
    public partial class AppointmentEndWindow : Window
    {
        private DynamicEquipmentController _equipmentController;
        public ObservableCollection<DynamicEquipment> DynamicEquipment { get; set; }
        public AppointmentEndWindow()
        {
            InitializeComponent();
            DataContext = this;
            _equipmentController = new DynamicEquipmentController();
            DynamicEquipment = new ObservableCollection<DynamicEquipment>(_equipmentController.GetAllDynamicEquipment());

        }
    }
}
