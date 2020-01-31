using Microsoft.Xna.Framework;

using System;

using UntitledGame.Dynamics;
using UntitledGame.Animations;

namespace UntitledGame.GameObjects.Player
{
    public partial class Player_BehaviorScript
    {
        private class AttackTest2 : FixedAction
        {
            private Player      _player;
            private PhysicsBody _body;
            private Player_BehaviorScript _behaviorScript;

            private Action _startup;
            private Action _airborne;
            private Action _landing;

            public Player.BehaviorsDelegate Attack1Script { get; private set; }

            public AttackTest2(Player_BehaviorScript behaviorScript) : base(behaviorScript._animationHandler)
            {
                _player             = behaviorScript._player;
                _behaviorScript     = behaviorScript;
                _animationHandler   = behaviorScript._animationHandler;
                _body               = behaviorScript._body;
                _animation          = _animationHandler.Animations[(int)AnimationStates.Attack2_1];
                _frameActions       = new Action[_animation.FrameCount * _animation.FrameDelay];

                _startup    += FA_Attack2_GotoAirborne;
                _airborne   += FA_Attack2_CheckLand;
                _airborne   += FA_Attack2_Airborne;
                _landing    += FA_Attack2_Slide;

                BehaviorFunctions = _startup;
            }

            private void FA_Attack2_GotoAirborne()
            {
                if (_animationHandler.Finished)
                {
                    _player.BehaviorFunctions = _airborne;
                    _body.Velocity.Y = -6;
                    _body.Velocity.X = 4;
                }
            }

            private void FA_Attack2_Airborne()
            {
                if (!_body.IsFloored)
                {
                    if (_body.Velocity.Y <= 0)
                    {
                        _animationHandler.ChangeAnimation((int)AnimationStates.Attack2_2_rise);
                    }
                    else
                    {
                        _animationHandler.ChangeAnimation((int)AnimationStates.Attack2_2_fall);
                    }
                }
            }

            private void FA_Attack2_CheckLand()
            {
                if (_body.IsFloored)
                {
                    _body.Velocity.X = 0;
                    _player.BehaviorFunctions = _landing;
                }
            }

            private void FA_Attack2_Slide()
            {
                _animationHandler.ChangeAnimation((int)AnimationStates.Attack2_3);
                if (_animationHandler.Finished)
                {
                    _player.BehaviorFunctions = _behaviorScript._FA_Idle.BehaviorFunctions;
                }
            }
        }
    }
}
