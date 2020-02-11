﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UntitledGame.Debug
{
    public static class Assets
    {
        public static SpriteFont    DebugFont   = Game.Assets.Load<SpriteFont>("Fonts/console");

        public static Texture2D     RedBox      = Game.Assets.Load<Texture2D>("Debug/red");
        public static Texture2D     BlueBox     = Game.Assets.Load<Texture2D>("Debug/blue");
        public static Texture2D     GreenBox    = Game.Assets.Load<Texture2D>("Debug/green");
        public static Texture2D     OrangeBox   = Game.Assets.Load<Texture2D>("Debug/orange");
        public static Texture2D     PurpleBox   = Game.Assets.Load<Texture2D>("Debug/purple");
        public static Texture2D     PinkBox     = Game.Assets.Load<Texture2D>("Debug/pink");
        public static Texture2D     GreyBox     = Game.Assets.Load<Texture2D>("Debug/grey");
        public static Texture2D     BlackBox    = Game.Assets.Load<Texture2D>("Debug/black");
        
        public static Texture2D     InputHandler_Sheet = Game.Assets.Load<Texture2D>("UI/InputHandler_Sheet");
        public static Rectangle     D_Null = new Rectangle(0, 0, 0, 0);
        public static Rectangle     D4_SpriteIndex = new Rectangle(0, 56, 28, 28);
        public static Rectangle     D6_SpriteIndex = new Rectangle(28, 28, 28, 28);
        public static Rectangle     D8_SpriteIndex = new Rectangle(0, 0, 28, 28);
        public static Rectangle     D2_SpriteIndex = new Rectangle(28, 84, 28, 28);
        public static Rectangle     D1_SpriteIndex = new Rectangle(0, 84, 28, 28);
        public static Rectangle     D3_SpriteIndex = new Rectangle(28, 56, 28, 28);
        public static Rectangle     D7_SpriteIndex = new Rectangle(0, 28, 28, 28);
        public static Rectangle     D9_SpriteIndex = new Rectangle(28, 0, 28, 28);
    }
}
