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
        private int _stateQueue      = -1;

        public Dictionary<int, Animation> Animations { get; private set; }
        public Animation    CurrentAnimation         { get; private set; }

        public bool     Finished        { get; private set; } = false;
        public int      CurrentFrame    { get; private set; }

        public GameObject   Owner       { get; private set; }
        public Orientation  Facing      { get; set; } = Orientation.Right;
        public float        LayerDepth  { get; set; } = 0f;
        public Effect       Effect      { get; set; } = null;

        public AnimationHandler(GameObject owner, int initState = 0)
        {
            Owner       = owner;
            Animations  = new Dictionary<int, Animation>();
        }

        public void AddAnimation(int key, Animation animation)
        {
            Animations.Add(key, animation);
            if(animation.InitState)
            {
                CurrentAnimation = animation;
            }
        }

        public void ChangeAnimation(int state)
        {
            if(_state != state)
            {
                _stateQueue = state;

                _state = state;
                _drawIndex = Animations[_state].StartIndex;
                _animationTimer = 0;

                CurrentAnimation = Animations[_state];
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
                if (_stateQueue > -1)
                {
                    // Enter: animation state was queued to change; reset animation instead of incrementing
                    CurrentFrame = _drawIndex * CurrentAnimation.FrameDelay;
                    _state = _stateQueue;
                    _drawIndex = Animations[_state].StartIndex;
                    _animationTimer = 0;
                    _stateQueue = -1;

                    CurrentAnimation = Animations[_state];
                    Finished = false;
                } else if(!Finished)
                {
                    // Enter: increment current animation frame
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
                                CurrentFrame = _drawIndex * CurrentAnimation.FrameDelay - 1;
                            }
                            else
                            {
                                _drawIndex--;
                                CurrentFrame--;
                                Finished = true;
                            }
                        }
                    }
                }
            }
        }

        public void DrawFrame()
        {
            if(CurrentAnimation != null)
            {
                if(Effect != null)
                {
                    Game.SpriteBatch.End();
                    Game.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
                    Effect.Parameters["TextureHeight"].SetValue(CurrentAnimation.SpriteSheet.Height);
                    Effect.CurrentTechnique.Passes[0].Apply();
                }
                Game.SpriteBatch.Draw(
                    CurrentAnimation.SpriteSheet,
                    new Vector2(Owner.Body.BoxCollider.X, Owner.Body.BoxCollider.Y) - CurrentAnimation.Offset,
                    CurrentAnimation.GetDrawRect(_drawIndex),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1f,
                    Facing == Orientation.Right ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                    LayerDepth
                );
                if(Effect != null)
                {
                    Game.SpriteBatch.End();
                    Game.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
                }
            }
        }

        public void DrawDebug()
        {
            Game.SpriteBatch.DrawString(Debug.Assets.DebugFont, "_drawIndex   : " + _drawIndex, new Vector2(10, 100), Color.Pink);
            Game.SpriteBatch.DrawString(Debug.Assets.DebugFont, "CurrentFrame : " + CurrentFrame, new Vector2(10, 112), Color.Pink);
            CurrentAnimation.DrawDebug();
        }
    }
}
