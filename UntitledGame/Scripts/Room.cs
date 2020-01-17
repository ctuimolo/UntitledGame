using Microsoft.Xna.Framework;
using System.Collections.Generic;

using UntitledGame.Dynamics;

namespace UntitledGame
{
    public class Room
    {
        protected List<GameObject> ActiveGameObjects { get; set; }
        
        public WorldHandler World { get; protected set; }

        public Room(Point worldSize)
        {
            World = new WorldHandler(worldSize);
        }

        public Room(WorldHandler sharedWorld)
        {
            World = sharedWorld;
        }

        public void SpawnGameObject(GameObject gameObject)
        {
            gameObject.SetWorld(World);
            ActiveGameObjects.Add(gameObject);
        }

        public virtual void LoadContent()   { }
        public virtual void Update()        { }
        public virtual void Draw()          { }
    }
}
