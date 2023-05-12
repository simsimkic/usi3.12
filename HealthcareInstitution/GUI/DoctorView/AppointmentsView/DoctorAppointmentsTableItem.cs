using HealthcareInstitution.Core.Examination.Models;
using HealthcareInstitution.Core.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.GUI.DoctorView.AppontmentsView
{
    public class DoctorAppointmentsTableItem
    {

        public Appointment Appointment { get; set; }

        public Patient Patient { get; set; }

        public DoctorAppointmentsTableItem()
        {
            Appointment = new Appointment();
            Patient = new Patient();
        }

        public DoctorAppointmentsTableItem(Appointment appointment, Patient patient)
        {
            this.Appointment = appointment;
            this.Patient = patient;
        }
    }
}
