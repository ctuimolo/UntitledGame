using Microsoft.Xna.Framework;

using System;

using UntitledGame.Dynamics;
using UntitledGame.Animations;
using UntitledGame.Input;

namespace UntitledGame.GameObjects.Player
{
    public partial class Player_BehaviorScript
    {

        private class Player_Idle : FixedAction
        {
            private Player          _player;
            private PhysicsBody     _body;

            private readonly float _walkSpeed = 3;
            private readonly float _jumpStrength = 8;

            private Player_BehaviorScript _behaviorScript;

            public Player_Idle(Player_BehaviorScript behaviorScript) : base(behaviorScript._animationHandler)
            {
                _player         = behaviorScript._player;
                _behaviorScript = behaviorScript;
                _body           = behaviorScript._body;

                BehaviorFunctions += CheckJumpInput;
                BehaviorFunctions += CheckMoveInput;
                BehaviorFunctions += CheckAirborne;
                BehaviorFunctions += CheckAttack1Input;
                BehaviorFunctions += CheckAttack2Input;
                BehaviorFunctions += CheckPurpleOrange;
            }
            public void CheckAirborne()
            {
                if (!_body.IsFloored)
                {
                    if (_body.Velocity.Y <= 0)
                    {
                        _animationHandler.ChangeAnimation((int)AnimationStates.Rising);
                    }
                    else
                    {
                        _animationHandler.ChangeAnimation((int)AnimationStates.Falling);
                    }
                }
            }

            private void CheckJumpInput()
            {
                if(_behaviorScript._controller != null)
                    if (_behaviorScript._controller.InputPressed(InputFlags.Button1))
                    {
                        _body.Velocity.Y = -_jumpStrength;
                    }
            }

            private void CheckMoveInput()
            {
                if (_behaviorScript._controller != null)
                {
                    if (_behaviorScript._controller.InputDown(InputFlags.Left) && !_behaviorScript._controller.InputDown(InputFlags.Right))
                    {
                        _body.Velocity.X = -_walkSpeed;
                        _player.State.Facing = Orientation.Left;
                        if (_body.IsFloored)
                        {
                            _animationHandler.ChangeAnimation((int)AnimationStates.Walking);
                        }
                    }
                    else if (_behaviorScript._controller.InputDown(InputFlags.Right) && !_behaviorScript._controller.InputDown(InputFlags.Left))
                    {
                        _body.Velocity.X = _walkSpeed;
                        _player.State.Facing = Orientation.Right;
                        if (_body.IsFloored)
                        {
                            _animationHandler.ChangeAnimation((int)AnimationStates.Walking);
                        }
                    }
                    else
                    {
                        if (_body.IsFloored)
                        {
                            _animationHandler.ChangeAnimation((int)AnimationStates.Idle);
                        }
                        _body.Velocity.X = 0;
                    }
                }
            }

            private void CheckAttack1Input()
            {
                if(_behaviorScript._controller != null)
                    if (_behaviorScript._controller.InputPressed(InputFlags.Button2))
                    {
                        if (_body.IsFloored)
                        {
                            _body.Velocity.X = 0;
                            _animationHandler.ChangeAnimation((int)AnimationStates.Attack1);
                            _player.BehaviorFunctions = _behaviorScript._attackTest.BehaviorFunctions;
                        }
                    }
            }

            private void CheckAttack2Input()
            {
                if (_behaviorScript._controller != null) 
                    if (_behaviorScript._controller.InputPressed(InputFlags.Button3))
                    {
                        if (_body.IsFloored)
                        {
                            _body.Velocity.X = 0;
                            _animationHandler.ChangeAnimation((int)AnimationStates.Attack2_1);
                            _player.BehaviorFunctions = _behaviorScript._attack2Script_startup;
                        }
                    }
            }

            public void CheckPurpleOrange()
            {
                foreach (Hitbox collision in _body.CurrentCollisions)
                {
                    if (collision.Data.Value == "orange")
                    {
                        _behaviorScript.isOverlappingOrange = true;
                    }
                    if (collision.Data.Value == "purple")
                    {
                        _behaviorScript.isOverlappingPink = true;
                    }
                }
            }
        }
    }
}
