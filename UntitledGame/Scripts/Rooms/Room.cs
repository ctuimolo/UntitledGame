using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

using UntitledGame.GameObjects;
using UntitledGame.Dynamics;

namespace UntitledGame
{
    public class Room
    {
        protected Dictionary<string, GameObject> ActiveGameObjects { get; set; }
        
        public WorldHandler World   { get; protected set; }
        public string       Key     { get; protected set; }

        public Room(Point worldSize, string setKey)
        {
            World   = new WorldHandler(worldSize);
            Key     = setKey;
        }

        public Room(WorldHandler sharedWorld, string setKey)
        {
            World   = sharedWorld;
            Key     = setKey;
        }

        public void LoadGameObject(string key, GameObject gameObject)
        {
            if(ActiveGameObjects.ContainsKey(key))
            {
                Console.Error.WriteLine("Room : \"{0}\" : LoadGameObject() : Keyname \"{1}\" already exists", Key,  key);
                Environment.Exit(1);
            }
            gameObject.SetWorld(World);
            ActiveGameObjects[key] = gameObject;
        }

        public virtual void LoadContent()   { }
        public virtual void Update()        { }
        public virtual void Draw()          { }
    }
}
