﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

using PhysicsWorld;

using UntitledGame.GameObjects;

namespace UntitledGame.Dynamics
{
    public enum CollisionCategory
    {
        none,
        wall,
        dynamic,
    }

    public class PhysicsBody
    {
        public Dictionary<string, Hitbox>  ChildHitboxes { get; private set; }
        public List<string>         HitboxesToRemove    { get; private set; }
        public List<Hitbox>         CurrentCollisions   { get; private set; }
        public CollisionCategory    Category            { get; set; }

        public bool IsFloored   { get; set; }
        public bool IsActive    { get; set; }

        public readonly IBox        BoxCollider;
        public readonly GameObject  Owner;

        public bool     GravityEnabled      { get; set; } = true;
        public Vector2  Velocity = new Vector2(0, 0);

        public PhysicsBody(GameObject owner, IBox body, CollisionCategory collisionCategory = CollisionCategory.dynamic)
        {
            Owner            = owner;
            BoxCollider      = body;
            Category         = collisionCategory;

            ChildHitboxes       = new Dictionary<string, Hitbox>();
            HitboxesToRemove    = new List<string>();
            CurrentCollisions   = new List<Hitbox>();
        }

        public void ClearHitboxes()
        {
            foreach(string key in HitboxesToRemove)
            {
                Owner.CurrentWorld.RemoveHitbox(ChildHitboxes[key]);
                ChildHitboxes[key].Timer = ChildHitboxes[key].InitTimer;
                ChildHitboxes.Remove(key);
            }
            HitboxesToRemove.Clear();
        }
        
        public void DrawDebug()
        {
            foreach (Hitbox hitbox in ChildHitboxes.Values)
            {
                Game.SpriteBatch.Draw(
                    hitbox.DebugSprite,
                    new Vector2(hitbox.Position.X, hitbox.Position.Y),
                    new Rectangle(0, 0, hitbox.Size.X, hitbox.Size.Y),
                    new Color(Color.White, .5f),
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0f);
            }

            Debug.Assets.DrawRectBorder(
                Game.SpriteBatch,
                new Rectangle(
                    (int)BoxCollider.X,
                    (int)BoxCollider.Y,
                    (int)BoxCollider.Width,
                    (int)BoxCollider.Height),
                    1,
                    Color.Yellow);
        }

        public void Destruct()
        {

        }
    }
}
