using Microsoft.Xna.Framework;

using System;

using UntitledGame.Dynamics;
using UntitledGame.Animations;
using UntitledGame.Input;

using UntitledGame.ShaderEffects.Blinkout;

namespace UntitledGame.GameObjects.Sakazaki
{
    public partial class Sakazaki_BehaviorScript
    {
        private class Sakazaki_Knockdown : FixedAction
        {
            private Sakazaki _owner;
            private PhysicsBody _body;

            private int _countdownToBlinkout    = 15;
            private int _countdownToDestroy     = 30;

            private Blinkout _blinkout;

            private Sakazaki_BehaviorScript _behaviorScript;
            public Sakazaki_Knockdown(Sakazaki_BehaviorScript behaviorScript) : base(behaviorScript._animationHandler)
            {
                _behaviorScript = behaviorScript;
                _owner          = behaviorScript._owner;
                _body           = behaviorScript._body;

                _blinkout = new Blinkout();

                BehaviorFunctions += Animate;
            }

            private void Animate()
            {
                if (!_body.IsFloored)
                {
                    _animationHandler.ChangeAnimation((int)AnimationStates.Hit1);
                }
                else
                {
                    _body.Velocity.X = 0;
                    _animationHandler.ChangeAnimation((int)AnimationStates.Knockdown);
                    if (_animationHandler.Finished)
                    {
                        _owner.BehaviorFunctions = CountDownToBlinkout;
                    }
                }
            }

            private void CountDownToBlinkout()
            {
                _countdownToBlinkout--;
                if(_countdownToBlinkout <= 0)
                {
                    _animationHandler.SetShaderEffect(_blinkout);
                    _owner.BehaviorFunctions = CountDownToDestroy;
                }
            }

            private void CountDownToDestroy()
            {
                _countdownToDestroy--;
                if(_countdownToDestroy <= 0)
                {
                    _owner.FlagForDestruction();
                }
            }
        }
    }
}
