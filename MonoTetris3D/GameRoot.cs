using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoTetris3D.Assets;

namespace MonoTetris3D
{
    public class GameRoot : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static SceneManager SceneManager;
        public static BasicEffect BasicEffect;
        public static Camera Camera;

        public GameRoot()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.IsFullScreen = false;
            IsMouseVisible = true;

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SceneManager = new SceneManager();

            // TODO: use this.Content to load your game content here
            Models.Initialize(Content);
            Camera = new Camera(
                new Vector3(0, 0, 5),
                new Vector3(0, 0, -5),
                _graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight,
                MathHelper.ToRadians(60)
            );

            BasicEffect = new BasicEffect(GraphicsDevice);
            BasicEffect.View = Camera.View;
            BasicEffect.Projection = Camera.Projection;

            BasicEffect.EnableDefaultLighting();
            BasicEffect.PreferPerPixelLighting = true;
            BasicEffect.SpecularPower = 16f;

            TitleScreen titleScreen = new TitleScreen();
            SceneManager.PushScene(titleScreen);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            SceneManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            SceneManager.Draw(_spriteBatch, gameTime);
            base.Draw(gameTime);
        }
    }
}
