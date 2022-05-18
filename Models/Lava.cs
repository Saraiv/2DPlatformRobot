using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DPlatformerRobot.Models
{
    public class Lava
    {
        Game1 game;
        public Vector2 position;
        public int tileSize = 64;

        public Lava(Game1 game, Vector2 position)
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
