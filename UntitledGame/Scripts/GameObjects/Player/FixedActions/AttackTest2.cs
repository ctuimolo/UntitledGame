using Microsoft.Xna.Framework;

using System;

using UntitledGame.Dynamics;
using UntitledGame.Animations;

namespace UntitledGame.GameObjects.Player
{
    public partial class Player_BehaviorScript
    {
        private class AttackTest2 : FixedAction
        {
            private Player _player;
            private Hitbox _hitbox1;

            private Player_BehaviorScript _behaviorScript;

            public Player.BehaviorsDelegate Attack1Script { get; private set; }

            public AttackTest2(Animation animation, Player_BehaviorScript behaviorScript) : base(behaviorScript._animationHandler)
            {
            }

            protected override void InvokeFrame()
            {
                _frameActions[0]?.Invoke();
            }
        }
    }
}
