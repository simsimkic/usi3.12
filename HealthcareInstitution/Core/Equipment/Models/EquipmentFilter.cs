using HealthcareInstitution.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.Models
{
    public class EquipmentFilter
    {
        public string SearchText { get; set; }
        public RoomType? RoomType { get; set; }
        public EquipmentType? EquipmentType { get; set; }
        public QuantityRange? QuantityRange { get; set; }
        public bool IsLocationNotWarehouse { get; set; }

        public EquipmentFilter() 
        {
            this.SearchText = string.Empty;
            this.RoomType = null;
            this.EquipmentType = null;
            this.QuantityRange = null;
            this.IsLocationNotWarehouse = false;
        }
        public EquipmentFilter(string searchText, RoomType? roomType, EquipmentType? equipmentType, QuantityRange? quantityRange, bool isLocationNotWarehouse)
        {
            this.SearchText = searchText;
            this.RoomType = roomType;
            this.EquipmentType = equipmentType;
            this.QuantityRange = quantityRange;
            this.IsLocationNotWarehouse = isLocationNotWarehouse;
        }
    }

    public class QuantityRange
    {
        public int LowerBound { get; set; }
        public int UpperBound { get; set; }
        public QuantityRange(int lowerBound, int upperBound) 
        {
            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;
        }
    }
}
