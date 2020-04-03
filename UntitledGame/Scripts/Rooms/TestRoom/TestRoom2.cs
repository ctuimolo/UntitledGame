using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

using UntitledGame.Dynamics;
using UntitledGame.Input;
using UntitledGame.GameObjects;
using UntitledGame.GameObjects.Player;
using UntitledGame.GameObjects.Wall;
using UntitledGame.GameObjects.Sakazaki;

namespace UntitledGame.Rooms.TestRoom2
{
    public class TestRoom2 : Room
    {
        private InputManager _controller;

        public TestRoom2(Point worldSize, string setKey) : base(worldSize, setKey)
        {
            CachedGameObjects = new Dictionary<string, GameObject>();
            ActiveGameObjects = new List<GameObject>();
            DrawableGameObjects = new List<GameObject>();
        }

        public override void LoadContent()
        {
            _controller = Game.GlobalKeyboard;

            LoadGameObject(new Wall(World, new Rectangle(0, 420, 800, 80),  "wall_01"));
            LoadGameObject(new Wall(World, new Rectangle(796, 0, 4, 480),   "wall_02"));
            LoadGameObject(new Wall(World, new Rectangle(0, 0, 4, 480),     "wall_03"));
            LoadGameObject(new Wall(World, new Rectangle(0, 0, 800, 4),     "wall_04"));

            LoadGameObject(new Player(World, new Vector2(350, 300), "player_1"));
            LoadGameObject(new Sakazaki(World, new Vector2(Game.Rng.Next(80, 720), 100), "sakazaki_1"));
            LoadGameObject(new Sakazaki(World, new Vector2(Game.Rng.Next(80, 720), 100), "sakazaki_2"));
            LoadGameObject(new Sakazaki(World, new Vector2(Game.Rng.Next(80, 720), 100), "sakazaki_3"));
        }

        public override void InitializeRoom()
        {
            Instantiate("wall_01");
            Instantiate("wall_02");
            Instantiate("wall_03");
            Instantiate("wall_04");
            Instantiate("player_1");
            Instantiate("sakazaki_1");
            Instantiate("sakazaki_2");
            Instantiate("sakazaki_3");
        }

        private void HandleKeyboard()
        {
            if (_controller.InputPressed(InputFlags.Debug1))
                _drawDebug = !_drawDebug;

            if (_controller.InputPressed(InputFlags.Button9))
                World.State = (World.State == WorldState.Pause ? WorldState.Update : WorldState.Pause);
        }

        public override void Update()
        {
            base.Update();
            HandleKeyboard();
        }

        public override void Draw()
        {
            base.Draw();
            Game.SpriteBatch.DrawString(Debug.Assets.DebugFont, "CURRENT ROOM: " + Key, new Vector2(10, 24), Color.LightGray);
            if (World.State == WorldState.Pause)
            {
                Game.SpriteBatch.DrawString(Debug.Assets.DebugFont, "<< PAUSED >>", new Vector2(350, 48), Color.White);
            }
        }
    }
}
