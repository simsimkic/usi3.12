using HealthcareInstitution.Utilities.Enums;
using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Examination.Models
{
    public class AppointmentHistory : ISerializable
    {
        public const int Duration = 15;

        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string PatientUsername { get; set; }
        public AppointmentStatus Status { get; set; }
        public DateTime LastChangeDatetime { get; set; }

        public AppointmentHistory()
        {
            this.Id = -1;
            this.AppointmentId = -1;
            this.PatientUsername = string.Empty;
            this.Status = AppointmentStatus.Created;
            this.LastChangeDatetime = DateTime.Now;
        }

        public AppointmentHistory(int id, int appointmentId, string patientUsername, AppointmentStatus status, DateTime lastChangeDatetime)
        {
            this.Id = id;
            this.AppointmentId = appointmentId;
            this.PatientUsername = patientUsername;
            this.Status = status;
            this.LastChangeDatetime = lastChangeDatetime;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.AppointmentId.ToString(), this.PatientUsername, this.Status.ToString(), this.LastChangeDatetime.ToString("MM/dd/yyyy hh:mm") };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.AppointmentId = int.Parse(values[1]);
            this.PatientUsername = values[2];
            this.Status = Enum.Parse<AppointmentStatus>(values[3]);
            this.LastChangeDatetime = DateTime.ParseExact(values[4], "MM/dd/yyyy hh:mm", CultureInfo.InvariantCulture);
        }

        public override string ToString()
        {
            return $"{this.Id} {this.AppointmentId} {this.PatientUsername} {this.Status} {this.LastChangeDatetime}";
        }
    }
}
