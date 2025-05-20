using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MonoTetris3D
{
    internal class InputManager
    {
        private KeyboardState _oldState, _newState;

        public enum Controls
        {
            Left, Right, SoftDrop,
            HardDrop, RotateCW, RotateCCW
        }

        Dictionary<Controls, Keys> ControlScheme;

        public InputManager()
        {
            _newState = Keyboard.GetState();
            _oldState = _newState;

            ControlScheme = new Dictionary<Controls, Keys>
            {
                { Controls.Left, Keys.Left },
                { Controls.Right, Keys.Right },
                { Controls.SoftDrop, Keys.Down },
                { Controls.HardDrop, Keys.Up },
                { Controls.RotateCW, Keys.LeftControl },
                { Controls.RotateCCW, Keys.LeftShift }
            };
        } 

        public void Update()
        {
            _oldState = _newState;
            _newState = Keyboard.GetState();
        }

        public bool IsPressed(Controls key)
        {
            if (!ControlScheme.ContainsKey(key)) { throw new System.Exception("Control key doesn't exist."); }
            Keys k = ControlScheme[key];
            return _oldState.IsKeyUp(k) && _newState.IsKeyDown(k);
        }

        public bool IsDown(Controls key)
        {
            if (!ControlScheme.ContainsKey(key)) { throw new System.Exception("Control key doesn't exist."); }
            Keys k = ControlScheme[key];
            return _newState.IsKeyDown(k);
        }

        public bool IsReleased(Controls key)
        {
            if (!ControlScheme.ContainsKey(key)) { throw new System.Exception("Control key doesn't exist."); }
            Keys k = ControlScheme[key];
            return _oldState.IsKeyDown(k) && _newState.IsKeyUp(k);
        }
    }
}
