using _2DPlatformerRobot.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2DPlatformerRobot.UI
{
    public class Score
    {
        private SpriteFont font;
        public Score(Game1 game)
        {
            font = game.Content.Load<SpriteFont>("../../../Content/Fonts/Score");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Score: " + Robot._instance.points, new Vector2(70, 70), Color.Black);
        }
    }
}
