﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UntitledGame.Animations
{
    public enum OffsetType
    {
        CenterBottom,
        None,
    }

    public class Animation
    {
        public Texture2D SpriteSheet { get; set; }
        public Rectangle DrawRect;
        
        public Vector2  Offset     { get; set; }
        public int      FrameCount { get; set; }
        public int      FrameDelay { get; set; }
        public int      StartIndex { get; set; }
        public int      LoopIndex  { get; set; }
        public bool     Loop       { get; set; } = true;
        public bool     Play       { get; set; } = true;
        public bool     InitState  { get; set; } = false;

        public OffsetType OffsetType = OffsetType.CenterBottom;

        public Animation(Rectangle drawRect, Point ownerSize)
        {
            DrawRect = drawRect;
            if(OffsetType == OffsetType.CenterBottom)
            {
                Offset = new Vector2(DrawRect.Width / 2 - ownerSize.X / 2, drawRect.Height - ownerSize.Y - 1);
            }
        }

        public Rectangle GetDrawRect(int drawIndex)
        {
            DrawRect.X = (DrawRect.Width * drawIndex) % SpriteSheet.Width;
            DrawRect.Y = DrawRect.Height * ((DrawRect.Width * drawIndex) / SpriteSheet.Width);
            return DrawRect;
        }

        public void DrawDebug()
        {
            Game.SpriteBatch.DrawString(Debug.Assets.DebugFont, "DrawRect.X : " + DrawRect.X, new Vector2(10, 76), Color.Pink);
            Game.SpriteBatch.DrawString(Debug.Assets.DebugFont, "DrawRect.Y : " + DrawRect.Y, new Vector2(10, 88), Color.Pink);
        }
    }
}
