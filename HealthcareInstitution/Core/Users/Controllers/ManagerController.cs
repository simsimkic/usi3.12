using HealthcareInstitution.Users.DAOs;
using HealthcareInstitution.Users.Models;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Users.Controllers
{
    public class ManagerController
    {
        private ManagerDAO _managers;

        public ManagerController()
        {
            _managers = new ManagerDAO();
        }

        public List<Manager> GetAllManagers()
        {
            return _managers.GetAll();
        }

        public void Create(Manager manager)
        {
            _managers.Add(manager);
        }

        public void Delete(Manager manager)
        {
            _managers.Remove(manager);
        }

        public void Subscribe(IObserver observer)
        {
            _managers.Subscribe(observer);
        }
    }
}
