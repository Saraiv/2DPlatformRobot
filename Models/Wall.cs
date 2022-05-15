using _2DPlatformerRobot.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _2DPlatformerRobot.Models
{
    class Wall : Sprite
    {
        Texture2D wallTexture;

        public Wall(Texture2D wallTexture) : base(wallTexture)
        {
            this.wallTexture = wallTexture;
        }

        public void Update(GameTime gameTime, List<Sprite> textures)
        {
            foreach (var texture in textures)
            {
                if (texture == this)
                {
                    continue;
                }

                if (this.velocity.X > 0 && this.IsTouchingLeft(texture) ||
                    this.velocity.X < 0 && this.IsTouchingRight(texture))
                {
                    this.velocity.X = 0;
                }

                if (this.velocity.Y > 0 && this.IsTouchingTop(texture) ||
                    this.velocity.Y < 0 && this.IsTouchingBottom(texture))
                {
                    this.velocity.Y = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(wallTexture, position, Color.White);
        }
    }
}
