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
    public class DynamicEquipmentRequestDAO : ISubject
    {
        private readonly List<IObserver> _observers;
        private readonly DynamicEquipmentRequestStorage _storage;
        private readonly List<DynamicEquipmentRequest> _dynamicEquipmentRequests;
        private readonly Dictionary<int, DynamicEquipmentRequest> _dynamicEquipmentRequestsById;

        public DynamicEquipmentRequestDAO()
        {
            _storage = new DynamicEquipmentRequestStorage();
            _dynamicEquipmentRequests = _storage.Load();
            _dynamicEquipmentRequestsById = _storage.Load().ToDictionary(d => d.Id, d => d);
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_dynamicEquipmentRequests.Count > 0)
                return _dynamicEquipmentRequests.Max(d => d.Id) + 1;
            return 1;
        }

        public void Add(DynamicEquipmentRequest dynamicEquipmentRequest)
        {
            dynamicEquipmentRequest.Id = NextId();
            _dynamicEquipmentRequests.Add(dynamicEquipmentRequest);
            _dynamicEquipmentRequestsById.Add(dynamicEquipmentRequest.Id, dynamicEquipmentRequest);
            _storage.Save(_dynamicEquipmentRequests);
            NotifyObservers();
        }

        public void Remove(DynamicEquipmentRequest dynamicEquipmentRequest)
        {
            _dynamicEquipmentRequests.Remove(dynamicEquipmentRequest);
            _dynamicEquipmentRequestsById.Remove(dynamicEquipmentRequest.Id);
            _storage.Save(_dynamicEquipmentRequests);
            NotifyObservers();
        }

        public List<DynamicEquipmentRequest> GetAll()
        {
            return _dynamicEquipmentRequests;
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
