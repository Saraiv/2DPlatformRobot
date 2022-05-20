using _2DPlatformerRobot.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2DPlatformerRobot.UI
{
    public class Health
    {
        private SpriteFont font;
        public Health(Game1 game)
        {
            font = game.Content.Load<SpriteFont>("../../../Content/Fonts/Score");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Health: " + Robot._instance.health, new Vector2(240, 70), Color.Black);
        }
    }
}
