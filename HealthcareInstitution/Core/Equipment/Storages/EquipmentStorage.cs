using HealthcareInstitution.Users.Models;
using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.Storages
{
    public class EquipmentStorage
    {
        private const string StoragePath = "../../../Data/equipment.csv";

        private readonly Serializer<Models.Equipment> _serializer;

        public EquipmentStorage()
        {
            _serializer = new Serializer<Models.Equipment>();
        }

        public List<Models.Equipment> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<Models.Equipment> equipment)
        {
            _serializer.ToCSV(StoragePath, equipment);
        }
    }
}
