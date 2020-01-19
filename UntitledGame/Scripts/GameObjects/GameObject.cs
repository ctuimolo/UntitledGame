using Microsoft.Xna.Framework;

using UntitledGame.Animations;
using UntitledGame.Dynamics;

namespace UntitledGame.GameObjects
{

    public abstract class GameObject
    {
        public PhysicsBody      Body               { get; protected set; }
        public AnimationHandler AnimationHandler   { get; protected set; }
        public string           Key                { get; protected set; }

        public WorldHandler CurrentWorld    { get; protected set; }
        public Vector2      Position        { get; protected set; }
        public Point        Size            { get; protected set; }

        public virtual void Initialize()        { }
        public virtual void LoadContent()       { }
        public virtual void Update()            { }
        public virtual void ResolveCollisions() { }
        public virtual void Draw()              { }
        public virtual void DrawDebug()         { }
    }
}
