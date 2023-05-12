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
    public class NurseStorage
    {
        private const string StoragePath = "../../../Data/nurses.csv";

        private Serializer<Nurse> _serializer;

        public NurseStorage()
        {
            _serializer = new Serializer<Nurse>();
        }

        public List<Nurse> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<Nurse> nurses)
        {
            _serializer.ToCSV(StoragePath, nurses);
        }
    }
}
