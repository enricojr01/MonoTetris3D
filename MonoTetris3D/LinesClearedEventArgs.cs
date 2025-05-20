using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTetris3D
{
    internal class LinesClearedEventArgs : EventArgs
    {
        public int NumberOfClearedLines;
    }
}
