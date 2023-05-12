using HealthcareInstitution.Users.Models;
using HealthcareInstitution.Users.Storages;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Users.DAOs
{
    public class ManagerDAO : ISubject
    {
        private List<IObserver> _observers;
        private ManagerStorage _storage;
        private List<Manager> _managers;
        private Dictionary<string, Manager> _managersByUsername;

        public ManagerDAO()
        {
            _storage = new ManagerStorage();
            _managers = _storage.Load();
            _managersByUsername = _storage.Load().ToDictionary(m => m.Username, m => m);
            _observers = new List<IObserver>();
        }

        public void Add(Manager manager)
        {
            _managers.Add(manager);
            _managersByUsername.Add(manager.Username, manager);
            _storage.Save(_managers);
            NotifyObservers();
        }

        public void Remove(Manager manager)
        {
            _managers.Remove(manager);
            _managersByUsername.Remove(manager.Username);    
            _storage.Save(_managers);
            NotifyObservers();
        }

        public List<Manager> GetAll()
        {
            return _managers;
        }

        public Dictionary<string, Manager> GetALlByUsername()
        {
            return _managersByUsername;
        }

        public Manager? GetByUsername(string username)
        {
            if (_managersByUsername.ContainsKey(username))
                return _managersByUsername[username];
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
