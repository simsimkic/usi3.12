using HealthcareInstitution.Core.Users.DAOs;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Users.DAOs;
using HealthcareInstitution.Users.Models;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Users.Controllers
{
    public class NurseController
    {
        private NurseDAO _nurses;

        public NurseController()
        {
            _nurses = new NurseDAO();
        }

        public List<Nurse> GetAllNurses()
        {
            return _nurses.GetAll();
        }

        public void Create(Nurse nurse)
        {
            _nurses.Add(nurse);
        }

        public void Delete(Nurse nurse)
        {
            _nurses.Remove(nurse);
        }

        public void Subscribe(IObserver observer)
        {
            _nurses.Subscribe(observer);
        }
    }
}
