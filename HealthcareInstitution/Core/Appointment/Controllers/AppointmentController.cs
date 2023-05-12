using HealthcareInstitution.Core.Examination.DAOs;
using HealthcareInstitution.Core.Examination.Models;
using HealthcareInstitution.Core.Users.DAOs;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Examination.Controllers
{
    public class AppointmentController
    {
        private AppointmentDAO _appointments;

        public AppointmentController()
        {
            _appointments = new AppointmentDAO();
        }

        public List<Appointment> GetAllAppointments()
        {
            return _appointments.GetAll();
        }
        
        public Dictionary<int, Appointment> GetAllAppointmentsById()
        {
            return _appointments.GetAllById();
        }

        public Appointment? GetAppointmentById(int id)
        {
            return _appointments.GetById(id);
        }

        public void Create(Appointment appointment)
        {
            _appointments.Add(appointment);
        }

        public void Change(Appointment updatedAppointment)
        {
            _appointments.Update(updatedAppointment);
        }

        public void Delete(Appointment appointment)
        {
            _appointments.Remove(appointment);
        }

        public void Subscribe(IObserver observer)
        {
            _appointments.Subscribe(observer);
        }
    }
}
