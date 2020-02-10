using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;

namespace UntitledGame.Input.Replay
{
    public class InputRecord
    {
        private KeyboardState[] _kbRecord;
        private GamePadState[]  _gpRecord;
        private int _recordSize = 8192;
        private int _currFrame  = 0;

        private InputManager _controller;
        private Rectangle _null = new Rectangle(0,0,0,0);
        private Rectangle _d4_SpriteIndex = new Rectangle(0, 56, 28, 28);
        private Rectangle _d6_SpriteIndex = new Rectangle(28, 28, 28, 28);
        private Rectangle _d8_SpriteIndex = new Rectangle(0, 0, 28, 28);
        private Rectangle _d2_SpriteIndex = new Rectangle(28, 84, 28, 28);
        private Rectangle _d1_SpriteIndex = new Rectangle(0, 84, 28, 28);
        private Rectangle _d3_SpriteIndex = new Rectangle(28, 56, 28, 28);
        private Rectangle _d7_SpriteIndex = new Rectangle(0, 28, 28, 28);
        private Rectangle _d9_SpriteIndex = new Rectangle(28, 0, 28, 28);

        private Rectangle _currentDirection;
        private bool _hasHistory;
        private int _Xaxis;
        private int _Yaxis;

        public InputRecord(InputManager controller)
        {
            _controller = controller;
        }

        public void InitKBRecord()
        {
            _kbRecord = new KeyboardState[_recordSize];
        }

        public void InitGPRecord()
        {
            _gpRecord = new GamePadState[_recordSize];
        }

        public void InsertKBRecord()
        {
            if(_currFrame < _recordSize)
            {
                _kbRecord[_currFrame] = _controller.GetKBState();
                _currFrame++;
            }
        }

        public void DrawHistory()
        {
            int frame0 = (_currFrame - 1 > 0) ? _currFrame - 1 : 0;
            int frame1 = (_currFrame - 2 > 0) ? _currFrame - 2 : 0;
            int frame2 = (_currFrame - 3 > 0) ? _currFrame - 3 : 0;
            int frame3 = (_currFrame - 4 > 0) ? _currFrame - 4 : 0;
            int frame4 = (_currFrame - 5 > 0) ? _currFrame - 5 : 0;

            //Rectangle drawRect = GetDpadDrawRect(_kbRecord[_currFrame - 1]);
            //if (drawRect != _null ) 
            //{
            Game.SpriteBatch.Draw(
                    Debug.Assets.InputHandler_Sheet,
                    new Vector2(20, 80),
                    GetDpadDrawRect(_kbRecord[frame0]),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0f
                );
                Game.SpriteBatch.Draw(
                    Debug.Assets.InputHandler_Sheet,
                    new Vector2(20, 110),
                    GetDpadDrawRect(_kbRecord[frame1]),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0f
                );
                Game.SpriteBatch.Draw(
                    Debug.Assets.InputHandler_Sheet,
                    new Vector2(20, 140),
                    GetDpadDrawRect(_kbRecord[frame2]),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0f
                );
                Game.SpriteBatch.Draw(
                    Debug.Assets.InputHandler_Sheet,
                    new Vector2(20, 170),
                    GetDpadDrawRect(_kbRecord[frame3]),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0f
                );
                Game.SpriteBatch.Draw(
                    Debug.Assets.InputHandler_Sheet,
                    new Vector2(20, 200),
                    GetDpadDrawRect(_kbRecord[frame4]),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0f
                );
            Game.SpriteBatch.DrawString(
                    Debug.Assets.DebugFont,
                    _currFrame.ToString(),
                    new Vector2(60, 90),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0f
                );
            //}
        }

        public Rectangle GetDpadDrawRect(KeyboardState state)
        {
            _hasHistory = false;
            _Xaxis = 0;
            _Yaxis = 0;

            if (_controller.InputDown(state, InputFlags.Left))
            {
                _Xaxis -= 1;
            }
            if (_controller.InputDown(state, InputFlags.Right))
            {
                _Xaxis += 1;
            }
            if (_controller.InputDown(state, InputFlags.Up))
            {
                _Yaxis += 1;
            }
            if (_controller.InputDown(state, InputFlags.Down))
            {
                _Yaxis -= 1;
            }

            if (_Xaxis < 0 && _Yaxis < 0)
            {
                _hasHistory = true;
                return _d1_SpriteIndex;
            }
            if (_Xaxis == 0 && _Yaxis < 0)
            {
                _hasHistory = true;
                return _d2_SpriteIndex;
            }
            if (_Xaxis > 0 && _Yaxis < 0)
            {
                _hasHistory = true;
                return _d3_SpriteIndex;
            }

            if (_Xaxis < 0 && _Yaxis == 0)
            {
                _hasHistory = true;
                return _d4_SpriteIndex;
            }
            if (_Xaxis > 0 && _Yaxis == 0)
            {
                _hasHistory = true;
                return _d6_SpriteIndex;
            }

            if (_Xaxis < 0 && _Yaxis > 0)
            {
                _hasHistory = true;
                return _d7_SpriteIndex;
            }
            if (_Xaxis == 0 && _Yaxis > 0)
            {
                _hasHistory = true;
                return _d8_SpriteIndex;
            }
            if (_Xaxis > 0 && _Yaxis > 0)
            {
                _hasHistory = true;
                return _d9_SpriteIndex;
            }

            return _null;
        }
    }
}
