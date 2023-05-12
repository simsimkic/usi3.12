using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Users.Models;
using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Users.Storages
{
    public class PatientStorage
    {
        private const string StoragePath = "../../../Data/patients.csv";

        private Serializer<Patient> _serializer;

        public PatientStorage()
        {
            _serializer = new Serializer<Patient>();
        }

        public List<Patient> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<Patient> patients)
        {
            _serializer.ToCSV(StoragePath, patients);
        }
    }
}
