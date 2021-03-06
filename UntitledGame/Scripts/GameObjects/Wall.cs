﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using UntitledGame.Dynamics;

namespace UntitledGame.GameObjects.Wall
{
    public class Wall : GameObject
    {
        private bool _drawDebug = true;
        private KeyboardState _oldKeyState;

        public Hitbox Hitbox  { get; private set; }

        public Wall(Rectangle coordinates, string key)
        {
            // Object fields
            Size         = new Point(coordinates.Width, coordinates.Height);
            Position     = new Vector2(coordinates.X, coordinates.Y);
            InitPosition = Position;
            Key          = key;
            Drawable     = true;
        }

        public override void LoadContent()
        {
            Body = CurrentWorld.AddBody(this, Position, Size, false);
            Body.Category = CollisionCategory.wall;
        }

        public override void Update()
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.F1) && _oldKeyState.IsKeyUp(Keys.F1))
                _drawDebug = !_drawDebug;
            _oldKeyState = state;
        }

        public override void Draw()
        {
            DrawDebug();
        }

        public override void DrawDebug()
        {
            Game.SpriteBatch.Draw(
                Debug.Assets.GreyBox,
                new Vector2(Body.BoxCollider.X, Body.BoxCollider.Y),
                new Rectangle(0, 0, (int)Size.X, (int)Size.Y),
                Color.White,
                0,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                1f
            );
        }
    }
}