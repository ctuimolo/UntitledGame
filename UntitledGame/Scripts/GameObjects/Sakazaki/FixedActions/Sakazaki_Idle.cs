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
                BehaviorFunctions += CheckCollision;
            }

            private void CheckIdle()
            {
                if (_body.IsFloored)
                {
                    _animationHandler.ChangeAnimation((int)AnimationStates.Idle);
                } else
                {
                    _animationHandler.ChangeAnimation((int)AnimationStates.Airborne);
                }
            }

            private void CheckCollision()
            {
                foreach(Hitbox collision in _body.CurrentCollisions)
                {
                    if(collision.Data.Type == CollisionType.Attack)
                    {
                        _body.Velocity.Y = -8; 
                        if(collision.Data.Orientation == Orientation.Left)
                        {
                            _body.Velocity.X = -3;
                            _owner.State.Facing = Orientation.Left;
                        }
                        else if (collision.Data.Orientation == Orientation.Right)
                        {
                            _body.Velocity.X = 3;
                            _owner.State.Facing = Orientation.Right;
                        }
                        _owner.BehaviorFunctions = _behaviorScript._FA_knockdown.BehaviorFunctions;
                    }
                }
            }
        }
    }
}
