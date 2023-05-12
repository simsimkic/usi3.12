using HealthcareInstitution.Core.Equipment.Controllers;
using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.GUI.ManagerView.EquipmentView.DynamicEquipmentView;
using HealthcareInstitution.Utilities.Enums;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HealthcareInstitution.GUI.ManagerView.EquipmentView
{
    /// <summary>
    /// Interaction logic for DynamicEquipmentTableWindow.xaml
    /// </summary>
    public partial class DynamicEquipmentTableWindow : Window, INotifyPropertyChanged, IObserver
    {
        private readonly DynamicEquipmentController _dynamicEquipmentController;
        private readonly DynamicEquipmentRequestController _dynamicEquipmentRequestController;

        private DynamicEquipmentTableItem? _selectedDynamicEquipment;
        public DynamicEquipmentTableItem? SelectedDynamicEquipment
        {
            get => _selectedDynamicEquipment;
            set
            {
                if (value != _selectedDynamicEquipment)
                {
                    _selectedDynamicEquipment = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<DynamicEquipmentTableItem> DynamicEquipment { get; set; }

        public DynamicEquipmentTableWindow(DynamicEquipmentController dynamicEquipmentController, DynamicEquipmentRequestController dynamicEquipmentRequestController)
        {
            InitializeComponent();
            DataContext = this;

            _dynamicEquipmentController = dynamicEquipmentController;
            _dynamicEquipmentController.Subscribe(this);

            _dynamicEquipmentRequestController = dynamicEquipmentRequestController;

            _selectedDynamicEquipment = null;

            DynamicEquipment = new ObservableCollection<DynamicEquipmentTableItem>(_dynamicEquipmentController.GetDynamicEquipmentTableItems());

            CheckArrivedDynamicEquipment();
        }

        private void CheckArrivedDynamicEquipment()
        {
            _dynamicEquipmentController.MoveArrivedDynamicEquipment(_dynamicEquipmentRequestController);
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            CheckArrivedDynamicEquipment();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDynamicEquipment == null)
            {
                MessageBox.Show("Select the dynamic equipment you want to order first.", "Error");
                return;
            }

            if (!IsCountValid())
            {
                MessageBox.Show("Invalid quantity number. Please, try again.", "Error");
                return;
            }

            int count = int.Parse(CountTextBox.Text);
            _dynamicEquipmentRequestController.Create(new DynamicEquipmentRequest(-1, SelectedDynamicEquipment.Name, count, DateTime.Now));
            LastOrderedTextBlock.Text = $"You have successfully ordered {count} {SelectedDynamicEquipment.Name}(s).";
            CountTextBox.Clear();
            SelectedDynamicEquipment = null;
        }

        private bool IsCountValid()
        {
            return int.TryParse(CountTextBox.Text, out _);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateDynamicEquipmentList()
        {
            DynamicEquipment.Clear();
            foreach (DynamicEquipmentTableItem dynamicEquipmentTableItem in _dynamicEquipmentController.GetDynamicEquipmentTableItems())
            {
                DynamicEquipment.Add(dynamicEquipmentTableItem);
            }
        }

        public void Update()
        {
            UpdateDynamicEquipmentList();
        }
    }
}
