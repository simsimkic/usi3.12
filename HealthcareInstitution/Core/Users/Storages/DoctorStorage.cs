using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Users.Storages
{
    public class DoctorStorage
    {
        private const string StoragePath = "../../../Data/doctors.csv";

        private Serializer<Doctor> _serializer;

        public DoctorStorage()
        {
            _serializer = new Serializer<Doctor>();
        }

        public List<Doctor> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<Doctor> doctors)
        {
            _serializer.ToCSV(StoragePath, doctors);
        }
    }
}
