using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using UntitledGame.Animations;

namespace UntitledGame.GameObjects.Player
{
    public enum AnimationStates
    {
        Idle,
        Walking,
        Rising,
        Falling,
        Attack1,
    }

    public class Player_AnimationLibrary
    {
        public void LoadAnimations(AnimationHandler animationHandler)
        {
            animationHandler.AddAnimation(
                (int)AnimationStates.Idle,
                new Animation(new Rectangle(0, 0, 152, 152), animationHandler.Owner.Size)
                {
                    SpriteSheet = Game.Assets.Load<Texture2D>("SpriteSheets/suika_idle"),
                    FrameCount = 18,
                    FrameDelay = 6,
                });

            animationHandler.AddAnimation(
                (int)AnimationStates.Walking,
                new Animation(new Rectangle(0, 0, 96, 96), animationHandler.Owner.Size)
                {
                    SpriteSheet = Game.Assets.Load<Texture2D>("SpriteSheets/suika_walk"),
                    FrameCount = 8,
                    FrameDelay = 4
                });

            animationHandler.AddAnimation(
                (int)AnimationStates.Falling,
                new Animation(new Rectangle(0, 0, 126, 102), animationHandler.Owner.Size)
                {
                    SpriteSheet = Game.Assets.Load<Texture2D>("SpriteSheets/suika_fall"),
                    FrameCount = 3,
                    FrameDelay = 6,
                    LoopIndex = 1
                });

            animationHandler.AddAnimation(
               (int)AnimationStates.Rising,
                new Animation(new Rectangle(0, 0, 110, 110), animationHandler.Owner.Size)
                {
                    SpriteSheet = Game.Assets.Load<Texture2D>("SpriteSheets/suika_rise"),
                    FrameCount = 2,
                    FrameDelay = 4,
                    Loop = false
                });

            animationHandler.AddAnimation(
               (int)AnimationStates.Attack1,
                new Animation(new Rectangle(0, 0, 300, 100), animationHandler.Owner.Size)
                {
                    SpriteSheet = Game.Assets.Load<Texture2D>("SpriteSheets/suika_attack1"),
                    FrameCount = 11,
                    FrameDelay = 3,
                    Loop = false
                });
        }
    }
}
