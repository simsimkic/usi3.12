using HealthcareInstitution.Core.MedicalRecord.DAOs;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Users.DAOs;
using HealthcareInstitution.Users.Models;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.MedicalRecord.Controllers
{
    public class MedicalRecordController
    {
        private MedicalRecordDAO _medicalRecords;

        public MedicalRecordController()
        {
            _medicalRecords = new MedicalRecordDAO();
        }

        public List<Models.MedicalRecord> GetAllMedicalRecords()
        {
            return _medicalRecords.GetAll();
        }
        public Models.MedicalRecord? GetMedicalRecordById(int id)
        {
            return _medicalRecords.GetById(id);
        }
        public void Create(Models.MedicalRecord medicalRecord)
        {
            _medicalRecords.Add(medicalRecord);
        }
        public void Change(Models.MedicalRecord medicalRecord)
        {
            _medicalRecords.Update(medicalRecord);
        }
        public void Delete(Models.MedicalRecord medicalRecord)
        {
            _medicalRecords.Remove(medicalRecord);
        }

        public void Subscribe(IObserver observer)
        {
            _medicalRecords.Subscribe(observer);
        }
    }
}
