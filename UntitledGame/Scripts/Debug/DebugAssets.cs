using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace UntitledGame.Debug
{
    public static class Assets
    {
        public static SpriteFont    DebugFont   = Game.Assets.Load<SpriteFont>("Fonts/font");
        public static Texture2D     RedBox      = Game.Assets.Load<Texture2D>("DebugSprites/red");
        public static Texture2D     BlueBox     = Game.Assets.Load<Texture2D>("DebugSprites/blue");
        public static Texture2D     GreenBox    = Game.Assets.Load<Texture2D>("DebugSprites/green");
        public static Texture2D     OrangeBox   = Game.Assets.Load<Texture2D>("DebugSprites/orange");
        public static Texture2D     PurpleBox   = Game.Assets.Load<Texture2D>("DebugSprites/purple");
        public static Texture2D     PinkBox     = Game.Assets.Load<Texture2D>("DebugSprites/pink");
        public static Texture2D     GreyBox     = Game.Assets.Load<Texture2D>("DebugSprites/grey");
        public static Texture2D     BlackBox    = Game.Assets.Load<Texture2D>("DebugSprites/black");
    }
}
