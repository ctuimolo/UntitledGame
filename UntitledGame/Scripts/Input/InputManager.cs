using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;


namespace UntitledGame.Input
{
    public enum InputFlags
    {
        Up,
        Down,
        Left,
        Right,
        Button1,
        Button2,
        Button3,
        Button4,
        Button5,
        Button6,
        Button7,
        Button8,
        Button9,
        Button10,
        Button11,
        Button12,
        Escape,
        Debug1,
        Debug2,
        Debug3,
        Debug4,
    }

    public class InputManager
    {
        private Dictionary<InputFlags, Keys>        _keyboardDefinitions;
        private Dictionary<InputFlags, Buttons>     _buttonDefinitions;

        private int     _gpIndex;
        private bool    _gpEnabled = false;

        private KeyboardState   _kbState;
        private GamePadState    _gpState;

        private KeyboardState _oldKbState;
        private GamePadState  _oldGpState;

        public InputManager()
        {
            _keyboardDefinitions = new Dictionary<InputFlags, Keys>();
            _buttonDefinitions   = new Dictionary<InputFlags, Buttons>();

            LoadDefaultMapping();
        }

        public KeyboardState GetKBState()
        {
            return _kbState;
        }

        public GamePadState GetGPState()
        {
            return _gpState;
        }

        private void LoadDefaultMapping()
        {
            _keyboardDefinitions[InputFlags.Left]       = Keys.Left;
            _keyboardDefinitions[InputFlags.Right]      = Keys.Right;
            _keyboardDefinitions[InputFlags.Up]         = Keys.Up;
            _keyboardDefinitions[InputFlags.Down]       = Keys.Down;
            _keyboardDefinitions[InputFlags.Button1]    = Keys.Z;
            _keyboardDefinitions[InputFlags.Button2]    = Keys.X;
            _keyboardDefinitions[InputFlags.Button3]    = Keys.A;
            _keyboardDefinitions[InputFlags.Button4]    = Keys.S;
            _keyboardDefinitions[InputFlags.Button9]    = Keys.P;
            _keyboardDefinitions[InputFlags.Button10]   = Keys.O;
            _keyboardDefinitions[InputFlags.Escape]     = Keys.Escape;
            _keyboardDefinitions[InputFlags.Debug1]     = Keys.F1;
            _keyboardDefinitions[InputFlags.Debug2]     = Keys.F2;
            _keyboardDefinitions[InputFlags.Debug3]     = Keys.F3;
            _keyboardDefinitions[InputFlags.Debug4]     = Keys.F4;
        }

        private void ParseKeyboardInputs()
        {
            _oldKbState = _kbState;
            _kbState = Keyboard.GetState();        
        }

        private void ParseGamepadInputs()
        {
            _oldGpState = _gpState;
            _gpState = GamePad.GetState(_gpIndex);
        }

        public void SetGamePadIndex(int toindex)
        {
            _gpIndex = toindex;
        }

        public void ParseInput()
        {
            ParseKeyboardInputs();
            ParseGamepadInputs();
        }

        public bool InputDown(InputFlags key)
        {
            if(_kbState.IsKeyDown(_keyboardDefinitions[key]))
            {
                return true;
            }

            if(_gpEnabled)
            {
                if (_gpState.IsButtonDown(_buttonDefinitions[key]))
                {
                    return true;
                }
            }

            return false;
        }

        public bool InputDown(KeyboardState kbState, InputFlags key)
        {
            if (kbState.IsKeyDown(_keyboardDefinitions[key]))
            {
                return true;
            }

            return false;
        }

        public bool InputPressed(InputFlags key)
        {
            if (_kbState.IsKeyDown(_keyboardDefinitions[key]) && !_oldKbState.IsKeyDown(_keyboardDefinitions[key]))
            {
                return true;
            }

            if(_gpEnabled)
            {
                if (_gpState.IsButtonDown(_buttonDefinitions[key]) && !_gpState.IsButtonDown(_buttonDefinitions[key]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
