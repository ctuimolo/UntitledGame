using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using System;
using System.Collections.Generic;

using UntitledGame.Input;
using UntitledGame.Rooms;
using UntitledGame.Rooms.TestRoom;

namespace UntitledGame
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        // Engine IO and Physics inits
        private static int  _targetFPS = 60;

        // Game camera and view
        private bool                _drawDebug = false;
        private Matrix              _view;
        private int                 _frameCount;
        private double              _frameRate;

        // Global input profiles
        public static Dictionary<string, InputManager> InputProfiles { get; private set; }
        public static InputManager GlobalKeyboard { get; private set; }

        // Graphics and World physics
        public static GraphicsDeviceManager Graphics        { get; private set; }
        public static SpriteBatch           SpriteBatch     { get; private set; }
        public static ContentManager        Assets          { get; private set; }
        public static RoomHandler           Rooms           { get; private set; }
        public static Room                  CurrentRoom     { get; private set; }

        public Game()
        {
            Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth        = 800,
                PreferredBackBufferHeight       = 480,
            };
            TargetElapsedTime       = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / _targetFPS);
            Content.RootDirectory   = "Content";
            Assets = Content;
        }

        protected override void LoadContent()
        {
            InputProfiles   = new Dictionary<string, InputManager>();
            SpriteBatch     = new SpriteBatch(Graphics.GraphicsDevice);
            Rooms           = new RoomHandler();

            InputProfiles["global_keyboard"] = new InputManager();
            GlobalKeyboard = InputProfiles["global_keyboard"];

            Rooms.AddRoom(new TestRoom(new Point(1000,1000), "test_room"));
            CurrentRoom = Rooms.GetRoom("test_room");
            CurrentRoom.LoadContent();
            CurrentRoom.InitializeRoom();

            _view = Matrix.Identity;
        }

        public static int CurrentTargetFPS()
        {
            return _targetFPS;
        }

        private void HandleKeyboard()
        {
            if (GlobalKeyboard.InputPressed(InputFlags.Escape))
                Exit();

            if (GlobalKeyboard.InputPressed(InputFlags.Debug1))
                _drawDebug = !_drawDebug;
        }

        protected override void Update(GameTime gameTime) // 60 updates per ~1000ms, non-buffering (slowdown enabled)
        {
            if(IsActive)
            {
                CurrentRoom.Update();
                HandleKeyboard();
                foreach(InputManager profile in InputProfiles.Values)
                {
                    profile.ParseInput();
                }
            }
        }

        protected override void Draw(GameTime gameTime) // calls after Update()
        {
            if (IsActive)
            {
                // FPS and update count debug
                if (_frameCount >= _targetFPS - 1)
                    _frameCount = 0;
                else
                    _frameCount++;

                _frameRate = Math.Round((1 / gameTime.ElapsedGameTime.TotalSeconds), 1);

                GraphicsDevice.Clear(Color.Black);
                SpriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, _view);

                SpriteBatch.DrawString(Debug.Assets.DebugFont, "Toggle debug view: [F1] Pause: [P] Move: [A][D][Space]", new Vector2(10, 10), Color.White);
                SpriteBatch.DrawString(Debug.Assets.DebugFont, "_frameCount: " + _frameCount, new Vector2(10, 22), Color.White);
                SpriteBatch.DrawString(Debug.Assets.DebugFont, "Target FPS: " + _targetFPS, new Vector2(10, 34), Color.White);
                SpriteBatch.DrawString(Debug.Assets.DebugFont, "Avg FPS:    " + _frameRate, new Vector2(10, 46), Color.White);

                CurrentRoom.Draw();

                SpriteBatch.End();

                base.Draw(gameTime);
            }

        }
    }
}