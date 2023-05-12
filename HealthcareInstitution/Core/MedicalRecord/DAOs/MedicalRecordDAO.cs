using HealthcareInstitution.Core.MedicalRecord.Models;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Core.Users.Storages;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.MedicalRecord.DAOs
{
    internal class MedicalRecordDAO:ISubject
    {

        private List<IObserver> _observers;
        private Storages.MedicalRecordStorage _medicalRecordStorage;
        private List<Models.MedicalRecord> _medicalRecords;
        private Dictionary<int, Models.MedicalRecord> _medicalRecordsById;

        public MedicalRecordDAO()
        {
            _medicalRecordStorage = new Storages.MedicalRecordStorage();
            _medicalRecords = _medicalRecordStorage.Load();
            _medicalRecordsById = _medicalRecordStorage.Load().ToDictionary(m => m.Id, m => m);
            _observers = new List<IObserver>();
        }
        public int NextId()
        {
            return _medicalRecords.Max(m => m.Id) + 1;
        }
        public void Add(Models.MedicalRecord medicalRecord)
        {
            medicalRecord.Id = NextId();
            _medicalRecords.Add(medicalRecord);
            _medicalRecordsById.Add(medicalRecord.Id, medicalRecord);
            _medicalRecordStorage.Save(_medicalRecords);
            NotifyObservers();
        }
        public void Update(Models.MedicalRecord updateMedicalRecord)
        {
            Models.MedicalRecord? medicalRecord = _medicalRecords.Find(m => m.Id == updateMedicalRecord.Id);
            if(medicalRecord != null)
            {
                medicalRecord.Height = updateMedicalRecord.Height;
                medicalRecord.Weight = updateMedicalRecord.Weight;
                medicalRecord.MedicalHistory = updateMedicalRecord.MedicalHistory;
                _medicalRecordsById[updateMedicalRecord.Id] = medicalRecord;
                _medicalRecordStorage.Save(_medicalRecords);
                NotifyObservers();
            }
        }

        public void Remove(Models.MedicalRecord medicalRecord)
        {
            _medicalRecords.Remove(medicalRecord);
            _medicalRecordsById.Remove(medicalRecord.Id);
            _medicalRecordStorage.Save(_medicalRecords);
            NotifyObservers();
        }

        public List<Models.MedicalRecord> GetAll()
        {
            return _medicalRecords;
        }

        public Dictionary<int, Models.MedicalRecord> GetAllById()
        {
            return _medicalRecordsById;
        }

        public Models.MedicalRecord? GetById(int id)
        {
            if(_medicalRecordsById.ContainsKey(id))
                return _medicalRecordsById[id];
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
