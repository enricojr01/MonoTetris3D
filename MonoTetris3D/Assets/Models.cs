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

        public static void DrawCube(Matrix world, Color color)
        {
            foreach (ModelMesh m in CubeObject.Meshes)
            {
                foreach (ModelMeshPart part in m.MeshParts)
                {
                    part.Effect = GameRoot.BasicEffect;
                    GameRoot.BasicEffect.World = world;
                    GameRoot.BasicEffect.DiffuseColor = color.ToVector3();
                }
                m.Draw();
            }
        }
    }
}
