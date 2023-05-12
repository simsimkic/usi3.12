using HealthcareInstitution.Core.Equipment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.GUI.ManagerView.EquipmentView
{
    public class EquipmentTableItem
    {
        public Equipment Equipment { get; set; }
        public Room Room { get; set; }

        public EquipmentTableItem(Equipment equipment, Room room)
        {
            this.Equipment = equipment;
            this.Room = room;
        }
    }
}
