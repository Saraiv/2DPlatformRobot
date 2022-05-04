using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DPlatformerRobot.Screens
{
    interface IScreen
    {
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
    }
}
