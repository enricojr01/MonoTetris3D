using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTetris3D
{
    internal class TitleScreen : IScene
    {
        private float _angle;
        private Matrix _world;

        private Tetrimino _tetrimino;
        
        public TitleScreen() 
        {
            TetriminoFactory factory = new TetriminoFactory();
            _tetrimino = factory.Generate(Tetriminoes.J);
        }

        public void Update(GameTime gameTime)
        {
            // When update is called, the _angle attribute is increased
            // at a rate of 0.75 radians per second, and clamped to the
            // 2 PI bound. I have no idea how the 2 PI bound is significant
            _angle += 0.75f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _angle %= MathHelper.TwoPi;

            _world = Matrix.CreateScale(5) * Matrix.CreateRotationY(_angle) * Matrix.CreateTranslation(0, 0, -3f);
        }
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _tetrimino.Draw(_world);
        }
    }
}
