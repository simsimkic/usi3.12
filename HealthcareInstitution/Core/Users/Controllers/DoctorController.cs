using HealthcareInstitution.Core.Examination.DAOs;
using HealthcareInstitution.Core.Examination.Models;
using HealthcareInstitution.Core.Users.DAOs;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.GUI.DoctorView.AppontmentsView;
using HealthcareInstitution.GUI.PatientView.AppointmentsView;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Users.Controllers
{
    public class DoctorController
    {
        private DoctorDAO _doctors;

        public DoctorController()
        {
            _doctors = new DoctorDAO();
        }

        public List<Doctor> GetAllDoctors()
        {
            return _doctors.GetAll();
        }

        public List<DoctorAppointmentsTableItem> GetDoctorAppointmentsForDoctor(string doctorUsername)
        {
            AppointmentDAO _appointments = new AppointmentDAO();
            PatientDAO _patients = new PatientDAO();
            List<DoctorAppointmentsTableItem> items = new List<DoctorAppointmentsTableItem>();
            foreach (Appointment appointment in _appointments.GetAll())
            {
                if (appointment.DoctorUsername == doctorUsername)
                {
                    Patient? patient = _patients.GetByUsername(appointment.PatientUsername);
                    if (patient != null)
                    {
                        DoctorAppointmentsTableItem item = new DoctorAppointmentsTableItem(appointment, patient);
                        items.Add(item);
                    }
                }
            }
            return items;
        }

        public List<Appointment> GetAppointmentsForDoctor(string doctorUsername)
        {
            AppointmentDAO _appointments = new AppointmentDAO();
            List<Appointment> items = new List<Appointment>();
            foreach (Appointment appointment in _appointments.GetAll())
            {
                if (appointment.DoctorUsername == doctorUsername)
                    items.Add(appointment);
            }
            return items;
        }

        public void Create(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public void Delete(Doctor doctor)
        {
            _doctors.Remove(doctor);
        }

        public void Subscribe(IObserver observer)
        {
            _doctors.Subscribe(observer);
        }
    }
}
