using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.Storages
{
    public class RoomStorage
    {
        private const string StoragePath = "../../../Data/rooms.csv";

        private readonly Serializer<Room> _serializer;

        public RoomStorage()
        {
            _serializer = new Serializer<Room>();
        }

        public List<Room> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<Room> rooms)
        {
            _serializer.ToCSV(StoragePath, rooms);
        }
    }
}
