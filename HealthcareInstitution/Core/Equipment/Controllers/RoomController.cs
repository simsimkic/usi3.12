using HealthcareInstitution.Core.Equipment.DAOs;
using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.Controllers
{
    public class RoomController
    {
        private readonly RoomDAO _rooms;

        public RoomController()
        {
            _rooms = new RoomDAO();
        }

        public List<Room> GetAllRooms()
        {
            return _rooms.GetAll();
        }

        public Room? GetRoomById(int id)
        {
            return _rooms.GetById(id);
        }

        public void Create(Room room)
        {
            _rooms.Add(room);
        }

        public void Delete(Room room)
        {
            _rooms.Remove(room);
        }

        public void Subscribe(IObserver observer)
        {
            _rooms.Subscribe(observer);
        }
    }
}
