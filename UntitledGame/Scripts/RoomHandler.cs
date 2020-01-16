using System.Collections.Generic;

namespace UntitledGame
{
    public class RoomHandler
    {
        public Dictionary<string, Room> Hash { get; private set; }

        public RoomHandler()
        {
            Hash = new Dictionary<string, Room>();
        }
    }
}
