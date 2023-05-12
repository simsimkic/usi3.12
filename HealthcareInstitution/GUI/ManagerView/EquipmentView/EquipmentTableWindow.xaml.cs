using HealthcareInstitution.Core.Equipment.Controllers;
using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Utilities.Enums;
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

namespace HealthcareInstitution.GUI.ManagerView.EquipmentView
{
    /// <summary>
    /// Interaction logic for EquipmentTableWindow.xaml
    /// </summary>
    public partial class EquipmentTableWindow : Window, IObserver
    {
        private readonly EquipmentController _equipmentController;
        private readonly DynamicEquipmentController _dynamicEquipmentController;
        private readonly EquipmentArrangementController _equipmentArrangementController;
        private readonly DynamicEquipmentRequestController _dynamicEquipmentRequestController;

        public ObservableCollection<EquipmentTableItem> Equipment { get; set; }
        public EquipmentFilter EquipmentFilter { get; set; }
        public List<RoomType> RoomTypes { get; set; }   
        public List<EquipmentType> EquipmentTypes { get; set; }

        public EquipmentTableWindow()
        {
            InitializeComponent();
            DataContext = this;

            _equipmentController = new EquipmentController();
            _equipmentController.Subscribe(this);

            _dynamicEquipmentController = new DynamicEquipmentController();

            _equipmentArrangementController = new EquipmentArrangementController();

            _dynamicEquipmentRequestController = new DynamicEquipmentRequestController();

            EquipmentFilter = new EquipmentFilter();

            Equipment = new ObservableCollection<EquipmentTableItem>(_equipmentController.GetFilteredEquipment(EquipmentFilter));
            RoomTypes = new List<RoomType>(Enum.GetValues(typeof(RoomType)).Cast<RoomType>().ToList());
            EquipmentTypes = new List<EquipmentType>(Enum.GetValues(typeof(EquipmentType)).Cast<EquipmentType>().ToList());

            LoadComboBoxes();

            CheckEquipmentArrangements();
        }

        private void LoadComboBoxes()
        {
            RoomType.Items.Add("Any");
            foreach (RoomType roomType in RoomTypes)
            {
                RoomType.Items.Add(roomType.ToString());
            }
            RoomType.SelectedValue = RoomType.Items[0];

            EquipmentType.Items.Add("Any");
            foreach (EquipmentType equipmentType in EquipmentTypes)
            {
                EquipmentType.Items.Add(equipmentType.ToString());
            }
            EquipmentType.SelectedValue = EquipmentType.Items[0];

            Quantity.Items.Add("Any");
            Quantity.Items.Add("Not available");
            Quantity.Items.Add("0-10");
            Quantity.Items.Add("10+");
            Quantity.SelectedValue = Quantity.Items[0];
        }

        private void CheckEquipmentArrangements()
        {
            _equipmentController.PerformEquipmentArrangements(_equipmentArrangementController);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            CheckEquipmentArrangements();
        }

        private void ShowDynamicEquipment_Click(object sender, RoutedEventArgs e)
        {
            DynamicEquipmentTableWindow dynamicEquipmentTableWindow = new(_dynamicEquipmentController, _dynamicEquipmentRequestController);
            dynamicEquipmentTableWindow.Show();
        }

        private void ShowArrangeEquipment_Click(object sender, RoutedEventArgs e)
        {
            ArrangeEquipmentTableWindow arrangeEquipmentTableWindow = new(_equipmentController, _dynamicEquipmentController, _equipmentArrangementController, _dynamicEquipmentRequestController);
            arrangeEquipmentTableWindow.Show();
        }

        private void UpdateEquipmentList()
        {
            Equipment.Clear();
            foreach (EquipmentTableItem equipmentTableItem in _equipmentController.GetFilteredEquipment(EquipmentFilter))
            {
                Equipment.Add(equipmentTableItem);
            }
        }

        private void FilterChanged(object sender, RoutedEventArgs e)
        {
            UpdateEquipmentList();
        }

        public void Update()
        {
            UpdateEquipmentList();
        }
    }
}
