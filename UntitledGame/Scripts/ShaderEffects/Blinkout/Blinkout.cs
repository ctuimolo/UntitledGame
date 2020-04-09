using Microsoft.Xna.Framework.Graphics;

using UntitledGame.Animations;

namespace UntitledGame.ShaderEffects.Blinkout
{
    public class Blinkout : ShaderEffect
    {
        private bool _step;
        private int _stepCounter;
        private int _stepCounterInit = 2;

        public Blinkout()
        {
            Effect = Game.Assets.Load<Effect>("Effects/Blinkout");
            _stepCounter = _stepCounterInit;
        }

        public override void Update()
        {
            if (AnimationHandler != null && AnimationHandler.CurrentAnimation != null)
            {
                _stepCounter--;
                if(_stepCounter == 0)
                {
                    _step = !_step;
                    _stepCounter = _stepCounterInit;
                }
            }
        }

        public override void Apply()
        {
            Effect.Parameters["SetInvisible"].SetValue(_step);
            Effect.CurrentTechnique.Passes[0].Apply();
        }
    }
}
