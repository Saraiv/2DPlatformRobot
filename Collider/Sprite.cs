using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace _2DPlatformerRobot.Collider
{
    public class Sprite
    {
        private Texture2D texture;

        public Vector2 position;
        public Vector2 velocity;
        public float speed = 2f;
        public int tileSize = 64;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }

        public Sprite()
        {

        }

        public virtual void Update(GameTime gameTime, List<Sprite> spriteList)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        //Collisions
        public bool IsColliding(Vector2 position)
        {
            return Vector2.Distance(this.position, position) < tileSize;
        }
    }
}
