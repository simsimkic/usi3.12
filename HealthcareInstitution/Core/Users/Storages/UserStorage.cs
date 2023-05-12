using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Users.Models;
using HealthcareInstitution.Utilities.Enums;
using HealthcareInstitution.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Users.Storages
{
    public class UserStorage
    {
        private const string ManagersStoragePath = "../../../Data/managers.csv";
        private const string DoctorsStoragePath = "../../../Data/doctors.csv";
        private const string PatientsStoragePath = "../../../Data/patients.csv";
        private const string NursesStoragePath = "../../../Data/nurses.csv";

        private Serializer<User> _serializer;

        public UserStorage()
        {
            _serializer = new Serializer<User>();
        }

        public List<User> Load()
        {
            List<User> users = new List<User>();
            users.AddRange(_serializer.FromCSV(ManagersStoragePath));
            users.AddRange(_serializer.FromCSV(DoctorsStoragePath));
            users.AddRange(_serializer.FromCSV(PatientsStoragePath));
            users.AddRange(_serializer.FromCSV(NursesStoragePath));
            return users;
        }

        public void Save(List<User> users)
        {
            int lastUserIndex = 0;
            User lastUser = users[0];
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Type != lastUser.Type) 
                {
                    switch (lastUser.Type)
                    {
                        case UserType.Manager:
                            {
                                _serializer.ToCSV(ManagersStoragePath, users.GetRange(lastUserIndex, i - lastUserIndex));
                                lastUserIndex = i;
                                lastUser = users[i];
                                break;
                            }
                        case UserType.Doctor:
                            {
                                _serializer.ToCSV(DoctorsStoragePath, users.GetRange(lastUserIndex, i - lastUserIndex));
                                lastUserIndex = i;
                                lastUser = users[i];
                                break;
                            }
                        case UserType.Patient:
                            {
                                _serializer.ToCSV(PatientsStoragePath, users.GetRange(lastUserIndex, i - lastUserIndex));
                                lastUserIndex = i;
                                lastUser = users[i];
                                break;
                            }
                        case UserType.Nurse:
                            {
                                _serializer.ToCSV(NursesStoragePath, users.GetRange(lastUserIndex, i - lastUserIndex));
                                lastUserIndex = i;
                                lastUser = users[i];
                                break;
                            }
                    }
                }
            }
        }
    }
}
