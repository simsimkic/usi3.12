using HealthcareInstitution.Core.Equipment.DAOs;
using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.GUI.ManagerView.EquipmentView.ArrangeEquipmentView;
using HealthcareInstitution.GUI.ManagerView.EquipmentView.DynamicEquipmentView;
using HealthcareInstitution.Utilities.Enums;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.Controllers
{
    public class DynamicEquipmentController
    {
        private DynamicEquipmentDAO _dynamicEquipment;

        public DynamicEquipmentController()
        {
            _dynamicEquipment = new DynamicEquipmentDAO();
        }

        public List<DynamicEquipment> GetAllDynamicEquipment()
        {
            return _dynamicEquipment.GetAll();
        }

        public DynamicEquipment? GetDynamicEquipmentById(int id)
        {
            return _dynamicEquipment.GetById(id);
        }

        public DynamicEquipment? FindDynamicEquipmentInRoom(string dynamicEquipmentName, int roomId)
        {
            return _dynamicEquipment.FindInRoom(dynamicEquipmentName, roomId);
        }

        public List<DynamicEquipmentTableItem> GetDynamicEquipmentTableItems()
        {   
            List<DynamicEquipmentTableItem> items = new();

            foreach (DynamicEquipment dynamicEquipment in GetAllDynamicEquipment())
            {
                DynamicEquipmentTableItem item = new(dynamicEquipment.Name, dynamicEquipment.Count);

                DynamicEquipmentTableItem? foundItem = items.Find(x => x.Name == item.Name);

                if (foundItem == null)
                    items.Add(item);
                else
                    foundItem.Count += item.Count;
            }

            foreach (DynamicEquipmentTableItem item in items.ToList())
            {
                if (item.Count >= 5)
                    items.Remove(item);
            }

            return items;
        }

        public void MoveArrivedDynamicEquipment(DynamicEquipmentRequestController dynamicEquipmentRequestController)
        {
            List<DynamicEquipmentRequest> arrivedDynamicEquipment = GetArrivedDynamicEquipment(dynamicEquipmentRequestController);
            if (arrivedDynamicEquipment.Count > 0)
                MoveToWarehouse(arrivedDynamicEquipment, dynamicEquipmentRequestController);
        }

        private List<DynamicEquipmentRequest> GetArrivedDynamicEquipment(DynamicEquipmentRequestController dynamicEquipmentRequestController)
        {
            List<DynamicEquipmentRequest> items = new();
            foreach (DynamicEquipmentRequest dynamicEquipmentRequest in dynamicEquipmentRequestController.GetAllDynamicEquipmentRequests())
            {
                if (dynamicEquipmentRequest.CreatedOn.AddDays(1) < DateTime.Now)
                    items.Add(dynamicEquipmentRequest);
            }
            return items;
        }

        private void MoveToWarehouse(List<DynamicEquipmentRequest> arrivedDynamicEquipment, DynamicEquipmentRequestController dynamicEquipmentRequestController)
        {
            int warehouseId = new RoomDAO().GetWarehouseId();
            foreach (DynamicEquipmentRequest dynamicEquipmentRequest in arrivedDynamicEquipment)
            {
                DynamicEquipment? foundDynamicEquipment = FindDynamicEquipmentInRoom(dynamicEquipmentRequest.DynamicEquipmentName, warehouseId);
                if (foundDynamicEquipment != null)
                {
                    foundDynamicEquipment.Count += dynamicEquipmentRequest.Count;
                    Change(foundDynamicEquipment);
                }
                else
                {
                    DynamicEquipment dynamicEquipment = new(-1, dynamicEquipmentRequest.Count, dynamicEquipmentRequest.DynamicEquipmentName, warehouseId);
                    Create(dynamicEquipment);
                }
                dynamicEquipmentRequestController.Delete(dynamicEquipmentRequest);
            }
        }

        public List<ArrangeEquipmentTableItem> GetArrangeEquipmentTableItems()
        {
            RoomController _rooms = new();
            List<ArrangeEquipmentTableItem> items = new();

            foreach (DynamicEquipment equipment in GetAllDynamicEquipment())
            {
                Room? room = _rooms.GetRoomById(equipment.RoomId);
                if (room != null)
                {
                    ArrangeEquipmentTableItem item = new(equipment.Id, equipment.Name, equipment.Count, EquipmentCategory.Dynamic, room.Id, room.Type);
                    items.Add(item);
                }
            }

            return items;
        }

        public List<DestinationComboBoxItem> GetDestinationComboBoxItems(ArrangeEquipmentTableItem selectedEquipment)
        {
            List<DestinationComboBoxItem> items = new();

            RoomDAO rooms = new();

            foreach (Room room in rooms.GetAll())
            {
                int equipmentCount = 0;

                foreach (DynamicEquipment equipment in GetAllDynamicEquipment())
                {
                    if (selectedEquipment.Name == equipment.Name && equipment.RoomId == room.Id)
                    {
                        equipmentCount = equipment.Count;
                        break;
                    }
                }

                if (selectedEquipment.RoomId != room.Id && equipmentCount < 5)
                {
                    DestinationComboBoxItem item = new(room.Id, room.Type, equipmentCount);
                    items.Add(item);
                }
            }

            return items;
        }

        public void MoveEquipment(ArrangeEquipmentTableItem selectedEquipment, int destinationRoomId, int count)
        {
            DynamicEquipment? equipment = GetDynamicEquipmentById(selectedEquipment.Id);

            if (equipment == null)
                return;

            equipment.Count -= count;
            Change(equipment);
            DynamicEquipment? equipmentInDestination = FindDynamicEquipmentInRoom(equipment.Name, destinationRoomId);

            if (equipmentInDestination != null)
            {
                equipmentInDestination.Count += count;
                Change(equipmentInDestination);
            }
            else
            {
                DynamicEquipment newEquipment = new(-1, count, selectedEquipment.Name, destinationRoomId);
                Create(newEquipment);
            }
        }

        public void Create(DynamicEquipment dynamicEquipment)
        {
            _dynamicEquipment.Add(dynamicEquipment);
        }

        public void Delete(DynamicEquipment dynamicEquipment)
        {
            _dynamicEquipment.Remove(dynamicEquipment);
        }

        public void Change(DynamicEquipment updatedDynamicEquipment)
        {
            _dynamicEquipment.Update(updatedDynamicEquipment);
        }

        public void Subscribe(IObserver observer)
        {
            _dynamicEquipment.Subscribe(observer);
        }
    }
}
