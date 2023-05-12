using HealthcareInstitution.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace HealthcareInstitution.Core.MedicalRecord.Models
{
    public class MedicalRecord:Utilities.Serializer.ISerializable, IDataErrorInfo
    {
        public int Id { get; set; }
        public int Weight { get; set; } 
        public int Height { get; set; }
        public string MedicalHistory { get; set; }


        public MedicalRecord()
        {
            this.Id = -1;
            this.Weight = 0;
            this.Height = 0;
            this.MedicalHistory = string.Empty;
        }

        public MedicalRecord(int id, int weight, int height, string medicalHistory)
        {
            this.Id = id;
            this.MedicalHistory = medicalHistory;
            this.Weight = weight;
            this.Height = height;
        }

        public  string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.Weight.ToString(), this.Height.ToString(), this.MedicalHistory };
            return csvValues;
        }

        public  void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.Weight = int.Parse(values[1]);
            this.Height = int.Parse(values[2]);
            this.MedicalHistory = values[3];   
        }

        public override string ToString()
        {
            return $"{this.Id} {this.Weight} {this.Height} {this.MedicalHistory}";
        }
        private Regex _heightRegex = new Regex("[0-9]{1,3}");
        private Regex _weightRegex = new Regex("[0-9]{1,3}");

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Weight")
                {
                    if (string.IsNullOrEmpty(this.Weight.ToString()))
                        return "Weight is required";
                    //Match match = _weightRegex.Match(this.Weight.ToString());
                    //if (!match.Success)
                    //    return "Weight has to be an int";
                    //if (!int.TryParse(this.Weight.ToString().Trim(), out int index))
                    //{
                    //    return "Weight has to be an int";
                    //}
                    if (this.Weight < 1)
                    {
                        return "Weight can't be less than 1";
                    }
                    if (this.Weight > 450)
                    {
                        return "Weight can't be more than 450";
                    }
                }
                else if (columnName == "Height")
                {
                    if (string.IsNullOrEmpty(this.Height.ToString()))
                        return "Height is required";
                    //Match match = _heightRegex.Match(this.Height.ToString());
                    //if (!match.Success)
                    //    return "Height has to be an int";
                   
                    if(this.Height < 1)
                    {
                        return "Height cant be less than 1";
                    }
                    if (this.Height > 300)
                    {
                        return "Height can't be more than 300";
                    }
                }
                else if (columnName == "MedicalHistory")
                {
                    if (string.IsNullOrEmpty(this.MedicalHistory))
                        return "Medical History is required";
                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Weight", "Height", "MedicalHistory" };

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
