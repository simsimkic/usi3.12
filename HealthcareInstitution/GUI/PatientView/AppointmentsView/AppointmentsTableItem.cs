using HealthcareInstitution.Core.Examination.Models;
using HealthcareInstitution.Core.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.GUI.PatientView.AppointmentsView
{
    public class AppointmentsTableItem
    {
        public Appointment Appointment { get; set; }

        public Doctor Doctor { get; set; }

        public AppointmentsTableItem()
        {
            Appointment = new Appointment();
            Doctor = new Doctor();
        }

        public AppointmentsTableItem(Appointment appointment, Doctor doctor)
        {
            this.Appointment = appointment;
            this.Doctor = doctor;
        }
    }
}
