using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.Storages
{
    public class EquipmentArrangementStorage
    {
        private const string StoragePath = "../../../Data/equipmentArrangements.csv";

        private readonly Serializer<EquipmentArrangement> _serializer;

        public EquipmentArrangementStorage()
        {
            _serializer = new Serializer<EquipmentArrangement>();
        }

        public List<EquipmentArrangement> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<EquipmentArrangement> equipmentArrangements)
        {
            _serializer.ToCSV(StoragePath, equipmentArrangements);
        }
    }
}
