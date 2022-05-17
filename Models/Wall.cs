using _2DPlatformerRobot.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace _2DPlatformerRobot.Models
{
    public class Wall
    {
        Game1 game;
        public Vector2 position;
        public int tileSize = 64;

        public Wall(Game1 game, Vector2 position)
        {
            this.game = game;
            this.position = position;
        }
            
        //Collisions
        public bool IsColliding(Vector2 position)
        {
            return Vector2.Distance(this.position, position) < tileSize;
        }
    }
}
