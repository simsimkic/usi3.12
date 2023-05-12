using HealthcareInstitution.Core.Equipment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.GUI.ManagerView.EquipmentView.DynamicEquipmentView
{
    public class DynamicEquipmentTableItem
    {
        public string Name { get; set; }
        public int Count { get; set; }

        public DynamicEquipmentTableItem(string name, int count)
        {
            this.Name = name;
            this.Count = count;
        }
    }
}
