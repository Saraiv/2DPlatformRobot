using _2DPlatformerRobot.Manager;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _2DPlatformerRobot.Models
{
    class Robot
    {
        private Texture2D robotTexture;
        private KeyboardManager km;
        private SpriteBatch spriteBatch;
        private ContentManager content;
        private Rectangle robotRectangle;
        bool hasJumped;

        public Robot(KeyboardManager km, SpriteBatch spriteBatch, ContentManager content, string robotImage, Rectangle robotRectangle)
        {
            this.km = km;
            this.spriteBatch = spriteBatch;
            this.content = content;
            robotTexture = content.Load<Texture2D>(robotImage);
            this.robotRectangle = robotRectangle;
            hasJumped = false;
        }

        public void Movement()
        {
            if (km.IsKeyHeld(Keys.A) || km.IsKeyHeld(Keys.Left))
            {
                robotRectangle.X -= 3;
            }
            if (km.IsKeyHeld(Keys.D) || km.IsKeyHeld(Keys.Right))
            {
                robotRectangle.X += 3;
            }
            if (km.IsKeyPressed(Keys.Space) && hasJumped == false)
            {
                robotRectangle.Y -= 100;
                hasJumped = true;
            }

            hasJumped = false;
        }

        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(robotTexture, robotRectangle, Color.White);
            spriteBatch.End();
        }

        public void Update()
        {

        }
    }
}
