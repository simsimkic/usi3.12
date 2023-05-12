using HealthcareInstitution.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Users.Models
{
    public class Nurse: User, Utilities.Serializer.ISerializable
    {
        public Nurse() : base()
        {
        }

        public Nurse(string username, string password, string name, string surname) : base(UserType.Nurse, username, password, name, surname)
        {
        }

        public override string[] ToCSV()
        {
            string[] csvValues = { this.Username, this.Password, this.Name, this.Surname };
            return csvValues;
        }

        public override void FromCSV(string[] values)
        {
            this.Username = values[1];
            this.Password = values[2];
            this.Name = values[3];
            this.Surname = values[4];
        }

        public override string ToString()
        {
            return $"{this.Username} {this.Password} {this.Name} {this.Surname}";
        }


    }
}
