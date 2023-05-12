using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.Core.Equipment.Storages;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.DAOs
{
    public class EquipmentArrangementDAO : ISubject
    {
        private readonly List<IObserver> _observers;
        private readonly EquipmentArrangementStorage _storage;
        private readonly List<EquipmentArrangement> _equipmentArrangements;
        private readonly Dictionary<int, EquipmentArrangement> _equipmentArrangementsById;

        public EquipmentArrangementDAO()
        {
            _storage = new EquipmentArrangementStorage();
            _equipmentArrangements = _storage.Load();
            _equipmentArrangementsById = _storage.Load().ToDictionary(e => e.Id, e => e);
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_equipmentArrangements.Count > 0)
                return _equipmentArrangements.Max(e => e.Id) + 1;
            return 1;
        }

        public void Add(EquipmentArrangement equipmentArrangement)
        {
            equipmentArrangement.Id = NextId();
            _equipmentArrangements.Add(equipmentArrangement);
            _equipmentArrangementsById.Add(equipmentArrangement.Id, equipmentArrangement);
            _storage.Save(_equipmentArrangements);
            NotifyObservers();
        }

        public void Remove(EquipmentArrangement equipmentArrangement)
        {
            _equipmentArrangements.Remove(equipmentArrangement);
            _equipmentArrangementsById.Remove(equipmentArrangement.Id);
            _storage.Save(_equipmentArrangements);
            NotifyObservers();
        }

        public List<EquipmentArrangement> GetAll()
        {
            return _equipmentArrangements;
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
