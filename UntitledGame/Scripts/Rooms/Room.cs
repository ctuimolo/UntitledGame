using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

using UntitledGame.GameObjects;
using UntitledGame.Dynamics;

namespace UntitledGame
{
    public class Room
    {
        protected Dictionary<string, GameObject> CachedGameObjects { get; set; }
        protected List<GameObject> ActiveGameObjects;
        
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

        public void LoadGameObject(GameObject gameObject)
        {
            if(CachedGameObjects.ContainsKey(gameObject.Key))
            {
                Console.Error.WriteLine("Room : \"{0}\" : LoadGameObject() : Keyname \"{1}\" already exists", Key, gameObject.Key);
                Environment.Exit(1);
            }
            CachedGameObjects[gameObject.Key] = gameObject;
        }

        public virtual void LoadContent()    { }
        public virtual void InitializeRoom() { }
        public virtual void Update()         { }
        public virtual void Draw()           { }
    }
}
