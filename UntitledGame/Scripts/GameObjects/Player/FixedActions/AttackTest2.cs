using Microsoft.Xna.Framework;

using System;

using UntitledGame.Dynamics;
using UntitledGame.Animations;

namespace UntitledGame.GameObjects.Player.FixedActions
{
    public class AttackTest2 : FixedAction
    {
        private Player _player;

        private Hitbox _hitbox1;

        public AttackTest2(Animation animation, Player player) : base(animation)
        {
            _player = player;
            _hitbox1 = new Hitbox(player, new Vector2(53,14), new Point(80,16), _player.Key + "_AttackTest2_hitbox1", 2)
            {
                DebugSprite =  Debug.Assets.RedBox,
            };

            _frameActions = new Action[animation.FrameCount * animation.FrameDelay];
            _frameActions[2] = TestMe;
            _frameActions[3] = TestMe2;
            _frameActions[4] = TestMe2;
            _frameActions[5] = TestMe2;
            _frameActions[6] = TestMe2;
            _frameActions[7] = TestMe2;
            _frameActions[8] = TestMe2;
            _frameActions[9] = TestMe2;
            _frameActions[10] = TestMe2;
            _frameActions[11] = TestMe2;
            _frameActions[12] = TestMe2;
            _frameActions[13] = TestMe2;
            _frameActions[14] = TestMe2;
            _frameActions[15] = TestMe2;
        }

        private void TestMe()
        {
            Player_BehaviorFunctions.SetVelocity(_player, 2, 0);
        }


        private void TestMe2()
        {
            Player_BehaviorFunctions.SetVelocity(_player, 2, _player.Body.Velocity.Y);
        }

        public override void InvokeFrame(int animationFrame)
        {
            _frameActions[animationFrame]?.Invoke();
        }
    }
}
