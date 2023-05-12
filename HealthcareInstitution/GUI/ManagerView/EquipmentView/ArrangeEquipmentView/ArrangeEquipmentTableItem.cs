using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.GUI.ManagerView.EquipmentView.ArrangeEquipmentView
{
    public class ArrangeEquipmentTableItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public EquipmentCategory Category { get; set; }
        public int RoomId { get; set; }
        public RoomType RoomType { get; set; }

        public ArrangeEquipmentTableItem(int id, string name, int count, EquipmentCategory category, int roomId, RoomType roomType)
        {
            this.Id = id;
            this.Name = name;
            this.Count = count;
            this.Category = category;
            this.RoomId = roomId;
            this.RoomType = roomType;
        }
    }
}
