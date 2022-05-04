using _2DPlatformerRobot.Manager;
using _2DPlatformerRobot.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2DPlatformerRobot
{
    public class Game1 : Game
    {
        public static int screenWidth = 1280;
        public static int screenHeight = 720;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D playerModelRobot;
        private ScreenManager _screenManager;
        KeyboardManager _km;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _screenManager = new ScreenManager();
            _km = new KeyboardManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playerModelRobot = Content.Load<Texture2D>("robot-idle");


            // TODO: use this.Content to load your game content here

            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.ApplyChanges();

            _screenManager.SetScreen(new SplashScreen(playerModelRobot));
            _screenManager.SwitchScreen();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000f;

            _screenManager.Update(delta);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _screenManager.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
