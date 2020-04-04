using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using UntitledGame.Animations;

namespace UntitledGame.GameObjects.Sakazaki
{
    public enum AnimationStates
    {
        Idle,
        Airborne,
        Hit1,
        Knockdown,
    }

    public class Sakazaki_AnimationLibrary
    {
        public void LoadAnimations(AnimationHandler animationHandler)
        {
            animationHandler.AddAnimation(
                (int)AnimationStates.Idle,
                new Animation(new Rectangle(0, 0, 120, 120), animationHandler.Owner.Size)
                {
                    SpriteSheet = Game.Assets.Load<Texture2D>("SpriteSheets/yuri_idle_sheet"),
                    FrameCount = 7,
                    FrameDelay = 5,
                    InitState  = true,
                });

            animationHandler.AddAnimation(
                (int)AnimationStates.Airborne,
                new Animation(new Rectangle(0, 0, 120, 120), animationHandler.Owner.Size)
                {
                    SpriteSheet = Game.Assets.Load<Texture2D>("SpriteSheets/yuri_airborne_sheet"),
                    FrameCount = 1,
                    Loop = false,
                });

            animationHandler.AddAnimation(
                (int)AnimationStates.Hit1,
                new Animation(new Rectangle(0, 0, 120, 120), animationHandler.Owner.Size)
                {
                    SpriteSheet = Game.Assets.Load<Texture2D>("SpriteSheets/yuri_hit1_sheet"),
                    FrameCount = 3,
                    FrameDelay = 3,
                    Loop = false,
                });

            animationHandler.AddAnimation(
                (int)AnimationStates.Knockdown,
                new Animation(new Rectangle(0, 0, 140, 76), animationHandler.Owner.Size)
                {
                    SpriteSheet = Game.Assets.Load<Texture2D>("SpriteSheets/yuri_knockdown_sheet"),
                    FrameCount = 4,
                    FrameDelay = 3,
                    Loop = false,
                });
        }
    }
}
