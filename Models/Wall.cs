using _2DPlatformerRobot.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace _2DPlatformerRobot.Models
{
    public class Wall : Sprite
    {
        Texture2D wallTexture;

        public Wall(Texture2D wallTexture)
        {
            this.wallTexture = wallTexture;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(wallTexture, position, Color.White);
        }
    }
}
