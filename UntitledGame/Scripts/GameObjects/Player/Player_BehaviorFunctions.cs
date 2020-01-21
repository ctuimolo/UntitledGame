using UntitledGame.Dynamics;
using UntitledGame.Animations;

namespace UntitledGame.GameObjects.Player
{
    public class Player_BehaviorFunctions
    {
        private readonly Player             _player;
        private readonly PhysicsBody        _body;
        private readonly AnimationHandler   _animationHandler;

        public bool isOverlappingOrange;
        public bool isOverlappingPink;

        public Player_BehaviorFunctions(Player player, PhysicsBody body, AnimationHandler animationHandler)
        {
            _player             = player;
            _body               = body;
            _animationHandler   = animationHandler;
        }

        public void InitBehaviors()
        {
            _player.BehaviorFunctions += CheckPurpleOrange;
            _player.BehaviorFunctions += IdleAnimation;
        }

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
