using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

using UntitledGame.Animations;
using UntitledGame.Dynamics;
using UntitledGame.Input;

namespace UntitledGame.GameObjects.Particles
{
    public class Particle : GameObject
    {
        protected int _lifespan;

        public override void Update()
        {
            if (CurrentWorld.State == WorldState.Update)
            {
                base.Update();
                
            }
        }
    }
}
