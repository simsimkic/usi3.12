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
    public class UserDAO : ISubject
    {
        private List<IObserver> _observers;
        private UserStorage _storage;
        private List<User> _users;
        private Dictionary<string, User> _usersByUsername;

        public UserDAO()
        {
            _storage = new UserStorage();
            _users = _storage.Load();
            _usersByUsername = _storage.Load().ToDictionary(u => u.Username, u => u);
            _observers = new List<IObserver>();
        }

        public void Add(User user)
        {
            _users.Add(user);
            _usersByUsername.Add(user.Username, user);
            _storage.Save(_users);
            NotifyObservers();
        }

        public void Remove(User user)
        {
            _users.Remove(user);
            _usersByUsername.Remove(user.Username);
            _storage.Save(_users);
            NotifyObservers();
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public Dictionary<string, User> GetAllByUsername()
        {
            return _usersByUsername;
        }

        public User? GetByUsername(string username)
        {
            if (_usersByUsername.ContainsKey(username))
                return _usersByUsername[username];
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
