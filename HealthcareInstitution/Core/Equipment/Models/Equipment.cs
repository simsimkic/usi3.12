using HealthcareInstitution.Utilities.Enums;
using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.Models
{
    public class Equipment : ISerializable
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public EquipmentType Type { get; set; }
        public int RoomId { get; set; }

        public Equipment()
        {
            this.Id = -1;
            this.Count = 0;
            this.Name = string.Empty;
            this.Type = EquipmentType.HallwayEquipment;
            this.RoomId = -1;
        }

        public Equipment(int id, int count, string name, EquipmentType type, int roomId)
        {
            this.Id = id;
            this.Count = count; 
            this.Name = name;
            this.Type = type;
            this.RoomId = roomId;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.Count.ToString(), this.Name, this.Type.ToString(), this.RoomId.ToString() };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.Count = int.Parse(values[1]);
            this.Name = values[2];
            this.Type = Enum.Parse<EquipmentType>(values[3], true);
            this.RoomId = int.Parse(values[4]);
        }

        public override string ToString()
        {
            return $"{this.Id} {this.Count} {this.Name} {this.Type} {this.RoomId}";
        }
    }
}
