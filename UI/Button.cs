using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DPlatformerRobot.UI
{
    class Button
    {
        Game1 game;
        Texture2D buttonTexture;
        Vector2 position;
        Rectangle buttonRectangle;
        public Vector2 buttonSize;
        Color color = new Color(255, 255, 255, 255);
        public bool down;
        public bool isClicked;

        public Button(Game1 game)
        {
            this.game = game;
            buttonTexture = game.Content.Load<Texture2D>("Sprites/StartButton");
            buttonSize = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 5);
        }

        public void Update(MouseState mouse)
        {
            buttonRectangle = new Rectangle((int)position.X, (int)position.Y, buttonTexture.Width, buttonTexture.Height);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(buttonRectangle))
            {
                if (color.A == 255) down = false;
                if (color.A == 0) down = true;
                if (down) color.A += 3;
                else color.A -= 3;
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
            }
            else if (color.A < 255)
            {
                color.A += 3;
                isClicked = false;
            }
        }

        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonTexture, buttonRectangle, color);
        }
    }
}
