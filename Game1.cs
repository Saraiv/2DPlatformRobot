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

        //robot
        Vector2 playerPos;
        Robot player;

        //map
        LevelManager levelManager;
        char[,] map;
        public const int tileSize = 64;
        int screenHeight, screenWidth;
        List<Vector2> objectivePointsPos;
        Texture2D wallTexture;

        public List<ICollider> colliders;

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

            player = new Robot(_km, _spriteBatch, Content, screenHeight, GraphicsDevice);

            // TODO: use this.Content to load your game content here
            wallTexture = Content.Load<Texture2D>("Sprites/Wall");

            colliders = new List<ICollider>();
            colliders.Add(player);

            levelManager.LoadLevel(ref screenWidth, ref screenHeight, ref map, tileSize, ref objectivePointsPos, _graphics, _spriteBatch, Content, ref playerPos);
            player.SetPlayerPos(playerPos);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _km.Update();
            player.Update(gameTime);

            for (int i = 0; i < colliders.Count - 1; i++)
            {
                for (int j = i + 1; j < colliders.Count; j++)
                {
                    if (colliders[i].CollidesWith(colliders[j]))
                    {
                        colliders[i].CollisionWith(colliders[j]);
                        colliders[j].CollisionWith(colliders[i]);
                    }
                }
            }

            if (_km.IsKeyPressed(Keys.R))
            {
                levelManager.LoadLevel(ref screenWidth, ref screenHeight, ref map, tileSize, ref objectivePointsPos, _graphics, _spriteBatch, Content, ref playerPos);
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
                for (int y = 0; y < screenHeight; y++)
                {
                    char currentSymbol = map[x, y];
                    switch (currentSymbol)
                    {
                        case 'X':
                            _spriteBatch.Draw(wallTexture, new Vector2(x, y) * tileSize, Color.White);
                            break;
                        default:
                            break;
                    }
                }
            player.Draw();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
