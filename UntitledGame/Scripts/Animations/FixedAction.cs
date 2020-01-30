using System;

namespace UntitledGame.Animations
{
    public class FixedAction
    {
        protected AnimationHandler  _animationHandler;
        protected Animation         _animation;

        protected Action[]  _frameActions;

        public Action BehaviorFunctions { get; protected set; }

        public FixedAction(AnimationHandler animationHandler)
        {
            _animationHandler = animationHandler;
            BehaviorFunctions = InvokeFrame;
        }

        protected virtual void InvokeFrame()
        {
            if(_animationHandler != null && _frameActions != null)
                _frameActions[_animationHandler.CurrentFrame]?.Invoke();
        }
    }
}
