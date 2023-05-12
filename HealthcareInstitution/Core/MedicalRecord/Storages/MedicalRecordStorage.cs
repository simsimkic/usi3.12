using HealthcareInstitution.Core.MedicalRecord.Models;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Users.Models;
using HealthcareInstitution.Utilities.Enums;
using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.MedicalRecord.Storages
{
    internal class MedicalRecordStorage
    {

        private const string MedicalRecordsPath = "../../../Data/medicalRecords.csv";

        private Serializer<Models.MedicalRecord> _serializer;

        public MedicalRecordStorage()
        {
            _serializer = new Serializer<Models.MedicalRecord>();
        }

        public List<Models.MedicalRecord> Load()
        {
            return _serializer.FromCSV(MedicalRecordsPath);
        }

        public void Save(List<Models.MedicalRecord> medicalRecords)
        {
            _serializer.ToCSV(MedicalRecordsPath, medicalRecords);
        }


    }
}
