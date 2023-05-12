using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Utilities.Enums;
using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace HealthcareInstitution.Core.Examination.Models
{
    public class Appointment : ISerializable, IDataErrorInfo
    {
        public const int Duration = 15;

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string PatientUsername { get; set; }
        public string DoctorUsername { get; set; }
        public string Anamnesis { get; set; }
        public bool IsFinished { get; set; }

        public Appointment()
        {
            this.Id = -1;
            this.Date = DateTime.Now;
            this.Time = string.Empty;
            this.PatientUsername = string.Empty;
            this.DoctorUsername = string.Empty;
            this.Anamnesis = string.Empty;
            this.IsFinished = false;
        }

        public Appointment(int id, DateTime date, string time, string patientUsername, string doctorUsername, string anamnesis, bool isFinished)
        {
            this.Id = id;
            this.Date = date;
            this.Time = time;
            this.PatientUsername = patientUsername;
            this.DoctorUsername = doctorUsername;
            this.Anamnesis = anamnesis;
            this.IsFinished = isFinished;
        }

        public DateTime GetDateTime()
        {
            DateTime dateTime = Date;
            
            dateTime = dateTime.AddHours(int.Parse(Time.Split(":")[0]));
            dateTime = dateTime.AddMinutes(int.Parse(Time.Split(":")[1]));
            return dateTime;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.Date.ToString("MM/dd/yyyy"), this.Time, this.PatientUsername, this.DoctorUsername, this.Anamnesis, this.IsFinished.ToString() };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.Date = DateTime.ParseExact(values[1], "MM/dd/yyyy", CultureInfo.InvariantCulture);
            this.Time = values[2];
            this.PatientUsername = values[3];
            this.DoctorUsername = values[4];
            this.Anamnesis = values[5];
            this.IsFinished = bool.Parse(values[6]);
        }

        public override string ToString()
        {
            return $"{this.Id} {this.Date} {this.Time} {this.PatientUsername} {this.DoctorUsername} {this.Anamnesis} {this.IsFinished}";
        }

        private Regex _timeRegex = new Regex("^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$");

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Date")
                {
                    if (Date < DateTime.Now)
                        return "You cannot choose date in the past!";

                    //DateTime dateTime = GetDateTime();
                }
                else if (columnName == "Time")
                {
                    if (string.IsNullOrEmpty(Time))
                        return "Time is required";

                    Match match = _timeRegex.Match(Time);
                    if (!match.Success)
                        return "Time should be in format: hh:mm";

                    DateTime dateTime = GetDateTime();
                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Date", "Time" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }
    }
}
