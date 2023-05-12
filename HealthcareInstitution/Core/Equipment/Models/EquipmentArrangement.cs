using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.Models
{
    public class EquipmentArrangement : ISerializable
    {
        public int Id { get; set; }
        public string EquipmentName { get; set; }
        public int Count { get; set; }
        public int CurrentLocationId { get; set; }
        public int DestinationId { get; set; }
        public DateTime? ExecutionOn { get; set; }

        public EquipmentArrangement()
        {
            this.Id = -1;
            this.EquipmentName = string.Empty;
            this.Count = 0;
            this.CurrentLocationId = 0;
            this.DestinationId = 0;
            this.ExecutionOn = null;
        }

        public EquipmentArrangement(int id, string equipmentName, int count, int currentLocationId, int destinationId, DateTime executeOn)
        {
            this.Id = id;
            this.EquipmentName = equipmentName;
            this.Count = count;
            this.CurrentLocationId = currentLocationId;
            this.DestinationId = destinationId;
            this.ExecutionOn = executeOn;
        }

        public virtual string[] ToCSV()
        {
            string? datetime = this.ExecutionOn.ToString();
            string[] csvValues = { this.Id.ToString(), this.EquipmentName, this.Count.ToString(), this.CurrentLocationId.ToString(), this.DestinationId.ToString(), datetime ?? "null" };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.EquipmentName = values[1];
            this.Count = int.Parse(values[2]);
            this.CurrentLocationId = int.Parse(values[3]);
            this.DestinationId = int.Parse(values[4]);
            this.ExecutionOn = values[5] != "null" ? DateTime.Parse(values[5]) : null;
        }

        public override string ToString()
        {
            return $"{this.Id} {this.EquipmentName} {this.Count} {this.CurrentLocationId} {this.DestinationId} {this.ExecutionOn}";
        }
    }
}
