using HealthcareInstitution.Core.Equipment.Storages;
using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.Users.Models;
using HealthcareInstitution.Users.Storages;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.DAOs
{
    public class EquipmentDAO : ISubject
    {
        private readonly List<IObserver> _observers;
        private readonly EquipmentStorage _storage;
        private readonly List<Models.Equipment> _equipment;
        private readonly Dictionary<int, Models.Equipment> _equipmentById;

        public EquipmentDAO()
        {
            _storage = new EquipmentStorage();
            _equipment = _storage.Load();
            _equipmentById = _storage.Load().ToDictionary(e => e.Id, e => e);
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_equipment.Count > 0)
                return _equipment.Max(e => e.Id) + 1;
            return 1;
        }

        public void Add(Models.Equipment equipment)
        {
            equipment.Id = NextId();
            _equipment.Add(equipment);
            _equipmentById.Add(equipment.Id, equipment);
            _storage.Save(_equipment);
            NotifyObservers();
        }

        public void Remove(Models.Equipment equipment)
        {
            _equipment.Remove(equipment);
            _equipmentById.Remove(equipment.Id);
            _storage.Save(_equipment);
            NotifyObservers();
        }

        public void Update(Models.Equipment updatedEquipment)
        {
            Models.Equipment? equipment = _equipment.Find(d => d.Id == updatedEquipment.Id);
            if (equipment != null)
            {
                equipment.Name = updatedEquipment.Name;
                equipment.Count = updatedEquipment.Count;
                equipment.RoomId = updatedEquipment.RoomId;
                _equipmentById[updatedEquipment.Id] = equipment;
                _storage.Save(_equipment);
                NotifyObservers();
            }
        }

        public List<Models.Equipment> GetAll()
        {
            return _equipment;
        }

        public Models.Equipment? GetById(int id)
        {
            if (_equipmentById.ContainsKey(id))
                return _equipmentById[id];
            return null;
        }

        public Models.Equipment? FindInRoom(string equipmentName, int roomId)
        {

            return _equipment.Find(x => x.Name == equipmentName && x.RoomId == roomId);
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
