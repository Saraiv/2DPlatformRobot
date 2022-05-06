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
        private Rectangle robotRectangle;
        private Vector2 velocity;
        bool hasJumped;

        public Robot(KeyboardManager km, SpriteBatch spriteBatch, Texture2D robotTexture, Rectangle robotRectangle)
        {
            this.km = km;
            this.spriteBatch = spriteBatch;
            this.robotTexture = robotTexture;
            this.robotRectangle = robotRectangle;
            hasJumped = false;
        }

        public void Movement()
        {
            if (km.IsKeyHeld(Keys.A) || km.IsKeyHeld(Keys.Left))
            {
                robotRectangle.X -= 2;
            }
            if (km.IsKeyHeld(Keys.D) || km.IsKeyHeld(Keys.Right))
            {
                robotRectangle.X += 2;
            }
            if (km.IsKeyPressed(Keys.Space) && hasJumped == false)
            {
                robotRectangle.Y -= 10;
                velocity.Y -= 5;
                hasJumped = true;
            }
            if (hasJumped)
            {
                float i = 1;
                velocity.Y += 0.15f * i;
            }
            if (robotRectangle.Y + robotTexture.Height >= 450) hasJumped = false;
            if (hasJumped == false) velocity.Y = 0;

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
