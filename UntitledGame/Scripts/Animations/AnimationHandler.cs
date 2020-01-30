using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

using UntitledGame.GameObjects;

namespace UntitledGame.Animations
{
    public enum Orientation
    {
        Right,
        Left
    }

    public class AnimationHandler
    {

        private int _drawIndex       = 0;
        private int _animationTimer  = 0;
        private int _state           = 0;

        public Dictionary<int, Animation> Animations { get; private set; }
        public Animation    CurrentAnimation         { get; private set; }

        public bool     Finished        { get; private set; } = false;
        public int      CurrentFrame    { get; private set; }

        public GameObject   Owner       { get; private set; }
        public Orientation  Facing      { get; set; }

        public AnimationHandler(GameObject owner)
        {
            Owner          = owner;
            Animations   = new Dictionary<int, Animation>();
        }

        public void AddAnimation(int key, Animation animation)
        {
            Animations.Add(key, animation);
        }

        public void ChangeAnimation(int state)
        {
            if(_state != state)
            {
                _state = state;
                CurrentAnimation = Animations[_state];
                _drawIndex   = Animations[_state].StartIndex;
                CurrentFrame = _drawIndex;
                Finished = false;
            }
        }

        public void SetDrawIndex(int setDrawIndex)
        {
            if (setDrawIndex >= CurrentAnimation.FrameCount)
                _drawIndex = 0;
            else
                _drawIndex = setDrawIndex;
        }

        public void UpdateIndex()
        {
            if (CurrentAnimation != null && CurrentAnimation.Play)
            {
                CurrentFrame++;
                _animationTimer++;
                if (_animationTimer >= CurrentAnimation.FrameDelay)
                {
                    _animationTimer = 0;
                    _drawIndex++;
                    if (_drawIndex >= CurrentAnimation.FrameCount)
                    {
                        if (CurrentAnimation.Loop)
                        {
                            _drawIndex = CurrentAnimation.LoopIndex;
                            CurrentFrame = _drawIndex;
                        }
                        else
                        {
                            Finished = true;
                            _drawIndex--;
                            CurrentFrame--;
                        }
                    }
                }
            }
        }

        public void DrawFrame()
        {
            if(CurrentAnimation != null)
            {
                Game.SpriteBatch.Draw(
                    CurrentAnimation.SpriteSheet,
                    new Vector2(Owner.Body.BoxCollider.X, Owner.Body.BoxCollider.Y) - CurrentAnimation.Offset,
                    CurrentAnimation.GetDrawRect(_drawIndex),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1f,
                    Facing == Orientation.Right ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                    0.5f
                );
            }
        }

        public void DrawDebug()
        {
            Game.SpriteBatch.DrawString(Debug.Assets.DebugFont, "_drawIndex  : " + _drawIndex, new Vector2(10, 100), Color.Pink);
            CurrentAnimation.DrawDebug();
        }
        
    }
}
