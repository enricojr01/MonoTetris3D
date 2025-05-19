using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTetris3D
{
    internal class TitleScreen : IScene
    {
        private float _angle;
        
        public TitleScreen() { }

        public void Update(GameTime gameTime)
        {
            // When update is called, the _angle attribute is increased
            // at a rate of 0.75 radians per second, and clamped to the
            // 2 PI bound. I have no idea how the 2 PI bound is significant
            _angle += 0.75f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _angle %= MathHelper.TwoPi;
        }
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw cube mesh
            foreach (ModelMesh m in Assets.Models.CubeObject.Meshes)
            {
                foreach (ModelMeshPart part in m.MeshParts)
                {
                    part.Effect = GameRoot.BasicEffect;
                    GameRoot.BasicEffect.World = (
                        Matrix.CreateScale(5) *
                        Matrix.CreateRotationY(_angle) *
                        Matrix.CreateTranslation(0, 0, -3f)
                    );
                    GameRoot.BasicEffect.DiffuseColor = Color.Red.ToVector3();
                }
                m.Draw();
            }

        }
    }
}
