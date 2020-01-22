using UntitledGame.Dynamics;
using UntitledGame.Animations;
using UntitledGame.Input;

namespace UntitledGame.GameObjects.Player
{
    public class Player_BehaviorFunctions
    {
        // Required fields for game component behavior. Add and remove fields from the constructor as necessary
        private readonly Player             _player;
        private readonly PhysicsBody        _body;
        private readonly AnimationHandler   _animationHandler;
        private readonly InputManager       _controller;

        // Local fields for behavior scripts
        private readonly float _walkSpeed       = 3;
        private readonly float _jumpStrength    = 8;

        // Only expose what behavior fields are necessary for external elements like the animation handler.
        // Working on a method to decouple the Behavior and Animation
        public bool isOverlappingOrange;
        public bool isOverlappingPink;

        public Player_BehaviorFunctions(Player player, PhysicsBody body, AnimationHandler animationHandler, InputManager controller)
        {
            _player             = player;
            _body               = body;
            _animationHandler   = animationHandler;
            _controller         = controller;
        }

        // The order of procedure from owner.Update();
        public void InitBehaviors()
        {
            _player.BehaviorFunctions += CheckPurpleOrange;
            _player.BehaviorFunctions += IdleAnimation;
            _player.BehaviorFunctions += HandleInput;
        }

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

        private void HandleInput()
        {
            if (_controller.InputDown(InputFlags.Left) && !_controller.InputDown(InputFlags.Right))
            {
                _body.Velocity.X = -_walkSpeed;
                _animationHandler.Facing = Orientation.Left;
                if (_body.IsFloored)
                {
                    _animationHandler.ChangeAnimation((int)AnimationStates.Walking);
                }
            }

            if (_controller.InputDown(InputFlags.Right) && !_controller.InputDown(InputFlags.Left))
            {
                _body.Velocity.X = _walkSpeed;
                _animationHandler.Facing = Orientation.Right;
                if (_body.IsFloored)
                {
                    _animationHandler.ChangeAnimation((int)AnimationStates.Walking);
                }
            }

            if (!_controller.InputDown(InputFlags.Left) && !_controller.InputDown(InputFlags.Right))
            {
                _body.Velocity.X = 0;
                if (_body.IsFloored)
                {
                    _animationHandler.ChangeAnimation((int)AnimationStates.Idle);
                }
            }

            if (_controller.InputDown(InputFlags.Left) && _controller.InputDown(InputFlags.Right))
            {
                _body.Velocity.X = 0;
                if (_body.IsFloored)
                {
                    _animationHandler.ChangeAnimation((int)AnimationStates.Idle);
                }
            }

            if (_controller.InputPressed(InputFlags.Button1))
            {
                _body.Velocity.Y = -_jumpStrength;
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

    }
}
