using _2DPlatformerRobot.Collider;
using _2DPlatformerRobot.Manager;
using _2DPlatformerRobot.Models;
using _2DPlatformerRobot.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace _2DPlatformerRobot
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        KeyboardManager _km;
        private List<Sprite> textures;

        //robot
        Vector2 playerPos;
        Robot player;
        Texture2D robotTexture;

        //map
        LevelManager levelManager;
        char[,] map;
        public const int tileSize = 64;
        int screenHeight, screenWidth;
        List<Vector2> objectivePointsPos;
        Wall wall;
        Texture2D wallTexture;
        Texture2D lavaTexture;
        Texture2D gearTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _km = new KeyboardManager();
            levelManager = new LevelManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            textures = new List<Sprite>();

            robotTexture = Content.Load<Texture2D>("Sprites/robotRight");
            
            player = new Robot(robotTexture, _km, _spriteBatch, Content, GraphicsDevice);

            // TODO: use this.Content to load your game content here
            wallTexture = Content.Load<Texture2D>("Sprites/Wall");
            lavaTexture = Content.Load<Texture2D>("Sprites/Lava");
            gearTexture = Content.Load<Texture2D>("Sprites/gear");

            wall = new Wall(wallTexture);

            levelManager.LoadLevel(ref screenWidth, ref screenHeight, ref map, tileSize, ref objectivePointsPos, _graphics, _spriteBatch, Content, ref playerPos, wall);
            player.SetPlayerPos(playerPos);

            textures.Add(player);
            textures.Add(wall);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            foreach (var texture in textures)
            {
                texture.Update(gameTime, textures);
            }

            _km.Update();
            player.Update(gameTime, textures);
            wall.Update(gameTime, textures);

            if (_km.IsKeyPressed(Keys.R))
            {
                levelManager.LoadLevel(ref screenWidth, ref screenHeight, ref map, tileSize, ref objectivePointsPos, _graphics, _spriteBatch, Content, ref playerPos, wall);
                player.SetPlayerPos(playerPos);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            _spriteBatch.Begin();
            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    char currentSymbol = map[x, y];
                    switch (currentSymbol)
                    {
                        case 'X':
                            wall.Draw(_spriteBatch, new Vector2(x, y) * tileSize); 
                            //_spriteBatch.Draw(wallTexture, new Vector2(x, y) * tileSize, Color.White);
                            break;
                        case 'l':
                            _spriteBatch.Draw(lavaTexture, new Vector2(x, y) * tileSize, Color.White);
                            break;
                        case 'c':
                            _spriteBatch.Draw(gearTexture, new Vector2(x, y) * tileSize, Color.White);
                            break;  
                        default:
                            break;
                    }
                }
            }

            //foreach (var texture in textures)
            //    texture.Draw(_spriteBatch);

            player.Draw();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
