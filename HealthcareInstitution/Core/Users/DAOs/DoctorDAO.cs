using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Core.Users.Storages;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Users.DAOs
{
    public class DoctorDAO : ISubject
    {
        private List<IObserver> _observers;
        private DoctorStorage _storage;
        private List<Doctor> _doctors;
        private Dictionary<string, Doctor> _doctorsByUsername;

        public DoctorDAO()
        {
            _storage = new DoctorStorage();
            _doctors = _storage.Load();
            _doctorsByUsername = _storage.Load().ToDictionary(d => d.Username, d => d);
            _observers = new List<IObserver>();
        }

        public void Add(Doctor doctor)
        {
            _doctors.Add(doctor);
            _doctorsByUsername.Add(doctor.Username, doctor);
            _storage.Save(_doctors);
            NotifyObservers();
        }

        public void Remove(Doctor doctor)
        {
            _doctors.Remove(doctor);
            _doctorsByUsername.Remove(doctor.Username);
            _storage.Save(_doctors);
            NotifyObservers();
        }

        public List<Doctor> GetAll()
        {
            return _doctors;
        }

        public Dictionary<string, Doctor> GetAllByUsername()
        {
            return _doctorsByUsername;
        }

        public Doctor? GetByUsername(string username)
        {
            if (_doctorsByUsername.ContainsKey(username))
                return _doctorsByUsername[username];
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
