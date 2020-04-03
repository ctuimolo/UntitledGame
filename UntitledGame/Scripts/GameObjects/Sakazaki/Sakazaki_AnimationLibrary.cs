using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using UntitledGame.Animations;

namespace UntitledGame.GameObjects.Sakazaki
{
    public enum AnimationStates
    {
        Idle,
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
        }
    }
}
