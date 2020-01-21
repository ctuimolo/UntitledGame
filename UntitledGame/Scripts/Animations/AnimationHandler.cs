using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

using UntitledGame.GameObjects;

namespace UntitledGame.Animations
{
    public enum PlayerOrientation
    {
        Right,
        Left
    }

    public class AnimationHandler
    {

        private Dictionary<int, Animation> _animationDic { get; }
        private Animation  _currentAnimation;

        private int _drawIndex       = 0;
        private int _animationTimer  = 0;
        private int _state           = 0;

        public GameObject Owner         { get; private set; }
        public PlayerOrientation Facing { get; set; }

        public AnimationHandler(GameObject owner)
        {
            Owner          = owner;
            _animationDic   = new Dictionary<int, Animation>();
        }

        public void AddAnimation(int key, Animation animation)
        {
            _animationDic.Add(key, animation);
        }

        public void ChangeAnimation(int state)
        {
            if(_state != state)
            {
                _state = state;
                _drawIndex = _animationDic[_state].StartIndex;
            }
        }

        public void SetDrawIndex(int set_drawIndex)
        {
            if (set_drawIndex >= _currentAnimation.FrameCount)
                _drawIndex = 0;
            else
                _drawIndex = set_drawIndex;
        }

        public void UpdateIndex()
        {
            if (_currentAnimation != null && _currentAnimation.Play)
            {
                _animationTimer++;
                if (_animationTimer >= _currentAnimation.FrameDelay)
                {
                    _animationTimer = 0;
                    _drawIndex++;
                    if (_drawIndex >= _currentAnimation.FrameCount)
                    {
                        if (_currentAnimation.Loop)
                        {
                            _drawIndex = _currentAnimation.LoopIndex;
                        }
                        else
                        {
                            _drawIndex--;
                        }
                    }
                }
            }
        }

        public void DrawFrame()
        {
            _currentAnimation = _animationDic[_state];
            Game.SpriteBatch.Draw(
                _currentAnimation.SpriteSheet,
                new Vector2(Owner.Body.BoxCollider.X, Owner.Body.BoxCollider.Y) - _currentAnimation.Offset,
                _currentAnimation.GetDrawRect(_drawIndex),
                Color.White,
                0,
                Vector2.Zero,
                1f,
                Facing == PlayerOrientation.Right ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                0f
            );
        }

        public void DrawDebug()
        {
            Game.SpriteBatch.DrawString(Debug.Assets.DebugFont, "_drawIndex  : " + _drawIndex, new Vector2(10, 100), Color.Pink);
            _currentAnimation.DrawDebug();
        }
        
    }
}
