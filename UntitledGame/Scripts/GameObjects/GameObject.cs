using Microsoft.Xna.Framework;

using UntitledGame.Animations;
using UntitledGame.Input;
using UntitledGame.Dynamics;

namespace UntitledGame.GameObjects
{

    public abstract class GameObject
    {
        protected   AnimationHandler  AnimationHandler    { get; set; }

        public PhysicsBody  Body        { get; protected set; }
        public bool         Drawable    { get; protected set; }
        public string       Key         { get; protected set; }

        protected Vector2 Position;

        public WorldHandler CurrentWorld    { get; protected set; }
        public Vector2      InitPosition    { get; protected set; }
        public Point        Size            { get; protected set; }

        public virtual void LoadContent()       { }
        public virtual void Draw()              { }
        public virtual void DrawDebug()         { }

        public virtual void Update()
        {
            if(Position != null && Body.BoxCollider != null)
            {
                Position.X = Body.BoxCollider.X;
                Position.Y = Body.BoxCollider.Y;
            }
        }

        public virtual void Destruct()
        {
            if(CurrentWorld != null && Body != null)
            {
                CurrentWorld.RemoveBody(Body);
            }
        }
    }
}
