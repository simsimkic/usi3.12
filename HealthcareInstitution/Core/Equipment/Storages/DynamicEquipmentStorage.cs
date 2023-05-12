using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.Storages
{
    public class DynamicEquipmentStorage
    {
        private const string StoragePath = "../../../Data/dynamicEquipment.csv";

        private readonly Serializer<DynamicEquipment> _serializer;

        public DynamicEquipmentStorage()
        {
            _serializer = new Serializer<DynamicEquipment>();
        }

        public List<DynamicEquipment> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<DynamicEquipment> dynamicEquipment)
        {
            _serializer.ToCSV(StoragePath, dynamicEquipment);
        }
    }
}
