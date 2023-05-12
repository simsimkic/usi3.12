using HealthcareInstitution.Core.Users.Controllers;
using HealthcareInstitution.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace HealthcareInstitution.Core.Users.Models
{
    public class Patient : User, Utilities.Serializer.ISerializable, IDataErrorInfo
    {
        public int MedicalRecordId { get; set; }
        public bool IsBlocked { get; set; }

        public Patient() : base() 
        {
            this.MedicalRecordId = -1;
            this.IsBlocked = false;
        }

        public Patient(string username, string password, string name, string surname, int medicalRecordId, bool isBlocked) : base(UserType.Patient, username, password, name, surname)
        {
            this.MedicalRecordId = medicalRecordId;
            this.IsBlocked = isBlocked;
        }

        public override string[] ToCSV()
        {
            string[] csvValues = { this.Type.ToString(), this.Username, this.Password, this.Name, this.Surname, this.MedicalRecordId.ToString(), this.IsBlocked.ToString() };
            return csvValues;
        }

        public override void FromCSV(string[] values)
        {
            this.Username = values[1];
            this.Password = values[2];
            this.Name = values[3];
            this.Surname = values[4];
            this.MedicalRecordId = int.Parse(values[5]);
            this.IsBlocked = bool.Parse(values[6]);
        }

        public override string ToString()
        {
            return $"{this.Type} {this.Username} {this.Password} {this.Name} {this.Surname} {this.MedicalRecordId} {this.IsBlocked}";
        }
        
        private Regex _nameRegex = new Regex("[A-Z]");
        private Regex _surnameRegex = new Regex("[A-Z]");

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Username")
                {
                    if (string.IsNullOrEmpty(this.Username))
                        return "Username is required";
                    if (this.Username.Length < 2 || this.Username.Length > 20)
                        return "Username has to be between 2 and 20 characters long";

                    UserController userController = new UserController();
                    if (userController.GetUserByUsername(this.Username) != null)
                    {
                        return "Username already exists";
                    }

                }
                else if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(this.Name))
                        return "Name is required";
                    if (this.Name.Length < 2 || this.Name.Length > 20)
                        return "Name has to be between 2 and 20 characters long";

                }
                else if (columnName == "Password")
                {
                    if (string.IsNullOrEmpty(this.Password))
                        return "Password is required";
                    if (this.Password.Length < 2 || this.Password.Length > 20)
                        return "Password has to be between 2 and 20 characters long";

                }
                else if (columnName == "Surname")
                {
                    if (string.IsNullOrEmpty(this.Surname))
                        return "Surname is required";
                    if (this.Surname.Length < 2 || this.Surname.Length > 20)
                        return "Surname has to be between 2 and 20 characters long";

                }

                return null;
            }
        }



        private readonly string[] _validatedProperties = { "Username", "Name", "Surname", "Password" };

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
        
        public bool IsValidWithouUsername
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (property.Equals("Username"))
                        continue;
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }
    }
}