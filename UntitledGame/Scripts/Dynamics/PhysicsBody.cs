﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

using PhysicsWorld;

using UntitledGame.GameObjects;

namespace UntitledGame.Dynamics
{
    public class PhysicsBody
    {
        public Dictionary<int, Hitbox>  ChildHitboxes       { get; private set; }
        public List<Hitbox>             CurrentCollisions   { get; private set; }
        public bool IsFloored   { get; set; }

        public readonly IBox        BoxCollider;
        public readonly GameObject  Owner;

        public bool     GravityEnabled      { get; set; } = true;
        public Vector2  Velocity = new Vector2(0, 0);

        public PhysicsBody(GameObject owner, IBox body)
        {
            Owner       = owner;
            BoxCollider = body;

            ChildHitboxes       = new Dictionary<int, Hitbox>();
            CurrentCollisions   = new List<Hitbox>();
        }
        
        public void DrawDebug()
        {
            foreach(Hitbox hitbox in ChildHitboxes.Values)
            {
                Game.SpriteBatch.Draw(
                    Debug.Assets.BlueBox,
                    new Vector2(hitbox.Position.X, hitbox.Position.Y),
                    new Rectangle(0, 0, hitbox.Size.X, hitbox.Size.Y),
                    new Color(Color.White, 0.5f),
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0f);
            }
        }

        public void Destruct()
        {

        }
    }
}
