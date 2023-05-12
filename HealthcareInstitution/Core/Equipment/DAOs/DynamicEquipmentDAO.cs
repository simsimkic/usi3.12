using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.Core.Equipment.Storages;
using HealthcareInstitution.Core.Examination.Models;
using HealthcareInstitution.Core.MedicalRecord.Models;
using HealthcareInstitution.Core.MedicalRecord.Storages;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.DAOs
{
    public class DynamicEquipmentDAO : ISubject
    {
        private readonly List<IObserver> _observers;
        private readonly DynamicEquipmentStorage _storage;
        private readonly List<DynamicEquipment> _dynamicEquipment;
        private readonly Dictionary<int, DynamicEquipment> _dynamicEquipmentById;

        public DynamicEquipmentDAO()
        {
            _storage = new DynamicEquipmentStorage();
            _dynamicEquipment = _storage.Load();
            _dynamicEquipmentById = _storage.Load().ToDictionary(d => d.Id, d => d);
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_dynamicEquipment.Count > 0)
                return _dynamicEquipment.Max(d => d.Id) + 1;
            return 1;
        }

        public void Add(DynamicEquipment dynamicEquipment)
        {
            dynamicEquipment.Id = NextId();
            _dynamicEquipment.Add(dynamicEquipment);
            _dynamicEquipmentById.Add(dynamicEquipment.Id, dynamicEquipment);
            _storage.Save(_dynamicEquipment);
            NotifyObservers();
        }

        public void Remove(DynamicEquipment dynamicEquipment)
        {
            _dynamicEquipment.Remove(dynamicEquipment);
            _dynamicEquipmentById.Remove(dynamicEquipment.Id);
            _storage.Save(_dynamicEquipment);
            NotifyObservers();
        }

        public void Update(DynamicEquipment updatedDynamicEquipment)
        {
            DynamicEquipment? dynamicEquipment = _dynamicEquipment.Find(d => d.Id == updatedDynamicEquipment.Id);
            if (dynamicEquipment != null)
            {
                dynamicEquipment.Name = updatedDynamicEquipment.Name;
                dynamicEquipment.Count = updatedDynamicEquipment.Count;
                dynamicEquipment.RoomId = updatedDynamicEquipment.RoomId;
                _dynamicEquipmentById[updatedDynamicEquipment.Id] = dynamicEquipment;
                _storage.Save(_dynamicEquipment);
                NotifyObservers();
            }
        }

        public List<DynamicEquipment> GetAll()
        {
            return _dynamicEquipment;
        }

        public DynamicEquipment? GetById(int id)
        {
            if (_dynamicEquipmentById.ContainsKey(id))
                return _dynamicEquipmentById[id];
            return null;
        }

        public DynamicEquipment? FindInRoom(string dynamicEquipmentName, int roomId)
        {

            return _dynamicEquipment.Find(x => x.Name == dynamicEquipmentName && x.RoomId == roomId);
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
