using HealthcareInstitution.Core.MedicalRecord.Models;
using HealthcareInstitution.Core.MedicalRecord.Storages;
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
    public class PatientDAO : ISubject
    {
        private List<IObserver> _observers;
        private PatientStorage _storage;
        private List<Patient> _patients;
        private Dictionary<string, Patient> _patientsByUsername;

        public PatientDAO()
        {
            _storage = new PatientStorage();
            _patients = _storage.Load();
            _patientsByUsername = _storage.Load().ToDictionary(m => m.Username, m => m);
            _observers = new List<IObserver>();
        }

        public void Add(Patient patient)
        {
            _patients.Add(patient);
            _patientsByUsername.Add(patient.Username, patient);
            _storage.Save(_patients);
            NotifyObservers();
        }

        public void Update(Patient updatePatient)
        {
            Patient? patient = _patients.Find(p => p.Username == updatePatient.Username);
            if (patient != null)
            {
                patient.Name = updatePatient.Name;
                patient.Surname = updatePatient.Surname;
                patient.Password = updatePatient.Password;
                _patientsByUsername[updatePatient.Username] = patient;
                _storage.Save(_patients);
                NotifyObservers();

            }
        }
        
        public void Remove(Patient patient)
        {
            _patients.Remove(patient);
            _patientsByUsername.Remove(patient.Username);
            _storage.Save(_patients);
            NotifyObservers();
        }

        public List<Patient> GetAll()
        {
            return _patients;
        }

        public Dictionary<string, Patient> GetAllByUsername()
        {
            return _patientsByUsername;
        }

        public Patient? GetByUsername(string username)
        {
            if (_patientsByUsername.ContainsKey(username))
                return _patientsByUsername[username];
            return null;
        }

        public void Block(string username)
        {
            Patient? patient = _patients.Find(p => p.Username == username);
            if (patient != null)
            {
                patient.IsBlocked = true;
                _patientsByUsername[username] = patient;
                _storage.Save(_patients);
            }
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
