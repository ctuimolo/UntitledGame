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

        public Dictionary<string, GameObject> CachedGameObjects { get; protected set; }
        public List<GameObject> ActiveGameObjects       { get; protected set; }
        public List<GameObject> DrawableGameObjects     { get; protected set; }
        public List<string>     AcitvateQueue           { get; protected set; }
        public List<string>     DeactivateQueue         { get; protected set; }
        public List<string>     DestructQueue           { get; protected set; }

        public WorldHandler World   { get; protected set; }
        public string       Key     { get; protected set; }

        public Room(Point worldSize, string setKey)
        {
            World   = new WorldHandler(worldSize);
            Key     = setKey;
            World.OwnerRoom = this;

            CachedGameObjects   = new Dictionary<string, GameObject>();
            ActiveGameObjects   = new List<GameObject>();
            DrawableGameObjects = new List<GameObject>();
            AcitvateQueue       = new List<string>();
            DeactivateQueue     = new List<string>();
            DestructQueue       = new List<string>();
        }

        public Room(WorldHandler sharedWorld, string setKey)
        {
            World   = sharedWorld;
            Key     = setKey;
        }

        // public function for queing a game object to be loaded at the end of this update loop
        public void QueueGameObject(GameObject gameObject)
        {
            LoadGameObject(gameObject);
            AcitvateQueue.Add(gameObject.Key);
        }

        // cache an object into memory
        protected void LoadGameObject(GameObject gameObject)
        {
            if(CachedGameObjects.ContainsKey(gameObject.Key))
            {
                Console.Error.WriteLine("Room : \"{0}\" : LoadGameObject() : Keyname \"{1}\" already exists", Key, gameObject.Key);
                Environment.Exit(1);
            }
            CachedGameObjects[gameObject.Key] = gameObject;
            gameObject.CurrentRoom  = this;
            gameObject.CurrentWorld = World;
            gameObject.LoadContent();
        }

        // Activate all the queued objects
        protected void RunActivateQueue()
        {
            foreach (string key in AcitvateQueue)
            {
                Activate(key);
            }
            AcitvateQueue.Clear();
        }

        // Deactivate all the queued objects
        protected void RunDeactivateQueue()
        {
            foreach (string key in DeactivateQueue)
            {
                Deactivate(key);
            }
            DeactivateQueue.Clear();
        }

        // Destruct all the queued objects
        protected void RunDestructQueue()
        {
            foreach (string key in DestructQueue)
            {
                Destruct(key);
            }
            DestructQueue.Clear();
        }

        // Spawns a cached object, moves into active obj list and update loop. Physics bodies reset.
        protected void Activate(string key)
        {
            GameObject gameObject;
            if (!CachedGameObjects.ContainsKey(key))
            {
                Console.Error.WriteLine("Room : \"{0}\" : Activate() : Keyname \"{1}\" not found in loaded objects", Key, key);
                return;
            }
            gameObject = CachedGameObjects[key];
            if (!gameObject.Active)
            {
                ActiveGameObjects.Add(gameObject);
                gameObject.PreActivate();
                if (gameObject.Drawable)
                {
                    DrawableGameObjects.Add(gameObject);
                }
                if (gameObject.Body != null)
                {
                    gameObject.Body.BoxCollider.Data = gameObject.Body.Category;
                    foreach (Hitbox childHitbox in gameObject.Body.ChildHitboxes.Values)
                    {
                        World.AddHitbox(childHitbox);
                    }
                }
            }
        }

        // Remove a cached object from update loop, does not deallocate. Physics bodies crushed and moved to (-1,-1)
        protected void Deactivate(string key)
        {
            GameObject gameObject;
            if (!CachedGameObjects.ContainsKey(key))
            {
                Console.Error.WriteLine("Room : \"{0}\" : Remove() : Keyname \"{1}\" not found in loaded objects", Key, key);
                return;
            }
            gameObject = CachedGameObjects[key];
            if (gameObject.Active)
            {
                ActiveGameObjects.Remove(gameObject);
                gameObject.PreDeactivate();
                if (gameObject.Drawable)
                {
                    DrawableGameObjects.Remove(gameObject);
                }
                if (gameObject.Body != null)
                {
                    gameObject.Body.BoxCollider.Data = CollisionCategory.none;
                    foreach(Hitbox hitbox in gameObject.Body.ChildHitboxes.Values)
                    {
                        World.RemoveHitbox(hitbox);
                    }
                }
            }
        }

        protected void Destruct(string key)
        {
            GameObject gameObject;
            if (!CachedGameObjects.ContainsKey(key))
            {
                Console.Error.WriteLine("Room : \"{0}\" : Destruct() : Keyname \"{1}\" not found in loaded objects", Key, key);
                return;
            }
            gameObject = CachedGameObjects[key];
            if(gameObject.Active)
            {
                Deactivate(key);
            }
            gameObject.PreDestruct();
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

            RunActivateQueue();
            RunDeactivateQueue();
            RunDestructQueue();
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
