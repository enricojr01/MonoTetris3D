using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
    
namespace MonoTetris3D
{
    internal class Tetrimino
    {
        public Shape[] Shapes;
        private int _shapeID;
        public Color Color;
        public Tetriminoes ShapeType;

        public Tetrimino()
        {
            _shapeID = 0;
        }

        public Shape CurrentShape {  get { return Shapes[_shapeID]; } }
        public void SetShape(Shape[] shapes, Tetriminoes type, Color color)
        {
            Shapes = shapes;
            ShapeType = type;
            Color = color;
        }

        public void RotateLeft()
        {
            _shapeID = (_shapeID + 1) % Shapes.Length;
        }

        public void RotateRight()
        {
            _shapeID = (_shapeID - 1 + Shapes.Length) % Shapes.Length;
        }

        public void Draw(Matrix world, float alpha=1)
        {
            for (int y = 0; y < CurrentShape.shapeBit.Length; y++)
            {
                for (int x = 0; x < CurrentShape.shapeBit[y].Length; x++)
                {
                    if (CurrentShape.shapeBit[y][x] == false)
                    {
                        continue;
                    }
                    foreach (ModelMesh m in Assets.Models.CubeObject.Meshes)
                    {
                        foreach (ModelMeshPart part in m.MeshParts)
                        {
                            part.Effect = GameRoot.BasicEffect;
                            GameRoot.BasicEffect.World = Matrix.CreateTranslation(0.4f * x, 0.4f * -y, 0) * world;
                            GameRoot.BasicEffect.DiffuseColor = Color.ToVector3();
                            GameRoot.BasicEffect.Alpha = alpha;
                        }
                        m.Draw();
                    }
                }
            }
        }
    }
}
