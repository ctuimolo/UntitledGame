using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

using UntitledGame.Animations;
using UntitledGame.Dynamics;
using UntitledGame.Input;

namespace UntitledGame.GameObjects.Sakazaki
{
    public class SakazakiSpawner : GameObject
    {
        private InputManager _controller;
        private int _sakazakiCount = 0;

        public SakazakiSpawner(string key)
        {
            Key             = key;
            Drawable        = false;
        }

        public override void LoadContent()
        {
            _controller = Game.GlobalKeyboard;
        }

        public override void Update()
        {
            if (CurrentWorld.State == WorldState.Update)
            {
                base.Update();
                if(_controller.InputPressed(InputFlags.Debug5))
                {
                    // this is borked for now
                    _sakazakiCount++;
                    CurrentWorld.OwnerRoom.QueueGameObject(
                        new Sakazaki( 
                            new Vector2( Game.Rng.Next(80, 720), 100), "sakazakiSpawner_" + _sakazakiCount));
                }
            }
        }
    }
}
