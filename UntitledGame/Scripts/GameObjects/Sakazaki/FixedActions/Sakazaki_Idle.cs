using Microsoft.Xna.Framework;

using System;

using UntitledGame.Dynamics;
using UntitledGame.Animations;
using UntitledGame.Input;

namespace UntitledGame.GameObjects.Sakazaki
{
    public partial class Sakazaki_BehaviorScript
    {
        private class Sakazaki_Idle : FixedAction
        {
            private Sakazaki    _owner;
            private PhysicsBody _body;

            private Sakazaki_BehaviorScript _behaviorScript;

            public Sakazaki_Idle(Sakazaki_BehaviorScript behaviorScript) : base(behaviorScript._animationHandler)
            {
                _owner          = behaviorScript._owner;
                _behaviorScript = behaviorScript;
                _body           = behaviorScript._body;

                BehaviorFunctions += CheckIdle;
            }

            private void CheckIdle()
            {
                if (_body.IsFloored)
                {
                    _animationHandler.ChangeAnimation((int)AnimationStates.Idle);
                }
            }
        }
    }
}
