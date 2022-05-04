﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using _2DPlatformerRobot.Screens;

namespace _2DPlatformerRobot.Manager
{
    class ScreenManager
    {
        private IScreen _activeScreen;
        private IScreen _nextScreen;

        public void SetScreen(IScreen screen)
        {
            _nextScreen = screen;
        }

        public void SwitchScreen()
        {
            if(_nextScreen != null)
            {
                _activeScreen = _nextScreen;
            }

            _nextScreen = null;
        }

        public void Update(float delta)
        {
            _activeScreen?.Update(delta);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            _activeScreen?.Draw(spriteBatch);
        }
    }
}