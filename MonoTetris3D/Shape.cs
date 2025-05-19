using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTetris3D
{
    internal class Shape
    {
        public bool[][] shapeBit;

        public Shape(bool[][] shapeBits)
        {
            shapeBit = shapeBits;
        }

        public Shape(string[] shapedefinition)
        {
            shapeBit = new bool[shapedefinition.Length][];
            int j = 0;
            foreach (string s in shapedefinition)
            {
                shapeBit[j] = new bool[shapedefinition.Length];
                int i = 0;
                foreach (char c in s)
                {
                    shapeBit[j][i] = (c == '1');
                    i++;
                }
                j++;
            }
        }
    }
}
