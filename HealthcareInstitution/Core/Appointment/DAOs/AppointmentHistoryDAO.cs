using HealthcareInstitution.Core.Examination.Models;
using HealthcareInstitution.Core.Examination.Storages;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Examination.DAOs
{
    public class AppointmentHistoryDAO : ISubject
    {
        private List<IObserver> _observers;
        private AppointmentHistoryStorage _storage;
        private List<AppointmentHistory> _appointmentsHistory;
        private Dictionary<int, AppointmentHistory> _appointmentsHistoryById;

        public AppointmentHistoryDAO()
        {
            _storage = new AppointmentHistoryStorage();
            _appointmentsHistory = _storage.Load();
            _appointmentsHistoryById = _storage.Load().ToDictionary(a => a.Id, a => a);
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_appointmentsHistory.Count > 0)
                return _appointmentsHistory.Max(a => a.Id) + 1;
            return 1;
        }

        public void Add(AppointmentHistory appointmentHistory)
        {
            appointmentHistory.Id = NextId();
            _appointmentsHistory.Add(appointmentHistory);
            _appointmentsHistoryById.Add(appointmentHistory.Id, appointmentHistory);
            _storage.Save(_appointmentsHistory);
            NotifyObservers();
        }

        public void Remove(AppointmentHistory appointmentHistory)
        {
            _appointmentsHistory.Remove(appointmentHistory);
            _appointmentsHistoryById.Remove(appointmentHistory.Id);
            _storage.Save(_appointmentsHistory);
            NotifyObservers();
        }

        public List<AppointmentHistory> GetAll()
        {
            return _appointmentsHistory;
        }

        public Dictionary<int, AppointmentHistory> GetAllById()
        {
            return _appointmentsHistoryById;
        }

        public AppointmentHistory? GetById(int id)
        {
            if (_appointmentsHistoryById.ContainsKey(id))
                return _appointmentsHistoryById[id];
            return null;
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
