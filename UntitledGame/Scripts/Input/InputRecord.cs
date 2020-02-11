using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;

namespace UntitledGame.Input
{
    public struct HistoricalKBState
    {
        public KeyboardState State;
        public int FrameStamp;
    }

    public class InputRecord
    {
        private HistoricalKBState[] _kbRecord;
        private GamePadState[]      _gpRecord;
        private int _recordSize     = 8192;
        private int _maxFrame       = 8192;
        private int _currFrame      = -1;
        private int _historyCount   = 0;

        private InputManager _controller;

        private int _Xaxis;
        private int _Yaxis;

        public InputRecord(InputManager controller)
        {
            _controller = controller;
        }

        public void InitKBRecord()
        {
            _kbRecord = new HistoricalKBState[_recordSize];
        }

        public void InitGPRecord()
        {
            _gpRecord = new GamePadState[_recordSize];
        }

        public void UpdateInputRecordIndex()
        {
            if(_currFrame < _maxFrame)
                _currFrame++;
        }

        public void InsertKBRecord(KeyboardState state)
        {
            if(_currFrame < _recordSize)
            {
                _kbRecord[_currFrame].State      = state;
                _kbRecord[_currFrame].FrameStamp = _currFrame;
                _currFrame = -1;
                _historyCount++;
            }
        }

        public void DrawHistory()
        {
            int frame0 = (_historyCount - 1 > 0) ? _historyCount - 1 : 0;
            int frame1 = (_historyCount - 2 > 0) ? _historyCount - 2 : 0;
            int frame2 = (_historyCount - 3 > 0) ? _historyCount - 3 : 0;
            int frame3 = (_historyCount - 4 > 0) ? _historyCount - 4 : 0;
            int frame4 = (_historyCount - 5 > 0) ? _historyCount - 5 : 0;

            Game.SpriteBatch.Draw(
                Debug.Assets.InputHandler_Sheet,
                new Vector2(20, 80),
                GetDpadDrawRect(_kbRecord[frame0].State),
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
                GetDpadDrawRect(_kbRecord[frame1].State),
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
                GetDpadDrawRect(_kbRecord[frame2].State),
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
                GetDpadDrawRect(_kbRecord[frame3].State),
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
                GetDpadDrawRect(_kbRecord[frame4].State),
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
        }

        public Rectangle GetDpadDrawRect(KeyboardState state)
        {
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
                return Debug.Assets.D1_SpriteIndex;
            }
            if (_Xaxis == 0 && _Yaxis < 0)
            {
                return Debug.Assets.D2_SpriteIndex;
            }
            if (_Xaxis > 0 && _Yaxis < 0)
            {
                return Debug.Assets.D3_SpriteIndex;
            }

            if (_Xaxis < 0 && _Yaxis == 0)
            {
                return Debug.Assets.D4_SpriteIndex;
            }
            if (_Xaxis > 0 && _Yaxis == 0)
            {
                return Debug.Assets.D6_SpriteIndex;
            }

            if (_Xaxis < 0 && _Yaxis > 0)
            {
                return Debug.Assets.D7_SpriteIndex;
            }
            if (_Xaxis == 0 && _Yaxis > 0)
            {
                return Debug.Assets.D8_SpriteIndex;
            }
            if (_Xaxis > 0 && _Yaxis > 0)
            {
                return Debug.Assets.D9_SpriteIndex;
            }

            return Debug.Assets.D_Null;
        }
    }
}
