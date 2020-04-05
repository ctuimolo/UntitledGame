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
        protected List<GameObject> QueuedGameObjects;
        
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
            QueuedGameObjects   = new List<GameObject>();
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
            QueuedGameObjects.Add(gameObject);
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
        protected void ActivateQueue()
        {
            foreach (GameObject gameObject in QueuedGameObjects)
            {
                Activate(gameObject.Key);
            }
            QueuedGameObjects.Clear();
        }

        // Spawns a cached object, moves into active obj list and update loop. Physics bodies reset.
        protected void Activate(string key)
        {
            if (!CachedGameObjects.ContainsKey(key))
            {
                Console.Error.WriteLine("Room : \"{0}\" : Activate() : Keyname \"{1}\" not found in loaded objects", Key, key);
                return;
            }
            ActiveGameObjects.Add(CachedGameObjects[key]);
            if(CachedGameObjects[key].Body != null)
            {
                CachedGameObjects[key].Body.BoxCollider.Data = CachedGameObjects[key].Body.Category;
                foreach (Hitbox childHitbox in CachedGameObjects[key].Body.ChildHitboxes.Values)
                {
                    World.AddHitbox(childHitbox);
                }
            }
            if(CachedGameObjects[key].Drawable)
            {
                DrawableGameObjects.Add(CachedGameObjects[key]);
            }
        }

        // Remove a cached object from update loop, does not deallocate. Physics bodies crushed and moved to (-1,-1)
        protected void Deactivate(string key)
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
                CachedGameObjects[key].Body.BoxCollider.Data = CollisionCategory.none;
            }
        }

        // TODO: needs work, pre-destruct and deallocate the memory assigned to the cached object
        //       Also probably need to do something about the child hitboxes and PhysicsBodies...
        protected void Destruct(string key)
        {
            if (!CachedGameObjects.ContainsKey(key))
            {
                Console.Error.WriteLine("Room : \"{0}\" : Destruct() : Keyname \"{1}\" not found in loaded objects", Key, key);
                return;
            }
            ActiveGameObjects.Remove(CachedGameObjects[key]);
            DrawableGameObjects.Remove(CachedGameObjects[key]);
            CachedGameObjects[key].PreDestruct();
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

            ActivateQueue();
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
