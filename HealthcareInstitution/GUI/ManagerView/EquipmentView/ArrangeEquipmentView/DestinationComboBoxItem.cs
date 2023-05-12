using HealthcareInstitution.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.GUI.ManagerView.EquipmentView.ArrangeEquipmentView
{
    public class DestinationComboBoxItem
    {
        public int RoomId { get; set; }
        public RoomType RoomType { get; set; }
        public int EquipmentCount { get; set; }

        public DestinationComboBoxItem(int roomId, RoomType roomType, int equipmentCount)
        {
            this.RoomId = roomId;
            this.RoomType = roomType;
            this.EquipmentCount = equipmentCount;
        }

        public override string ToString()
        {
            return $"Id: {this.RoomId}, Location: {this.RoomType}, Quantity: {this.EquipmentCount}";
        }
    }
}
