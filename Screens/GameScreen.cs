using _2DPlatformerRobot.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DPlatformerRobot.Screens
{
    class GameScreen : IScreen
    {
        private Texture2D robotPlayerModel;
        public GameScreen(Texture2D robotPlayerModel)
        {
            this.robotPlayerModel = robotPlayerModel;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public void Update(float delta)
        {
            
        }
    }
}
