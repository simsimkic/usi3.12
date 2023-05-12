using HealthcareInstitution.Core.Users.Controllers;
using HealthcareInstitution.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Users.Models
{
    public class User : Utilities.Serializer.ISerializable
    {
        public UserType Type { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public User()
        {
            this.Type = UserType.Patient;
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.Name = string.Empty;
            this.Surname = string.Empty;
        }

        public User(UserType type, string username, string password, string name, string surname)
        {
            this.Type = type;
            this.Username = username;
            this.Password = password;
            this.Name = name;
            this.Surname = surname;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Type.ToString(), this.Username, this.Password, this.Name, this.Surname };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Type = Enum.Parse<UserType>(values[0], true);
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
