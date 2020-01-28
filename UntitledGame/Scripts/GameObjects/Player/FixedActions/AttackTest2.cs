using Microsoft.Xna.Framework;

using System;

using UntitledGame.Dynamics;
using UntitledGame.Animations;

namespace UntitledGame.GameObjects.Player.FixedActions
{
    public class AttackTest2 : FixedAction
    {
        private Player _player;

        public AttackTest2(Animation animation, Player player) : base(animation)
        {
            _player = player;
            _frameActions = new Action[animation.FrameCount * animation.FrameDelay];

            _frameActions[1] = TestMe;
            _frameActions[2] = TestMe;
            _frameActions[3] = TestMe;
            _frameActions[4] = TestMe;
            _frameActions[5] = TestMe;
            _frameActions[6] = TestMe;
            _frameActions[7] = TestMe;
        }

        private void TestMe()
        {
            Console.WriteLine("From TestMe");
        }

        public override void InvokeFrame(int animationFrame)
        {
            _frameActions[animationFrame]?.Invoke();
        }
    }
}
