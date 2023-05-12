using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.Storages
{
    public class DynamicEquipmentRequestStorage
    {
        private const string StoragePath = "../../../Data/dynamicEquipmentRequests.csv";

        private readonly Serializer<DynamicEquipmentRequest> _serializer;

        public DynamicEquipmentRequestStorage()
        {
            _serializer = new Serializer<DynamicEquipmentRequest>();
        }

        public List<DynamicEquipmentRequest> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<DynamicEquipmentRequest> dynamicEquipmentRequests)
        {
            _serializer.ToCSV(StoragePath, dynamicEquipmentRequests);
        }
    }
}
