using Microsoft.Xna.Framework.Graphics;

using UntitledGame.Animations;

namespace UntitledGame.ShaderEffects
{
    public class ShaderEffect
    {
        public Effect           Effect              { get; protected set; }
        public AnimationHandler AnimationHandler    { get; set; }

        protected ShaderEffect()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Apply()
        {
        }
    }
}
