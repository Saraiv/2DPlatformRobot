using _2DPlatformerRobot.Collider;
using _2DPlatformerRobot.Manager;
using _2DPlatformerRobot.Models;
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

        //textures
        public List<Sprite> textures;
        public int currentLevel;

        //robot
        Robot player;

        //map
        public string[] levels = { "../../../Content/Level/level1.txt",
                            "../../../Content/Level/level2.txt"};
        LevelManager levelManager;
        public List<Wall> walls;

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
            
            
            //player.SetPlayerPos(playerPos);
            //textures.Add(player);
            //foreach(var wall in walls)
            //    textures.Add(wall);
        }

        protected override void Update(GameTime gameTime)
        {
            

            // TODO: Add your update logic here

            km.Update();

            if (km.IsKeyPressed(Keys.R))
                Initialize();

            if (km.IsKeyPressed(Keys.Escape))
                Exit();

            player.Update(gameTime, textures);


            //player.Update(gameTime, textures);
            //foreach (var wall in walls)
            //    wall.Update(gameTime, textures);

            //foreach (var texture in textures)
            //    texture.Update(gameTime, textures);




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MediumPurple);

            // TODO: Add your drawing code here
            
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            levelManager.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
