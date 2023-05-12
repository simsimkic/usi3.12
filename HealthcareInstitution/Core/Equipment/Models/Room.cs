using HealthcareInstitution.Utilities.Enums;
using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.Models
{
    public class Room : ISerializable
    {
        public int Id { get; set; }
        public RoomType Type { get; set; }

        public Room()
        {
            this.Id = -1;
            this.Type = RoomType.WaitingRoom;
        }

        public Room(int id, RoomType type)
        {
            this.Id = id;
            this.Type = type;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.Type.ToString() };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.Type = Enum.Parse<RoomType>(values[1], true);
        }

        public override string ToString()
        {
            return $"{this.Id} {this.Type}";
        }
    }
}
