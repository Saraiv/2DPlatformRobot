using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2DPlatformerRobot.Screens
{
    class SplashScreen : IScreen
    {
        private Texture2D splashScreenImage;

        public SplashScreen(Texture2D splashScreenImage)
        {
            this.splashScreenImage = splashScreenImage;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            var zero = new Vector2(splashScreenImage.Width / 2f, splashScreenImage.Height / 2f);

            spriteBatch.Draw(splashScreenImage,
                new Vector2(Game1.screenWidth / 2f, Game1.screenHeight / 2f),
                null,
                Color.White,
                0f,
                zero,
                1f,
                SpriteEffects.None,
                0f);
            spriteBatch.End();
        }

        public void Update(float delta)
        {

        }
    }
}
