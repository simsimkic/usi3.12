using HealthcareInstitution.Core.MedicalRecord.Models;
using HealthcareInstitution.Core.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.GUI.NurseView.PatientsView
{
    public class PatientsTableItem
    {

        public Patient Patient { get; set; }

        public MedicalRecord MedicalRecord { get; set; }

        public PatientsTableItem(Patient patient, MedicalRecord medicalRecord)
        {
            this.Patient = patient;
            this.MedicalRecord = medicalRecord;
        }

    }
}
