using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

using PhysicsWorld;
using PhysicsWorld.Responses;

using UntitledGame.GameObjects;

namespace UntitledGame.Dynamics
{
    public enum WorldState
    {
        Update,
        Pause,
    }

    public class WorldHandler
    {
        private readonly World              _world;
        private readonly Dictionary<string, PhysicsBody>  _dynamicBodies;
        private readonly Dictionary<string, Hitbox>       _worldHitboxes;

        public WorldState State     { get; set; } = WorldState.Update;
        public float Gravity        { get; set; } = 0.6f;
        public float MaxFallSpeed   { get; set; } = 12f;

        public WorldHandler(Point worldSize)
        {
            _world          = new World(worldSize.X, worldSize.Y);
            _dynamicBodies  = new Dictionary<string, PhysicsBody>();
            _worldHitboxes  = new Dictionary<string, Hitbox>();
        }

        public PhysicsBody AddBody(GameObject owner, Vector2 position, Point size, bool isDynamic = true)
        {
            if(_dynamicBodies.ContainsKey(owner.Key))
            {
                Console.Error.WriteLine("WorldHandler : AddBody() : Keyname \"{1}\" already exists", owner.Key);
                Environment.Exit(1);
            }

            PhysicsBody newBody = new PhysicsBody(owner, _world.Create(position.X, position.Y, size.X, size.Y));
            if (isDynamic)
            {
                _dynamicBodies[owner.Key] = newBody;
            }

            return newBody;
        }

        public void RemoveBody(PhysicsBody body)
        {
            _world.Remove(body.BoxCollider);
            _dynamicBodies.Remove(body.Owner.Key);
        }

        public void AddHitbox(Hitbox hitbox)
        {
            if (_worldHitboxes.ContainsKey(hitbox.Key))
            {
                Console.Error.WriteLine("WorldHandler : AddHitbox() : Keyname \"{0}\" already exists", hitbox.Key);
                Environment.Exit(1);
            }

            _worldHitboxes[hitbox.Key] = hitbox;
        }

        public void RemoveHitbox(Hitbox hitbox)
        {
            _worldHitboxes.Remove(hitbox.Key);
        }

        public PhysicsBody CreatePhysicsBody(GameObject owner, Vector2 position, Point size)
        {
            return new PhysicsBody(owner, _world.Create(position.X, position.Y, size.X, size.Y));
        }

        private bool IsAABBOverlap(Hitbox A, Hitbox B)
        {
            return (A.Position.X <= B.Position.X + B.Size.X && A.Position.X + A.Size.X >= B.Position.X) &&
                   (A.Position.Y <= B.Position.Y + B.Size.Y && A.Position.Y + A.Size.Y >= B.Position.Y);
        }

        public void PhysicsStep()
        {
            if(State == WorldState.Update)
            {
                foreach (PhysicsBody body in _dynamicBodies.Values)
                {
                    body.IsFloored = false;
                    body.CurrentCollisions.Clear();

                    if (body.GravityEnabled && body.Velocity.Y < MaxFallSpeed)
                    {
                        if(body.Velocity.Y + Gravity <= MaxFallSpeed)
                        {
                            body.Velocity.Y += Gravity;
                        } else
                        {
                            body.Velocity.Y = MaxFallSpeed;
                        }
                    }

                    body.BoxCollider.Move(
                        body.BoxCollider.X + body.Velocity.X,
                        body.BoxCollider.Y + body.Velocity.Y, 
                        (collision) =>
                        {
                            if((CollisionCategory)collision.Other.Data == CollisionCategory.wall)
                            {
                                if (body.Velocity.Y > 0 && collision.Hit.Normal.Y < 0)
                                {
                                    body.Velocity.Y = 0;
                                    body.IsFloored = true;
                                }
                                else if (body.Velocity.Y < 0 && collision.Hit.Normal.Y > 0)
                                {
                                    body.Velocity.Y = 0;
                                }
                                if (body.Velocity.X > 0 && collision.Hit.Normal.X < 0)
                                {
                                    body.Velocity.X = 0;
                                }
                                else if (body.Velocity.X < 0 && collision.Hit.Normal.X > 0)
                                {
                                    body.Velocity.X = 0;
                                }
                                return CollisionResponses.Slide;
                            } else
                                return CollisionResponses.None;
                        });

                    body.ClearHitboxes();

                    foreach (Hitbox hitbox in body.ChildHitboxes.Values)
                    {
                        hitbox.Position.X = body.BoxCollider.X + hitbox.Offset.X;
                        hitbox.Position.Y = body.BoxCollider.Y + hitbox.Offset.Y;

                        foreach (Hitbox other in _worldHitboxes.Values)
                        {
                            if (!body.CurrentCollisions.Contains(other)     &&
                                !ReferenceEquals(body.Owner, other.Owner)   &&
                                IsAABBOverlap(hitbox, other))
                            {
                                body.CurrentCollisions.Add(other);
                            }
                        }

                        if(hitbox.Timer > 0)
                        {
                            hitbox.Timer --;
                            if(hitbox.Timer == 0)
                            {
                                body.HitboxesToRemove.Add(hitbox.Key);
                            }
                        }
                    }
                }
            }
        }

        public void DrawDebug()
        {
            foreach(Hitbox hitbox in _worldHitboxes.Values)
            {
                hitbox.DrawDebug();
            }
        }
    }
}
