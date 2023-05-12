using HealthcareInstitution.Core.Examination.Models;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Examination.Storages
{
  
    public class AppointmentStorage
    {
        private const string StoragePath = "../../../Data/appointments.csv";

        private Serializer<Appointment> _serializer;

        public AppointmentStorage()
        {
            _serializer = new Serializer<Appointment>();
        }

        public List<Appointment> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<Appointment> appointments)
        {
            _serializer.ToCSV(StoragePath, appointments);
        }
    }
}
