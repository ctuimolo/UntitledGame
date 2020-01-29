using System;

using UntitledGame.Dynamics;
using UntitledGame.Animations;
using UntitledGame.Input;

namespace UntitledGame.GameObjects.Player
{
    public class Player_State
    {
        public Orientation Facing = Orientation.Right;
    }

    public partial class Player_BehaviorScript
    {
        enum State
        {
            Idle,
            Busy,
        }

        // Required fields for game component behavior. Add and remove fields from the constructor as necessary
        private readonly Player             _player;
        private readonly PhysicsBody        _body;
        private readonly AnimationHandler   _animationHandler;
        private InputManager                _controller;

        // Fixed actions (i.e., attack animation, scripted events based on animation frames)
        private FixedAction  _currentFixedAction;
        private AttackTest   _attackTest;
        private AttackTest2  _attackTest2;

        private Player.BehaviorsDelegate _idleScript;
        private Player.BehaviorsDelegate _attack2Script_startup;
        private Player.BehaviorsDelegate _attack2Script_airborne;
        private Player.BehaviorsDelegate _attack2Script_land;

        // Local fields for behavior scripts
        private readonly float _walkSpeed       = 3;
        private readonly float _jumpStrength    = 8;

        // Only expose what behavior fields are necessary for external elements like the animation handler.
        // Working on a method to decouple the Behavior and Animation
        public bool isOverlappingOrange;
        public bool isOverlappingPink;

        public Player_BehaviorScript(Player player, AnimationHandler animationHandler)
        {
            _player             = player;
            _body               = player.Body;
            _animationHandler   = animationHandler;

            _attackTest = new AttackTest(_animationHandler.Animations[(int)AnimationStates.Attack1], this);
            _attackTest2 = new AttackTest2(_animationHandler.Animations[(int)AnimationStates.Attack2_3], this);
        }

        public void SetController(InputManager controller)
        {
            _controller = controller;
        }

        // The order of procedure from owner.Update();
        public void InitBehaviors()
        {
            // Creating scripts: add/remove methods to delegate to determine behavior
            _idleScript += CheckJumpInput;
            _idleScript += CheckMoveInput;
            _idleScript += CheckAirborne;
            _idleScript += CheckAttack1Input;
            _idleScript += CheckAttack2Input;
            _idleScript += CheckPurpleOrange;

            // Invoke fixed actions for auto initing to Idle upon animation finish
            _attack2Script_startup += FA_Attack2_GotoAirborne;
            _attack2Script_airborne += FA_Attack2_CheckLand;
            _attack2Script_airborne += FA_Attack2_Airborne;
            //_attack2Script_land += FA_ReturnToIdle;
            _attack2Script_land += FA_Attack2_Slide;

            // Set owner BehaviorFunctions delegate to the desired script from init and other behaviors
            _player.BehaviorFunctions = _idleScript;
        }

        public void CheckState()
        {
            isOverlappingOrange = false;
            isOverlappingPink   = false;
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
            if (_controller.InputPressed(InputFlags.Button1))
            {
                _body.Velocity.Y = -_jumpStrength;
            }
        }

        private void CheckMoveInput()
        {
            if (_controller.InputDown(InputFlags.Left) && !_controller.InputDown(InputFlags.Right))
            {
                _body.Velocity.X = -_walkSpeed;
                _player.State.Facing = Orientation.Left;
                if (_body.IsFloored)
                {
                    _animationHandler.ChangeAnimation((int)AnimationStates.Walking);
                }
            }else if (_controller.InputDown(InputFlags.Right) && !_controller.InputDown(InputFlags.Left))
            {
                _body.Velocity.X = _walkSpeed;
                _player.State.Facing = Orientation.Right;
                if (_body.IsFloored)
                {
                    _animationHandler.ChangeAnimation((int)AnimationStates.Walking);
                }
            }else
            {
                if (_body.IsFloored)
                {
                    _animationHandler.ChangeAnimation((int)AnimationStates.Idle);
                }
                _body.Velocity.X = 0;
            }
        }

        private void CheckAttack1Input()
        {
            if (_controller.InputPressed(InputFlags.Button2))
            {
                if (_body.IsFloored)
                {
                    _body.Velocity.X = 0;
                    _animationHandler.ChangeAnimation((int)AnimationStates.Attack1);
                    //_currentFixedAction = _attackTest;
                    _player.BehaviorFunctions = _attackTest.Attack1Script;
                }
            }
        }

        private void CheckAttack2Input()
        {
            if (_controller.InputPressed(InputFlags.Button3))
            {
                if (_body.IsFloored)
                {
                    _body.Velocity.X = 0;
                    _animationHandler.ChangeAnimation((int)AnimationStates.Attack2_1);
                    _currentFixedAction = null;
                    _player.BehaviorFunctions = _attack2Script_startup;
                }
            }
        }

        private void FA_ReturnToIdle()
        {
            //_currentFixedAction?.InvokeFrame(_animationHandler.CurrentFrame);
            if(_animationHandler.Finished)
            {
                _currentFixedAction = null;
                _player.BehaviorFunctions = _idleScript;
            }
        }

        private void FA_Attack2_GotoAirborne()
        {
            _currentFixedAction?.InvokeFrame(_animationHandler.CurrentFrame);
            if (_animationHandler.Finished)
            {
                _currentFixedAction = null;
                _player.BehaviorFunctions = _attack2Script_airborne;
                _body.Velocity.Y = -6;
                _body.Velocity.X = 4;
            }
        }

        private void FA_Attack2_Airborne()
        {
            if(!_body.IsFloored)
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
                _currentFixedAction       = null;
                _player.BehaviorFunctions = _attack2Script_land;
            }
        }

        private void FA_Attack2_Slide()
        {
            _animationHandler.ChangeAnimation((int)AnimationStates.Attack2_3);
            if (_animationHandler.Finished)
            {
                //Console.WriteLine("FINISHED");
                _currentFixedAction = null;
                _player.BehaviorFunctions = _idleScript;
            }
        }

        public static void SetVelocity(Player player, float x, float y)
        {
            player.Body.Velocity.X = x;
            player.Body.Velocity.Y = y;
        }

        public void CheckPurpleOrange()
        {
            foreach (Hitbox collision in _body.CurrentCollisions)
            {
                if (collision.Data.Value == "orange")
                {
                    isOverlappingOrange = true;
                }
                if (collision.Data.Value == "purple")
                {
                    isOverlappingPink = true;
                }
            }
        }
    }
}
