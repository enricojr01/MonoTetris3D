using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTetris3D
{
    internal class MarathonGame : IScene
    {
        private Player _player;
        private Playfield _playfield;

        public MarathonGame()
        {
            _playfield = new Playfield(new Vector3(-2f, 4f, 0));
            _player = new Player(_playfield);
        }

        public void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _player.Draw();
        }
    }
}
