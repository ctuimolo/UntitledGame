using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using UntitledGame.GameObjects;
using UntitledGame.Animations;

namespace UntitledGame.Dynamics
{
    public enum CollisionType
    {
        None,
        Attack,
        SomethingElse,
    }

    // Play around with what exactly to do with this
    // generic package for collision communication

    public struct CollisionPackage
    {
        public string Value;
        public string String;
        public CollisionType Type;
        public Orientation Orientation;
    }

    public class Hitbox
    {
        private bool    _flippedX = false;
        private Vector2 _initOffset;

        // Monogame drawing fields
        public GameObject   Owner       { get; private set; } = null;
        public string       Key         { get; protected set; }
        public Texture2D    DebugSprite { get; set; } 

        public Vector2 Offset;
        public Vector2 Position;
        public Point   Size;
        public int     InitTimer;
        public int     Timer;
        public CollisionPackage Data;

        public delegate void enact();

        public Hitbox(GameObject owner, Vector2 offset, Point size, string key, int timer = -1)
        {
            _initOffset = offset;

            Key         = key;
            Offset      = offset;
            Size        = size;
            InitTimer   = timer;
            Timer       = timer;

            if (owner != null )
            {
                Owner = owner;
                Position = new Vector2(owner.Body.BoxCollider.X + offset.X, owner.Body.BoxCollider.Y + offset.Y);
            } else {
                Position = Offset;
            }
        }

        public void InitPosition(Orientation direction)
        {
            if(direction == Orientation.Left && !_flippedX)
            {
                Offset.X = _initOffset.X * -1 - Size.X + Owner.Size.X;
                _flippedX = true;
            }

            if (direction == Orientation.Right && _flippedX)
            {
                Offset.X = _initOffset.X;
                _flippedX = false;
            }

            Position = new Vector2(Owner.Body.BoxCollider.X + Offset.X, Owner.Body.BoxCollider.Y + Offset.Y);
        }

        public void DrawDebug()
        {
            Game.SpriteBatch.Draw(
                DebugSprite,
                new Vector2(Position.X, Position.Y),
                new Rectangle(0, 0, Size.X, Size.Y),
                new Color(Color.White, 0.01f),
                0f,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                0f
            );
        }
    }
}
