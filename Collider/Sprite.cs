using _2DPlatformerRobot.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DPlatformerRobot.Collider
{
    class Sprite
    {
        private Texture2D texture;

        public Vector2 position;
        public Vector2 velocity;
        public float speed;
        KeyboardManager km;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Update(GameTime gameTime, List<Sprite> spriteList)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        //Collisions
        public bool IsTouchingLeft(Sprite textures)
        {
            return this.Rectangle.Right + this.velocity.X > textures.Rectangle.Left &&
                this.Rectangle.Left < textures.Rectangle.Left &&
                this.Rectangle.Bottom > textures.Rectangle.Top &&
                this.Rectangle.Top < textures.Rectangle.Bottom;
        }

        public bool IsTouchingRight(Sprite textures)
        {
            return this.Rectangle.Left + this.velocity.X < textures.Rectangle.Right &&
                this.Rectangle.Right > textures.Rectangle.Right &&
                this.Rectangle.Bottom > textures.Rectangle.Top &&
                this.Rectangle.Top < textures.Rectangle.Bottom;
        }

        public bool IsTouchingTop(Sprite textures)
        {
            return this.Rectangle.Bottom + this.velocity.Y < textures.Rectangle.Top &&
                this.Rectangle.Top < textures.Rectangle.Top &&
                this.Rectangle.Right > textures.Rectangle.Left &&
                this.Rectangle.Left < textures.Rectangle.Right;
        }

        public bool IsTouchingBottom(Sprite textures)
        {
            return this.Rectangle.Top + this.velocity.Y < textures.Rectangle.Bottom &&
                this.Rectangle.Bottom > textures.Rectangle.Bottom &&
                this.Rectangle.Right > textures.Rectangle.Left &&
                this.Rectangle.Left < textures.Rectangle.Right;
        }
    }
}
