using Microsoft.Xna.Framework.Graphics;

using UntitledGame.Animations;

namespace UntitledGame.ShaderEffects.PanelLines
{
    public class PanelLines : ShaderEffect
    {
        public PanelLines ()
        {
            Effect = Game.Assets.Load<Effect>("Effects/Panellines");
        }

        public override void Update()
        {
            if(AnimationHandler != null && AnimationHandler.CurrentAnimation != null)
            {
                Effect.Parameters["TextureHeight"].SetValue(AnimationHandler.CurrentAnimation.SpriteSheet.Height);
                Effect.CurrentTechnique.Passes[0].Apply();
            }
        }

        public override void Apply()
        {
            Effect.Parameters["TextureHeight"].SetValue(AnimationHandler.CurrentAnimation.SpriteSheet.Height);
            Effect.CurrentTechnique.Passes[0].Apply();
        }
    }
}
