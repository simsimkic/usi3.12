using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Core.Users.Storages;
using HealthcareInstitution.Users.Models;
using HealthcareInstitution.Users.Storages;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Users.DAOs
{
    public class NurseDAO
    {
        private List<IObserver> _observers;
        private NurseStorage _storage;
        private List<Nurse> _nurses;
        private Dictionary<string, Nurse> _nursesByUsername;

        public NurseDAO()
        {
            _storage = new NurseStorage();
            _nurses = _storage.Load();
            _nursesByUsername = _storage.Load().ToDictionary(m => m.Username, m => m);
            _observers = new List<IObserver>();
        }

        public void Add(Nurse nurse)
        {
            _nurses.Add(nurse);
            _nursesByUsername.Add(nurse.Username, nurse);
            _storage.Save(_nurses);
            NotifyObservers();
        }

        public void Remove(Nurse nurse)
        {
            _nurses.Remove(nurse);
            _nursesByUsername.Remove(nurse.Username);
            _storage.Save(_nurses);
            NotifyObservers();
        }

        public List<Nurse> GetAll()
        {
            return _nurses;
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
