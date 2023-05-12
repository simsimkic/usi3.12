using HealthcareInstitution.Core.MedicalRecord.DAOs;
using HealthcareInstitution.Core.MedicalRecord.Models;
using HealthcareInstitution.GUI.NurseView.PatientsView;
using HealthcareInstitution.Core.Examination.DAOs;
using HealthcareInstitution.Core.Examination.Models;
using HealthcareInstitution.Core.Users.DAOs;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.GUI.PatientView.AppointmentsView;
using HealthcareInstitution.Users.DAOs;
using HealthcareInstitution.Users.Models;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Users.Controllers
{
    public class PatientController
    {
        private PatientDAO _patients;

        public PatientController()
        {
            _patients = new PatientDAO();
        }
        
        public List<Patient> GetAllPatients()
        {
            return _patients.GetAll();
        }

        public List<PatientsTableItem> GetAllPatientsAndMedicalRecordsByDoctor(string doctorUsername)
        {
            MedicalRecordDAO _medicalRecords = new MedicalRecordDAO();
            List<PatientsTableItem> patentItems = new List<PatientsTableItem>();
            foreach (Patient patient in _patients.GetAll())
            {
                List<AppointmentsTableItem> appointmentItems = new List<AppointmentsTableItem>(GetAppointmentsForPatient(patient.Username));
                MedicalRecord.Models.MedicalRecord? medicalRecord = _medicalRecords.GetById(patient.MedicalRecordId);
                bool patientsDoctor = false;
                foreach (AppointmentsTableItem appointmentsTableItem in appointmentItems)
                {
                    if (appointmentsTableItem.Doctor.Username == doctorUsername)
                    {
                        patientsDoctor = true;
                        break;
                    }
                }
                if (medicalRecord != null && patientsDoctor)
                {
                    PatientsTableItem item = new PatientsTableItem(patient, medicalRecord);
                    patentItems.Add(item);
                }
            }
            return patentItems;
        }

        public List<PatientsTableItem> GetAllPatientsAndMedicalRecords()
        {
            MedicalRecordDAO _medicalRecords = new MedicalRecordDAO();
            List<PatientsTableItem> items = new List<PatientsTableItem>();    
            foreach(Patient patient in _patients.GetAll())
            {
                MedicalRecord.Models.MedicalRecord? medicalRecord = _medicalRecords.GetById(patient.MedicalRecordId);
                if(medicalRecord != null)
                {
                    PatientsTableItem item = new PatientsTableItem(patient, medicalRecord);
                    items.Add(item);
                }
            }
            return items;
        }

        public Patient? GetPatientByUsername(string username)
        {
            return _patients.GetByUsername(username);
        }

        public void BlockPacient(string username)
        {
            _patients.Block(username);
        }

        public List<AppointmentsTableItem> GetAppointmentsForPatient(string patientUsername)
        {
            AppointmentDAO _appointments = new AppointmentDAO();
            DoctorDAO _doctors = new DoctorDAO();
            List<AppointmentsTableItem> items = new List<AppointmentsTableItem>();
            foreach(Appointment appointment in _appointments.GetAll())
            {
                if (appointment.PatientUsername == patientUsername)
                {
                    Doctor? doctor = _doctors.GetByUsername(appointment.DoctorUsername);
                    if (doctor != null)
                    {
                        AppointmentsTableItem item = new AppointmentsTableItem(appointment, doctor);
                        items.Add(item);
                    }
                }
            }
            return items;
        }
        public List<AppointmentsTableItem> GetFilteredAppointmentsForPatient(string patientUsername, string searchText)
        {
            List<AppointmentsTableItem> appointmentsTableItems = GetAppointmentsForPatient(patientUsername);
            foreach (AppointmentsTableItem appointmentsTableItem in appointmentsTableItems.ToList())
            {
                if (!appointmentsTableItem.Appointment.Anamnesis.Contains(searchText))
                {
                    appointmentsTableItems.Remove(appointmentsTableItem);
                }
            }
            return appointmentsTableItems;
        }


        public void Create(Patient patient)
        {
            _patients.Add(patient);
        }
        
        public void Change(Patient patient)
        {
            _patients.Update(patient);
        }
        
        public void Delete(Patient patient)
        {
            _patients.Remove(patient);
        }

        public void Subscribe(IObserver observer)
        {
            _patients.Subscribe(observer);
        }
    }
}