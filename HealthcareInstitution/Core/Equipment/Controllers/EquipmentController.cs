using HealthcareInstitution.Core.Equipment.DAOs;
using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.GUI.ManagerView.EquipmentView;
using HealthcareInstitution.GUI.ManagerView.EquipmentView.ArrangeEquipmentView;
using HealthcareInstitution.Users.DAOs;
using HealthcareInstitution.Users.Models;
using HealthcareInstitution.Utilities.Enums;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.Controllers
{
    public class EquipmentController
    {
        private readonly EquipmentDAO _equipment;

        public EquipmentController()
        {
            _equipment = new EquipmentDAO();
        }

        public List<Models.Equipment> GetAllEquipment()
        {
            return _equipment.GetAll();
        }

        public Models.Equipment? GetEquipmentById(int id)
        {
            return _equipment.GetById(id);
        }

        public Models.Equipment? FindEquipmentInRoom(string equipmentName, int roomId)
        {
            return _equipment.FindInRoom(equipmentName, roomId);
        }

        public List<EquipmentTableItem> GetFilteredEquipment(EquipmentFilter equipmentFilter)
        {
            RoomController _rooms = new();
            List<EquipmentTableItem> items = new();

            foreach (Models.Equipment equipment in GetAllEquipment())
            {
                Room? room = _rooms.GetRoomById(equipment.RoomId);
                if (room != null)
                {
                    EquipmentTableItem item = new(equipment, room);
                    if (CheckEquipment(item, equipmentFilter))
                        items.Add(item);
                }
            }
            return items;
        }

        private static bool CheckEquipment(EquipmentTableItem item, EquipmentFilter equipmentFilter)
        {
            bool idMatchesSearch = item.Equipment.Id.ToString().Contains(equipmentFilter.SearchText);
            bool nameMatchesSearch = item.Equipment.Name.Contains(equipmentFilter.SearchText);
            bool countMatchesSearch = item.Equipment.Count.ToString().Contains(equipmentFilter.SearchText);
            bool equipmentTypeMatchesSearch = item.Equipment.Type.ToString().Contains(equipmentFilter.SearchText);
            bool roomTypeMatchesSearch = item.Room.Type.ToString().Contains(equipmentFilter.SearchText);

            if (!idMatchesSearch && !nameMatchesSearch && !countMatchesSearch && !equipmentTypeMatchesSearch && !roomTypeMatchesSearch)
                return false;

            if (equipmentFilter.RoomType != null)
                if (item.Room.Type != equipmentFilter.RoomType)
                    return false;

            if (equipmentFilter.EquipmentType != null)
                if (item.Equipment.Type != equipmentFilter.EquipmentType) 
                    return false;

            if (equipmentFilter.QuantityRange != null)
                if (item.Equipment.Count < equipmentFilter.QuantityRange.LowerBound || item.Equipment.Count > equipmentFilter.QuantityRange.UpperBound) 
                    return false;   

            if (equipmentFilter.IsLocationNotWarehouse && item.Room.Type == RoomType.Warehouse)
                return false;

            return true;
        }

        public List<ArrangeEquipmentTableItem> GetArrangeEquipmentTableItems()
        {
            RoomController _rooms = new();
            List<ArrangeEquipmentTableItem> items = new();

            foreach (Models.Equipment equipment in GetAllEquipment())
            {
                Room? room = _rooms.GetRoomById(equipment.RoomId);
                if (room != null)
                {
                    ArrangeEquipmentTableItem item = new(equipment.Id, equipment.Name, equipment.Count, EquipmentCategory.Regular, room.Id, room.Type);
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
                int count = 0;

                foreach (Models.Equipment equipment in GetAllEquipment())
                {
                    if (selectedEquipment.Name == equipment.Name && equipment.RoomId == room.Id)
                    {
                        count = equipment.Count;
                        break;
                    }
                }

                if (selectedEquipment.RoomId != room.Id)
                {
                    DestinationComboBoxItem item = new(room.Id, room.Type, count);
                    items.Add(item);
                }
            }

            return items;
        }

        public void PerformEquipmentArrangements(EquipmentArrangementController equipmentArrangementController)
        {
            foreach (EquipmentArrangement equipmentArrangement in equipmentArrangementController.GetAllEquipmentArrangements().ToList())
            {
                if (equipmentArrangement.ExecutionOn < DateTime.Now)
                {
                    Models.Equipment? equipment = FindEquipmentInRoom(equipmentArrangement.EquipmentName, equipmentArrangement.CurrentLocationId);

                    if (equipment == null)
                        return;

                    if (equipment.Count < equipmentArrangement.Count)
                        return;

                    equipment.Count -= equipmentArrangement.Count;
                    Change(equipment);

                    Models.Equipment? equipmentInDestination = FindEquipmentInRoom(equipment.Name, equipmentArrangement.DestinationId);

                    if (equipmentInDestination != null)
                    {
                        equipmentInDestination.Count += equipmentArrangement.Count;
                        Change(equipmentInDestination);
                    }
                    else
                    {
                        Models.Equipment newEquipment = new(-1, equipmentArrangement.Count, equipmentArrangement.EquipmentName, equipment.Type, equipmentArrangement.DestinationId);
                        Create(newEquipment);
                    }

                    equipmentArrangementController.Delete(equipmentArrangement);
                }
            }
        }

        public void Create(Models.Equipment equipment)
        {
            _equipment.Add(equipment);
        }

        public void Delete(Models.Equipment equipment)
        {
            _equipment.Remove(equipment);
        }

        public void Change(Models.Equipment updatedEquipment)
        {
            _equipment.Update(updatedEquipment);
        }

        public void Subscribe(IObserver observer)
        {
            _equipment.Subscribe(observer);
        }
    }
}
