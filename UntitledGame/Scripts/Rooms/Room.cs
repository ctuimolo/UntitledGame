using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

using PhysicsWorld.Responses;

using UntitledGame.GameObjects;
using UntitledGame.Dynamics;

namespace UntitledGame.Rooms
{
    public class Room
    {
        protected bool _drawDebug = false;

        protected Dictionary<string, GameObject> CachedGameObjects { get; set; }
        protected List<GameObject> ActiveGameObjects;
        protected List<GameObject> DrawableGameObjects;
        
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
            gameObject.LoadContent();
        }

        // Spawns a cached object, moves into active obj list and update loop. Physics bodies reset.
        public void Instantiate(string key)
        {
            if (!CachedGameObjects.ContainsKey(key))
            {
                Console.Error.WriteLine("Room : \"{0}\" : Instantiate() : Keyname \"{1}\" not found in loaded objects", Key, key);
                return;
            }
            ActiveGameObjects.Add(CachedGameObjects[key]);
            if(CachedGameObjects[key].Drawable)
            {
                DrawableGameObjects.Add(CachedGameObjects[key]);
            }
            if (CachedGameObjects[key].Body != null)
            {
                CachedGameObjects[key].Body.BoxCollider.Move(CachedGameObjects[key].InitPosition.X, CachedGameObjects[key].InitPosition.Y, (collision) => CollisionResponses.None);
            }
        }

        // Remove a cached object from update loop, does not deallocate. Physics bodies crushed and moved to (-1,-1)
        public void Remove(string key)
        {
            if (!CachedGameObjects.ContainsKey(key))
            {
                Console.Error.WriteLine("Room : \"{0}\" : Remove() : Keyname \"{1}\" not found in loaded objects", Key, key);
                return;
            }
            ActiveGameObjects.Remove(CachedGameObjects[key]);
            DrawableGameObjects.Remove(CachedGameObjects[key]);
            if(CachedGameObjects[key].Body != null)
            {
                CachedGameObjects[key].Body.BoxCollider.Move(CachedGameObjects[key].Size.X * -1, CachedGameObjects[key].Size.Y * -1, (collision) => CollisionResponses.None);
            }
        }

        public void Destruct(string key)
        {
            if (!CachedGameObjects.ContainsKey(key))
            {
                Console.Error.WriteLine("Room : \"{0}\" : Destruct() : Keyname \"{1}\" not found in loaded objects", Key, key);
                return;
            }
            ActiveGameObjects.Remove(CachedGameObjects[key]);
            DrawableGameObjects.Remove(CachedGameObjects[key]);
            CachedGameObjects[key].Destruct();
            CachedGameObjects.Remove(key);
        }

        public virtual void LoadContent()    { }
        public virtual void InitializeRoom() { }

        public virtual  void Update()
        {
            // Physics world step, and then resolve collisions
            // Send to collisions, interacting objects
            World.PhysicsStep();

            // update every game object
            foreach (GameObject obj in ActiveGameObjects)
            {
                obj.Update();
            }

            // late update/mostly enact actions
            foreach (GameObject obj in ActiveGameObjects)
            {
                obj.LateUpdate();
            }
        }

        public virtual void Draw()
        {
            foreach (GameObject obj in DrawableGameObjects)
            {
                obj.Draw();
                if (_drawDebug)
                {
                    obj.DrawDebug();
                }
            }

            if (_drawDebug)
            {
                World.DrawDebug();
            }
        }
    }
}
