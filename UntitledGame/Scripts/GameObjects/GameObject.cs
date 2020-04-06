using Microsoft.Xna.Framework;

using UntitledGame.Animations;
using UntitledGame.Dynamics;
using UntitledGame.Rooms;

namespace UntitledGame.GameObjects
{

    public abstract class GameObject
    {
        public AnimationHandler  AnimationHandler { get; protected set; }
        public string       Key         { get; protected set; }
        public PhysicsBody  Body        { get; protected set; }
        public bool         Drawable    { get; protected set; }
        public bool         Active      { get; private set; }

        protected Vector2 Position;

        public WorldHandler CurrentWorld    { get; set; }
        public Room         CurrentRoom     { get; set; }
        public Vector2      InitPosition    { get; protected set; }
        public Point        Size            { get; protected set; }

        public virtual void LoadContent()   { }

        public virtual void Update()
        {
            if(Position != null && Body != null)
            {
                Position.X = Body.BoxCollider.X;
                Position.Y = Body.BoxCollider.Y;
            }
        }

        public virtual void LateUpdate()    { }
        public virtual void Draw()          { }
        public virtual void DrawDebug()     { }

        public virtual void PreActivate()
        {
            Active = true;
        }

        public virtual void PreDeactivate()
        {
            Active = false;
        }

        public void FlagForDeactivation()
        {
            CurrentRoom.DeactivateQueue.Add(Key);
        }

        public void FlagForDestruction()
        {
            CurrentRoom.DestructQueue.Add(Key);
        }

        public void PreDestruct() { }
    }
}
