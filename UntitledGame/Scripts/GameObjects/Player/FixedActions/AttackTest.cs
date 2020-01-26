using Microsoft.Xna.Framework;

using System;

using UntitledGame.Dynamics;
using UntitledGame.Animations;

namespace UntitledGame.GameObjects.Player.FixedActions
{
    public class AttackTest : FixedAction
    {
        private Player _player;

        private Hitbox _hitbox1;

        public AttackTest(Animation animation, Player player) : base(animation)
        {
            _player = player;
            _hitbox1 = new Hitbox(player, new Vector2(53,14), new Point(80,16), _player.Key + "_AttackTest_hitbox1", 2)
            {
                DebugSprite =  Debug.Assets.RedBox,
            };

            _frameActions = new Action[animation.FrameCount * animation.FrameDelay];
            _frameActions[16] = TestMe;
        }

        private void TestMe()
        {
            _hitbox1.InitPosition(_player.State.Facing);
            _player.Body.ChildHitboxes[_hitbox1.Key] = _hitbox1;
        }

        public override void InvokeFrame(int animationFrame)
        {
            _frameActions[animationFrame]?.Invoke();
        }
    }
}
