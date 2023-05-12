using HealthcareInstitution.Core.Equipment.Models;
using HealthcareInstitution.Core.Equipment.Storages;
using HealthcareInstitution.Core.Users.Models;
using HealthcareInstitution.Utilities.Enums;
using HealthcareInstitution.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareInstitution.Core.Equipment.DAOs
{
    public class RoomDAO : ISubject
    {
        private readonly List<IObserver> _observers;
        private readonly RoomStorage _storage;
        private readonly List<Room> _rooms;
        private readonly Dictionary<int, Room> _roomsById;

        public RoomDAO()
        {
            _storage = new RoomStorage();
            _rooms = _storage.Load();
            _roomsById = _storage.Load().ToDictionary(r => r.Id, r => r);
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_rooms.Count > 0)
                return _rooms.Max(r => r.Id) + 1;
            return 1;
        }

        public void Add(Room room)
        {
            room.Id = NextId();
            _rooms.Add(room);
            _roomsById.Add(room.Id, room);
            _storage.Save(_rooms);
            NotifyObservers();
        }

        public void Remove(Room room)
        {
            _rooms.Remove(room);
            _roomsById.Remove(room.Id);
            _storage.Save(_rooms);
            NotifyObservers();
        }

        public List<Room> GetAll()
        {
            return _rooms;
        }

        public Dictionary<int, Room> GetAllById()
        {
            return _roomsById;
        }

        public Room? GetById(int id)
        {
            if (_roomsById.ContainsKey(id))
                return _roomsById[id];
            return null;
        }

        public int GetWarehouseId()
        {
            Room? foundWarehouse = _rooms.Find(r => r.Type == RoomType.Warehouse);
            if (foundWarehouse != null)
                return foundWarehouse.Id;
            return -1;
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
