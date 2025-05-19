using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoTetris3D
{
    public class SceneManager
    {
        private Stack<IScene> _scenes;

        public SceneManager()
        {
            _scenes = new Stack<IScene>();
        }

        public void PushScene(IScene scene)
        {
            _scenes.Push(scene);
        }

        public void PopScene()
        {
            if(_scenes.Count > 0)
            {
                _scenes.Pop();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (_scenes.Count > 0)
            {
                _scenes.Peek().Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_scenes.Count > 0)
            {
                _scenes.Peek().Draw(spriteBatch, gameTime);
            }
        }
    }
}
