using HealthcareInstitution.Core.Equipment.DAOs;
using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.Controllers
{
    public class EquipmentArrangementController
    {
        private readonly EquipmentArrangementDAO _equipmentArrangements;

        public EquipmentArrangementController()
        {
            _equipmentArrangements = new EquipmentArrangementDAO();
        }

        public List<EquipmentArrangement> GetAllEquipmentArrangements()
        {
            return _equipmentArrangements.GetAll();
        }

        public void Create(EquipmentArrangement equipmentArrangement)
        {
            _equipmentArrangements.Add(equipmentArrangement);
        }

        public void Delete(EquipmentArrangement equipmentArrangement)
        {
            _equipmentArrangements.Remove(equipmentArrangement);
        }

        public void Subscribe(IObserver observer)
        {
            _equipmentArrangements.Subscribe(observer);
        }
    }
}
