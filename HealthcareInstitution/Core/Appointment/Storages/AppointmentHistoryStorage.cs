using HealthcareInstitution.Core.Examination.Models;
using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Examination.Storages
{
    public class AppointmentHistoryStorage
    {
        private const string StoragePath = "../../../Data/appointmentsHistory.csv";

        private Serializer<AppointmentHistory> _serializer;

        public AppointmentHistoryStorage()
        {
            _serializer = new Serializer<AppointmentHistory>();
        }

        public List<AppointmentHistory> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<AppointmentHistory> appointmentsHistory)
        {
            _serializer.ToCSV(StoragePath, appointmentsHistory);
        }
    }
}
