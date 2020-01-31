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
        private AttackTest      _attackTest;
        private AttackTest2     _attackTest2;
        private Player_Idle     _FA_Idle;

        // Only expose what behavior fields are necessary for external elements like the animation handler.
        // Working on a method to decouple the Behavior and Animation
        public bool isOverlappingOrange;
        public bool isOverlappingPink;

        public Player_BehaviorScript(Player player, AnimationHandler animationHandler)
        {
            _player             = player;
            _body               = player.Body;
            _animationHandler   = animationHandler;

            _FA_Idle        = new Player_Idle(this);
            _attackTest     = new AttackTest(this);
            _attackTest2    = new AttackTest2(this);
        }

        public void SetController(ref InputManager controller)
        {
            _controller = controller;
        }

        // The order of procedure from owner.Update();
        public void InitBehaviors()
        {
            // Set owner BehaviorFunctions delegate to the desired script from init and other behaviors
            _player.BehaviorFunctions = _FA_Idle.BehaviorFunctions;
        }

        public void CheckState()
        {
            isOverlappingOrange = false;
            isOverlappingPink   = false;
        }

        private void FA_ReturnToIdle()
        {
            if(_animationHandler.Finished)
            {
                _player.BehaviorFunctions = _FA_Idle.BehaviorFunctions;
            }
        }

        public static void SetVelocity(Player player, float x, float y)
        {
            player.Body.Velocity.X = x;
            player.Body.Velocity.Y = y;
        }
    }
}
