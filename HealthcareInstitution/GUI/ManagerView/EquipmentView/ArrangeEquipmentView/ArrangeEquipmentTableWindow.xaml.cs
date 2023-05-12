using HealthcareInstitution.Core.Equipment.Controllers;
using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.GUI.ManagerView.EquipmentView.ArrangeEquipmentView;
using HealthcareInstitution.Utilities.Enums;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
    /// Interaction logic for ArrangeEquipmentTableWindow.xaml
    /// </summary>
    public partial class ArrangeEquipmentTableWindow : Window, INotifyPropertyChanged, IObserver
    {
        private readonly EquipmentController _equipmentController;
        private readonly DynamicEquipmentController _dynamicEquipmentController;
        private readonly EquipmentArrangementController _equipmentArrangementController;
        private readonly DynamicEquipmentRequestController _dynamicEquipmentRequestController;

        private readonly string _datetimeFormat;

        private ArrangeEquipmentTableItem? _selectedEquipment;
        public ArrangeEquipmentTableItem? SelectedEquipment
        {
            get => _selectedEquipment;
            set
            {
                if (value != _selectedEquipment)
                {
                    _selectedEquipment = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ArrangeEquipmentTableItem> Equipment { get; set; }

        public ArrangeEquipmentTableWindow(EquipmentController equipmentController, DynamicEquipmentController dynamicEquipmentController, EquipmentArrangementController equipmentArrangementController, DynamicEquipmentRequestController dynamicEquipmentRequestController)
        {
            InitializeComponent();
            DataContext = this;

            _datetimeFormat = "dd.MM.yyyy. HH:mm";

            _equipmentController = equipmentController;
            _equipmentController.Subscribe(this);

            _dynamicEquipmentController = dynamicEquipmentController;
            _dynamicEquipmentController.Subscribe(this);

            _equipmentArrangementController = equipmentArrangementController;
            _equipmentArrangementController.Subscribe(this);

            _dynamicEquipmentRequestController = dynamicEquipmentRequestController;

            List<ArrangeEquipmentTableItem> items = new(_equipmentController.GetArrangeEquipmentTableItems());
            items.AddRange(_dynamicEquipmentController.GetArrangeEquipmentTableItems());
            Equipment = new ObservableCollection<ArrangeEquipmentTableItem>(items);

            CheckArrivedDynamicEquipment();
            CheckEquipmentArrangements();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            CheckArrivedDynamicEquipment();
            CheckEquipmentArrangements();
        }

        private void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEquipment == null)
            {
                MessageBox.Show("Select the equipment you want to move first.", "Error");
                return;
            }

            if (!int.TryParse(CountTextBox.Text, out int count))
            {
                MessageBox.Show("Enter a valid quantity number", "Error");
                return;
            }

            if (SelectedEquipment.Count == 0)
            {
                MessageBox.Show($"There is no {SelectedEquipment.Name} in {SelectedEquipment.RoomId} {SelectedEquipment.RoomType}", "Error");
                return;
            }

            if (count > SelectedEquipment.Count || count < 1)
            {
                MessageBox.Show($"Quantity number must be between 1 and {SelectedEquipment.Count}", "Error");
                return;
            }

            int roomId = GetRoomIdFromComboBox();

            if (SelectedEquipment.Category == EquipmentCategory.Regular)
            {
                if (!DateTime.TryParseExact(DateTimeTextBox.Text, _datetimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime executionDate))
                {
                    MessageBox.Show("Enter a valid datetime", "Error");
                    return;
                }

                if (executionDate < DateTime.Now)
                {
                    MessageBox.Show("Enter a datetime which is not in past", "Error");
                    return;
                }

                EquipmentArrangement equipmentArrangement = new(-1, SelectedEquipment.Name, count, SelectedEquipment.RoomId, roomId, executionDate);
                _equipmentArrangementController.Create(equipmentArrangement);
            }
            else
                _dynamicEquipmentController.MoveEquipment(SelectedEquipment, roomId, count);

            CountTextBox.Text = string.Empty;
            DateTimeTextBox.IsEnabled = false;
            DateTimeTextBox.Text = string.Empty;
        }

        private int GetRoomIdFromComboBox()
        {
            string selectedRoom = DestinationComboBox.Text;
            int begin = selectedRoom.IndexOf(":") + 1;
            int end = selectedRoom.IndexOf(",");
            selectedRoom = selectedRoom[begin..end];

            return int.Parse(selectedRoom);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadDestinationComboBox();
            if (SelectedEquipment != null)
            {
                if (SelectedEquipment.Category == EquipmentCategory.Regular)
                {
                    DateTimeTextBox.IsEnabled = true;
                    DateTimeTextBox.Text = DateTime.Now.ToString("dd.MM.yyyy. HH:mm");
                }
                else
                {
                    DateTimeTextBox.IsEnabled = false;
                    DateTimeTextBox.Text = string.Empty;
                }
            }
            else
            {
                CountTextBox.Text = string.Empty;
                DateTimeTextBox.IsEnabled = false;
                DateTimeTextBox.Text = string.Empty;
            }
        }

        private void LoadDestinationComboBox()
        {
            DestinationComboBox.Items.Clear();

            if (SelectedEquipment == null)
            {
                DestinationComboBox.IsEnabled = false;
                return;
            }

            DestinationComboBox.IsEnabled = true;

            List<DestinationComboBoxItem> items;

            if (SelectedEquipment.Category == EquipmentCategory.Regular)
                items = _equipmentController.GetDestinationComboBoxItems(SelectedEquipment);
            else
                items = _dynamicEquipmentController.GetDestinationComboBoxItems(SelectedEquipment);

            foreach (DestinationComboBoxItem item in items)
            {
                ComboBoxItem comboBoxItem = new() { Content = item.ToString() };

                if (item.EquipmentCount == 0)
                {
                    comboBoxItem.Background = Brushes.Orange;
                    comboBoxItem.Content = "* " + item.ToString();
                }

                DestinationComboBox.Items.Add(comboBoxItem);
            }
            DestinationComboBox.SelectedValue = DestinationComboBox.Items[0];
        }

        private void CheckArrivedDynamicEquipment()
        {
            _dynamicEquipmentController.MoveArrivedDynamicEquipment(_dynamicEquipmentRequestController);
        }

        private void CheckEquipmentArrangements()
        {
            _equipmentController.PerformEquipmentArrangements(_equipmentArrangementController);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateEquipmentList()
        {
            Equipment.Clear();
            foreach (ArrangeEquipmentTableItem item in _equipmentController.GetArrangeEquipmentTableItems())
            {
                Equipment.Add(item);
            }
            foreach (ArrangeEquipmentTableItem item in _dynamicEquipmentController.GetArrangeEquipmentTableItems())
            {
                Equipment.Add(item);
            }
        }

        public void Update()
        {
            UpdateEquipmentList();
            LoadDestinationComboBox();
        }
    }
}
