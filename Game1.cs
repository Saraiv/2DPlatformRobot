using _2DPlatformerRobot.Collider;
using _2DPlatformerRobot.Manager;
using _2DPlatformerRobot.Models;
using _2DPlatformerRobot.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace _2DPlatformerRobot
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public KeyboardManager km;

        //robot
        Robot player;

        //map
        public string[] levels = { "../../../Content/Level/level1.txt",
                            "../../../Content/Level/level2.txt"};
        public LevelManager levelManager;
        public int currentLevel = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            km = new KeyboardManager();
            player = new Robot(this);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            levelManager = new LevelManager(this, levels);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            spriteBatch = new SpriteBatch(GraphicsDevice);
            levelManager.LoadLevelTextures();
            player.LoadTexture();
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            km.Update();
            if (currentLevel == 2)
                Exit();
            if (km.IsKeyPressed(Keys.R))
                Initialize();
            if (km.IsKeyPressed(Keys.Escape))
                Exit();
            player.Update(gameTime);
            levelManager.UnloadTexture();
            if (levelManager.NextLevel())
            {
                currentLevel++;
                Initialize();
            }

            if (Robot._instance.IsGameOver())
            {
                currentLevel = 0;
                Robot._instance.health = 3;
                Initialize();
            }

            base.Update(gameTime);
        }

        public void IsDead() => Initialize();

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.MediumPurple);
            spriteBatch.Begin();
            levelManager.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
