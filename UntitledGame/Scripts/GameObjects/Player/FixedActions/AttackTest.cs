using System;

using UntitledGame.Animations;

namespace UntitledGame.GameObjects.Player.FixedActions
{
    public class AttackTest : FixedAction
    {   
        public AttackTest(Animation animation) : base(animation)
        {
            _frameActions = new Action[animation.FrameCount * animation.FrameDelay];
            _frameActions[4] = TestMe;
        }

        private void TestMe()
        {
            Console.WriteLine("from frame 4 of attack");
        }

        public override void InvokeFrame(int animationFrame)
        {
            _frameActions[animationFrame]?.Invoke();
        }
    }
}
