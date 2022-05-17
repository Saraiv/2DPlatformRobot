using System;
using System.Collections.Generic;
using System.Text;

namespace _2DPlatformerRobot.Collider
{
    class CollisionManager
    {
        public static CollisionManager instance;
        private const int tileSize = 64;

        public CollisionManager()
        {
            if(instance == null)
            {
                instance = this;
            }
        }

        //public static bool isColiding(Sprite spriteUm, Sprite spriteDois)
        //{
        //    if(spriteUm.position)
        //}
    }
}
