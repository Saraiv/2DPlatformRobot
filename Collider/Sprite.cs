using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace _2DPlatformerRobot.Collider
{
    public class Sprite
    {
        public Vector2 position;
        public int tileSize = 64;

        public Sprite()
        {

        }

        public virtual void Update(GameTime gameTime, List<Sprite> spriteList)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        //Collisions
        public bool IsColliding(Vector2 position)
        {
            return Vector2.Distance(this.position, position) < tileSize;
        }
    }
}
