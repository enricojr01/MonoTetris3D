using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTetris3D.Assets
{
    internal static class Models
    {
        public static Model CubeObject;

        public static void Initialize(ContentManager content)
        {
            CubeObject = content.Load<Model>("Cube");
        }
    }
}
