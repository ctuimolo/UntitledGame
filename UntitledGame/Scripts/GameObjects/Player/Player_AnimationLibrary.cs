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
        Attack2_1,
        Attack2_2_rise,
        Attack2_2_fall,
        Attack2_3,
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

            animationHandler.AddAnimation(
               (int)AnimationStates.Attack2_1,
                new Animation(new Rectangle(0, 0, 100, 100), animationHandler.Owner.Size)
                {
                    SpriteSheet = Game.Assets.Load<Texture2D>("SpriteSheets/suika_attack2_1"),
                    FrameCount = 4,
                    FrameDelay = 3,
                    Loop = false
                });

            animationHandler.AddAnimation(
               (int)AnimationStates.Attack2_2_rise,
                new Animation(new Rectangle(0, 0, 140, 80), animationHandler.Owner.Size)
                {
                    SpriteSheet = Game.Assets.Load<Texture2D>("SpriteSheets/suika_attack2_2"),
                    FrameCount = 1,
                    Loop = false
                });

            animationHandler.AddAnimation(
               (int)AnimationStates.Attack2_2_fall,
                new Animation(new Rectangle(0, 0, 140, 80), animationHandler.Owner.Size)
                {
                    SpriteSheet = Game.Assets.Load<Texture2D>("SpriteSheets/suika_attack2_2"),
                    FrameCount = 1,
                    Loop = false,
                    StartIndex = 1,
                });

            animationHandler.AddAnimation(
               (int)AnimationStates.Attack2_3,
                new Animation(new Rectangle(0, 0, 140, 120), animationHandler.Owner.Size)
                {
                    SpriteSheet = Game.Assets.Load<Texture2D>("SpriteSheets/suika_attack2_3"),
                    FrameCount = 8,
                    FrameDelay = 6,
                    Loop = false,
                    Offset = new Vector2(60,44),
                });
        }
    }
}
