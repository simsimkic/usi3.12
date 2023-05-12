using HealthcareInstitution.Users.Models;
using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Users.Storages
{
    public class ManagerStorage
    {
        private const string StoragePath = "../../../Data/managers.csv";

        private Serializer<Manager> _serializer;

        public ManagerStorage() 
        {
            _serializer = new Serializer<Manager>();
        }

        public List<Manager> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<Manager> managers) 
        {
            _serializer.ToCSV(StoragePath, managers);
        }
    }
}
