using HealthcareInstitution.Core.Examination.Models;
using HealthcareInstitution.Core.Examination.Storages;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Core.Users.Storages;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Examination.DAOs
{
    public class AppointmentDAO : ISubject
    {
        private List<IObserver> _observers;
        private AppointmentStorage _storage;
        private List<Appointment> _appointments;
        private Dictionary<int, Appointment> _appointmentsById;

        public AppointmentDAO()
        {
            _storage = new AppointmentStorage();
            _appointments = _storage.Load();
            _appointmentsById = _storage.Load().ToDictionary(a => a.Id, a => a);
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            return _appointments.Max(a => a.Id) + 1;
        }

        public void Add(Appointment appointment)
        {
            appointment.Id = NextId();
            _appointments.Add(appointment);
            _appointmentsById.Add(appointment.Id, appointment);
            _storage.Save(_appointments);
            NotifyObservers();
        }

        public void Remove(Appointment appointment)
        {
            _appointments.Remove(appointment);
            _appointmentsById.Remove(appointment.Id);
            _storage.Save(_appointments);
            NotifyObservers();
        }

        public void Update(Appointment updatedAppointment)
        {
            Appointment? appointment = _appointments.Find(a => a.Id == updatedAppointment.Id);
            if (appointment != null)
            {
                appointment.Date = updatedAppointment.Date;
                appointment.Time = updatedAppointment.Time;
                appointment.DoctorUsername = updatedAppointment.DoctorUsername;
                appointment.Anamnesis = updatedAppointment.Anamnesis;
                appointment.IsFinished = updatedAppointment.IsFinished;
                _appointmentsById[appointment.Id] = appointment;
                _storage.Save(_appointments);
                NotifyObservers();
            }
        }

        public List<Appointment> GetAll()
        {
            return _appointments;
        }
        
        public Dictionary<int, Appointment> GetAllById()
        {
            return _appointmentsById;
        }

        public Appointment? GetById(int id)
        {
            if (_appointmentsById.ContainsKey(id))
                return _appointmentsById[id];
            return null;
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
