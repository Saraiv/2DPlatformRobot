using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2DPlatformerRobot.Models
{
    public class Coins
    {
        Game1 game;
        public Vector2 position;
        public int tileSize = 64;

        public Coins(Game1 game, Vector2 position)
        {
            this.game = game;
            this.position = position;
        }

        //Collisions
        public bool IsColliding(Vector2 position)
        {
            return Vector2.Distance(this.position, position) < tileSize - 10f;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D coinTexture)
        {
            spriteBatch.Draw(coinTexture, position, Color.White);
        }
    }
}
