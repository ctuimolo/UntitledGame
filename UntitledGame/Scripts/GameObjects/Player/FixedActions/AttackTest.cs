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

            private Player_BehaviorScript   _behaviorScript;

            public AttackTest(Player_BehaviorScript behaviorScript): base(behaviorScript._animationHandler)
            {
                _player             = behaviorScript._player;
                _behaviorScript     = behaviorScript;
                _animationHandler   = behaviorScript._animationHandler;
                _animation          = _animationHandler.Animations[(int)AnimationStates.Attack1];
                _frameActions       = new Action[_animation.FrameCount * _animation.FrameDelay];

                _hitbox1 = new Hitbox(_player, new Vector2(53, 14), new Point(80, 16), _player.Key + "_AttackTest_hitbox1", 2)
                {
                    DebugSprite = Debug.Assets.RedBox,
                    Data = new CollisionPackage()
                    {
                        Value   = "From player",
                        Type    = CollisionType.Attack,
                    },
                };

                _frameActions[17] = TestMe;
                BehaviorFunctions += _behaviorScript.FA_ReturnToIdle;
            }

            private void TestMe()
            {
                _hitbox1.Data.Orientation = _player.AnimationHandler.Facing;
                _hitbox1.InitPosition(_player.State.Facing);
                // TODO: need to add these childhitbox controls to physics body
                _player.Body.ChildHitboxes[_hitbox1.Key] = _hitbox1;
                _player.CurrentWorld.AddHitbox(_hitbox1);
            }
        }
    }
}
