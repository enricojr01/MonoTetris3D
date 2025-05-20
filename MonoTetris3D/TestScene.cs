using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTetris3D
{
    internal class TestScene : IScene
    {
        private Playfield _playField;
        public TestScene()
        {
            // origin is still top left, even in 3D
            // can't work out how "units" work in 3D, maybe it'll
            // come later.
            _playField = new Playfield(new Vector3(-1f, 2f, 0));

            TetriminoFactory factory = new TetriminoFactory();

            Tetrimino t1 = factory.Generate(Tetriminoes.O);
            Tetrimino t2 = factory.Generate(Tetriminoes.L);
            Tetrimino t3 = factory.Generate(Tetriminoes.Z);

            _playField.LockInPlace(t1, 3, 4);
            _playField.LockInPlace(t2, 6, 7);

            if (_playField.DoesShapeFitHere(t3, 3, 4)) { _playField.LockInPlace(t3, 3, 4); }
            if (_playField.DoesShapeFitHere(t3, 6, 7)) { _playField.LockInPlace(t3, 6, 7); }
            if (_playField.DoesShapeFitHere(t3, 6, 9)) { _playField.LockInPlace(t3, 6, 9); }
        }

        public void Update(GameTime gameTime) { }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _playField.Draw();
        }
    }
}
