using Microsoft.Xna.Framework;

using System;

using UntitledGame.Dynamics;
using UntitledGame.Animations;

namespace UntitledGame.GameObjects.Player
{
    public partial class Player_BehaviorScript {

        private class AttackTest : FixedAction
        {
            private Player      _player;
            private Hitbox      _hitbox1;
            private AnimationHandler _animationHandler;

            private Player_BehaviorScript   _behaviorScript;

            public  Player.BehaviorsDelegate Attack1Script { get; private set; }

            public AttackTest(Animation animation,Player_BehaviorScript behaviorScript) : base(animation)
            {
                _player             = behaviorScript._player;
                _behaviorScript     = behaviorScript;
                _animationHandler   = behaviorScript._animationHandler;

                _frameActions     = new Action[animation.FrameCount * animation.FrameDelay];
                _frameActions[16] = TestMe;

                _hitbox1 = new Hitbox(_player, new Vector2(53, 14), new Point(80, 16), _player.Key + "_AttackTest_hitbox1", 2)
                {
                    DebugSprite = Debug.Assets.RedBox,
                };
                Attack1Script += InvokeFrame;
                Attack1Script += _behaviorScript.FA_ReturnToIdle;
            }

            private void InvokeFrame ()
            {
                _frameActions[_animationHandler.CurrentFrame]?.Invoke();
            }

            private void TestMe()
            {
                _hitbox1.InitPosition(_player.State.Facing);
                _player.Body.ChildHitboxes[_hitbox1.Key] = _hitbox1;
            }

            //public override void InvokeFrame(int animationFrame)
            //{
            //    _frameActions[animationFrame]?.Invoke();
            //}
        }
    }
}
