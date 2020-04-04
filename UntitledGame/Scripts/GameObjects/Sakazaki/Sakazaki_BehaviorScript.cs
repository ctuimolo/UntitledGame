using System;

using UntitledGame.Dynamics;
using UntitledGame.Animations;

namespace UntitledGame.GameObjects.Sakazaki
{
    public class Sakazaki_State
    {
        public Orientation Facing = Orientation.Left;
    }

    public partial class Sakazaki_BehaviorScript
    {
        enum State
        {
            Idle,
            Busy,
        }

        // Required fields for game component behavior. Add and remove fields from the constructor as necessary
        private readonly Sakazaki       _owner;
        private readonly PhysicsBody    _body;
        private readonly AnimationHandler   _animationHandler;

        // Fixed actions (i.e., attack animation, scripted events based on animation frames)
        private Sakazaki_Idle       _FA_Idle;
        private Sakazaki_Knockdown  _FA_knockdown;

        // Only expose what behavior fields are necessary for external elements like the animation handler.
        // Working on a method to decouple the Behavior and Animation
        public bool isOverlappingOrange;
        public bool isOverlappingPink;

        public Sakazaki_BehaviorScript(Sakazaki owner)
        {
            _owner  = owner;
            _body   = owner.Body;
            _animationHandler = owner.AnimationHandler;

            _FA_Idle        = new Sakazaki_Idle(this);
            _FA_knockdown   = new Sakazaki_Knockdown(this);
        }

        // The order of procedure from owner.Update();
        public void InitBehaviors()
        {
            // Static delegate enacts first, for ease of logic
            _owner.FirstBehaviorFunctions = null;

            // Set owner BehaviorFunctions delegate to the desired script
            _owner.BehaviorFunctions += _FA_Idle.BehaviorFunctions;
        }
    }
}
