using System;

namespace UntitledGame.Animations
{
    public class FixedAction
    {
        protected Animation _animation { get; set; }
        protected Action[]  _frameActions;

        public FixedAction(Animation animation)
        {
            _animation = animation;
        }

        public virtual void InvokeFrame(int animationFrame) { }
    }
}
