using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2DPlatformerRobot
{
    class Background
    {
        Texture2D background;
        float width, height;
        Rectangle screenRectangle;

        public Background(ContentManager content, string backgroundImage, Rectangle screenRectangle)
        {
            background = content.Load<Texture2D>(backgroundImage);
            this.screenRectangle = screenRectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, screenRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
