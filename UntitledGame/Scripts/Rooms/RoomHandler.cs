using System.Collections.Generic;
using System;

namespace UntitledGame
{
    public class RoomHandler
    {
        private Dictionary<string, Room> _rooms { get; set; }

        public RoomHandler()
        {
            _rooms = new Dictionary<string, Room>();
        }

        public void AddRoom(Room room)
        {
            if (_rooms.ContainsKey(room.Key))
            {
                Console.Error.WriteLine("RoomHandler : AddRoom() : Keyname \"{1}\" already exists", room.Key);
                Environment.Exit(1);
            }
            _rooms[room.Key] = room;
        }

        public Room GetRoom(string key)
        {
            if (!_rooms.ContainsKey(key))
            {
                Console.Error.WriteLine("RoomHandler : GetRoom() : Keyname \"{1}\" not found in _rooms", key);
                Environment.Exit(1);
            }
            return _rooms[key];
        }
    }
}
