using Microsoft.Xna.Framework;

using System;

using UntitledGame.Dynamics;
using UntitledGame.Animations;
using UntitledGame.Input;

namespace UntitledGame.GameObjects.Sakazaki
{
    public partial class Sakazaki_BehaviorScript
    {
        private class Sakazaki_Knockdown : FixedAction
        {
            private Sakazaki _owner;
            private PhysicsBody _body;

            private Sakazaki_BehaviorScript _behaviorScript;
            public Sakazaki_Knockdown(Sakazaki_BehaviorScript behaviorScript) : base(behaviorScript._animationHandler)
            {
                _behaviorScript = behaviorScript;
                _owner          = behaviorScript._owner;
                _body           = behaviorScript._body;

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
                }
            }
        }
    }
}
