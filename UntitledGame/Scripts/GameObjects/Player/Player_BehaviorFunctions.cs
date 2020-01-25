using UntitledGame.Dynamics;
using UntitledGame.Animations;
using UntitledGame.Input;

using UntitledGame.GameObjects.Player.FixedActions;

namespace UntitledGame.GameObjects.Player
{
    public class Player_BehaviorFunctions
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

        private State _state;

        // Fixed actions (i.e., attack animation, scripted events based on animation frames)
        private FixedAction _currentFixedAction;
        private AttackTest  _attackTest;

        // Local fields for behavior scripts
        private readonly float _walkSpeed       = 3;
        private readonly float _jumpStrength    = 8;

        // Only expose what behavior fields are necessary for external elements like the animation handler.
        // Working on a method to decouple the Behavior and Animation
        public bool isOverlappingOrange;
        public bool isOverlappingPink;

        public Player_BehaviorFunctions(Player player, PhysicsBody body, AnimationHandler animationHandler)
        {
            _player             = player;
            _body               = body;
            _animationHandler   = animationHandler;

            _attackTest = new AttackTest(_animationHandler.Animations[(int)AnimationStates.Attack1]);
        }

        public void SetController(InputManager controller)
        {
            _controller = controller;
        }

        // The order of procedure from owner.Update();
        public void InitBehaviors()
        {
            _player.BehaviorFunctions += InvokeFixedAction;
            _player.BehaviorFunctions += CheckAnimationState;
            _player.BehaviorFunctions += CheckPurpleOrange;
            _player.BehaviorFunctions += IdleAnimation;

            // Should be last, most of the time. Otherwise risk overwiting animation state logic
            _player.BehaviorFunctions += HandleInput;
        }

        private void InvokeFixedAction()
        {
            _currentFixedAction?.InvokeFrame(_animationHandler.CurrentFrame);
        }

        #region BehaviorScripts
        // Functional game logic below here...
        public void IdleAnimation()
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

        public void CheckAnimationState()
        {
            if(_state == State.Busy)
            {
                if(_animationHandler.Finished)
                {
                    _state = State.Idle;
                    _currentFixedAction = null;
                }
            }
        }

        private void HandleInput()
        {
            if(_controller != null)
            {
                if (_controller.InputPressed(InputFlags.Button2))
                {
                    if (_body.IsFloored && _state == State.Idle)
                    {
                        _body.Velocity.X = 0;
                        _animationHandler.ChangeAnimation((int)AnimationStates.Attack1);
                        _state = State.Busy;
                        _currentFixedAction = _attackTest;
                    }
                }

                if (_controller.InputDown(InputFlags.Left) && !_controller.InputDown(InputFlags.Right))
                {
                    if (_state != State.Busy)
                    {
                        _body.Velocity.X = -_walkSpeed;
                        _animationHandler.Facing = Orientation.Left;
                        if (_body.IsFloored)
                        {
                            _animationHandler.ChangeAnimation((int)AnimationStates.Walking);
                            _state = State.Idle;
                        }
                    }
                }

                if (_controller.InputDown(InputFlags.Right) && !_controller.InputDown(InputFlags.Left))
                {
                    if (_state != State.Busy)
                    {
                        _body.Velocity.X = _walkSpeed;
                        _animationHandler.Facing = Orientation.Right;
                        if (_body.IsFloored)
                        {
                            _animationHandler.ChangeAnimation((int)AnimationStates.Walking);
                            _state = State.Idle;
                        }
                    }
                }

                if (!_controller.InputDown(InputFlags.Left) && !_controller.InputDown(InputFlags.Right) ||
                     _controller.InputDown(InputFlags.Left) && _controller.InputDown(InputFlags.Right) )
                {
                    if(_state != State.Busy) {
                        _body.Velocity.X = 0;
                        if (_body.IsFloored)
                        {
                            _animationHandler.ChangeAnimation((int)AnimationStates.Idle);
                            _state = State.Idle;
                        }
                    }
                }

                if (_controller.InputPressed(InputFlags.Button1))
                {
                    if (_state != State.Busy)
                    {
                        _body.Velocity.Y = -_jumpStrength;
                    }
                }
            }
        }

        public void CheckPurpleOrange()
        {

            isOverlappingOrange = false;
            isOverlappingPink = false;

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
        #endregion

    }
}
