using Microsoft.Xna.Framework;

using UntitledGame.Dynamics;

namespace UntitledGame
{
    public class Room
    {
        public WorldHandler World { get; protected set; }

        public Room(Point worldSize)
        {
            World = new WorldHandler(worldSize);
        }

        public Room(WorldHandler sharedWorld)
        {
            World = sharedWorld;
        }

        public virtual void LoadContent()   { }
        public virtual void Update()        { }
        public virtual void Draw()          { }
    }
}
