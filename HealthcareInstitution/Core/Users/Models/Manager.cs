using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Utilities.Enums;
using HealthcareInstitution.Utilities.Serializer;

namespace HealthcareInstitution.Users.Models
{
    public class Manager : User, Utilities.Serializer.ISerializable
    {
        public Manager() : base()
        {
        }

        public Manager(string username, string password, string name, string surname) : base(UserType.Manager, username, password, name, surname)
        {
        }

        public override string[] ToCSV()
        {
            string[] csvValues = { this.Type.ToString(), this.Username, this.Password, this.Name, this.Surname };
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
            return $"{this.Type} {this.Username} {this.Password} {this.Name} {this.Surname}";
        }
    }
}
