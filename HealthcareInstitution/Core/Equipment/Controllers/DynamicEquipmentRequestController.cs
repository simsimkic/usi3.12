using HealthcareInstitution.Core.Equipment.DAOs;
using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.GUI.ManagerView.EquipmentView;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.Controllers
{
    public class DynamicEquipmentRequestController
    {
        private readonly DynamicEquipmentRequestDAO _dynamicEquipmentRequests;

        public DynamicEquipmentRequestController()
        {
            _dynamicEquipmentRequests = new DynamicEquipmentRequestDAO();
        }

        public List<DynamicEquipmentRequest> GetAllDynamicEquipmentRequests()
        {
            return _dynamicEquipmentRequests.GetAll();
        }

        public void Create(DynamicEquipmentRequest dynamicEquipmentRequest)
        {
            _dynamicEquipmentRequests.Add(dynamicEquipmentRequest);
        }

        public void Delete(DynamicEquipmentRequest dynamicEquipmentRequest)
        {
            _dynamicEquipmentRequests.Remove(dynamicEquipmentRequest);
        }

        public void Subscribe(IObserver observer)
        {
            _dynamicEquipmentRequests.Subscribe(observer);
        }
    }
}
