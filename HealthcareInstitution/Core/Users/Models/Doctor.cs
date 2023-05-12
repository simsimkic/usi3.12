using HealthcareInstitution.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Users.Models
{
    public class Doctor : User, Utilities.Serializer.ISerializable
    {
        public Specialization Specialization { get; set; }
        public Doctor() : base()
        {
            this.Specialization = Specialization.General;
        }

        public Doctor(string username, string password, string name, string surname, Specialization specialization) : base(UserType.Doctor, username, password, name, surname)
        {
            this.Specialization = specialization;
        }

        public override string[] ToCSV()
        {
            string[] csvValues = { this.Type.ToString(), this.Username, this.Password, this.Name, this.Surname ,this.Specialization.ToString()};
            return csvValues;
        }

        public override void FromCSV(string[] values)
        {
            this.Username = values[1];
            this.Password = values[2];
            this.Name = values[3];
            this.Surname = values[4];
            this.Specialization = Enum.Parse<Specialization>(values[5],true);
        }

        public override string ToString()
        {
            return $"{this.Type} {this.Username} {this.Password} {this.Name} {this.Surname} {this.Specialization}";
        }
    }
}
