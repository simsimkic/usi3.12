using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.Models
{
    public class DynamicEquipmentRequest : ISerializable
    {
        public int Id { get; set; }
        public string DynamicEquipmentName { get; set; }
        public int Count { get; set; }
        public DateTime CreatedOn { get; set; }

        public DynamicEquipmentRequest()
        {
            this.Id = -1;
            this.DynamicEquipmentName = string.Empty;
            this.Count = 0;
        }

        public DynamicEquipmentRequest(int id, string dynamicEquipmentName, int count, DateTime createdOn)
        {
            this.Id = id;
            this.DynamicEquipmentName = dynamicEquipmentName;
            this.Count = count;
            this.CreatedOn = createdOn;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.DynamicEquipmentName, this.Count.ToString(), this.CreatedOn.ToString() };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.DynamicEquipmentName = values[1];
            this.Count = int.Parse(values[2]);
            this.CreatedOn = DateTime.Parse(values[3]);
        }

        public override string ToString()
        {
            return $"{this.Id} {this.DynamicEquipmentName} {this.Count} {this.CreatedOn}";
        }
    }
}
