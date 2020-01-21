using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

using UntitledGame.Dynamics;
using UntitledGame.Animations;

namespace UntitledGame.GameObjects.Player
{
    static class Player_BehaviorLibrary
    {
        public static bool isOverlappingOrange;
        public static bool isOverlappingPink;

        public static void LoadBehaviors(Player player)
        {
            player.BehaviorFunctions += Test;
        }

        private static void Test()
        {
            //Console.WriteLine("from behaviors delegate");
        }

        public static void LoadCollisionHandling(Player player)
        {
            player.ResolveCollisions += CheckPurpleOrange;
        }

        public static void CheckPurpleOrange(PhysicsBody Body)
        {

            isOverlappingOrange = false;
            isOverlappingPink = false;

            if (!Body.IsFloored)
            {
                if (Body.Velocity.Y <= 0)
                {
                    Body.Owner.AnimationHandler.ChangeAnimation((int)AnimationStates.Rising);
                }
                else
                {
                    Body.Owner.AnimationHandler.ChangeAnimation((int)AnimationStates.Falling);
                }
            }

            foreach (Hitbox collision in Body.CurrentCollisions)
            {
                if (collision.Data.Value == "orange")
                {
                    isOverlappingOrange = true;
                }
                if (collision.Data.Value == "purple")
                {
                    isOverlappingPink = true;
                }
            }

        }
    }
}
