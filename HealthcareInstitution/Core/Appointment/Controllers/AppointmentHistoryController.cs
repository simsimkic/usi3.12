using HealthcareInstitution.Core.Examination.DAOs;
using HealthcareInstitution.Core.Examination.Models;
using HealthcareInstitution.Core.Users.Controllers;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Examination.Controllers
{
    public class AppointmentHistoryController
    {
        private AppointmentHistoryDAO _appointmentsHistory;

        public AppointmentHistoryController()
        {
            _appointmentsHistory = new AppointmentHistoryDAO();
        }

        public List<AppointmentHistory> GetAllAppointmentsHistory()
        {
            return _appointmentsHistory.GetAll();
        }

        public Dictionary<int, AppointmentHistory> GetAllAppointmentsById()
        {
            return _appointmentsHistory.GetAllById();
        }

        public AppointmentHistory? GetAppointmentById(int id)
        {
            return _appointmentsHistory.GetById(id);
        }

        public bool CheckToBlockPatient(string username)
        {
            PatientController patientController = new PatientController();
            Patient? patient = patientController.GetPatientByUsername(username);
            if (patient != null)
            {
                int createdInLast30Days = 0;
                int changedInLast30Days = 0;

                foreach (AppointmentHistory appointmentHistory in _appointmentsHistory.GetAll())
                {
                    if (appointmentHistory.PatientUsername == patient.Username && appointmentHistory.LastChangeDatetime.AddDays(30) > DateTime.Now)
                    {
                        switch (appointmentHistory.Status)
                        {
                            case Utilities.Enums.AppointmentStatus.Created:
                                {
                                    createdInLast30Days++;
                                    break;
                                }
                            case Utilities.Enums.AppointmentStatus.Changed:
                                {
                                    changedInLast30Days++;
                                    break;
                                }
                            case Utilities.Enums.AppointmentStatus.Deleted:
                                {
                                    changedInLast30Days++;
                                    break;
                                }
                        }
                    }
                }

                if (createdInLast30Days >= 8 || changedInLast30Days >= 5)
                {
                    patientController.BlockPacient(patient.Username);
                    return true;
                }
            }
            return false;
        }

        public void Create(AppointmentHistory appointmentHistory)
        {
            _appointmentsHistory.Add(appointmentHistory);
        }

        public void Delete(AppointmentHistory appointmentHistory)
        {
            _appointmentsHistory.Remove(appointmentHistory);
        }

        public void Subscribe(IObserver observer)
        {
            _appointmentsHistory.Subscribe(observer);
        }
    }
}
