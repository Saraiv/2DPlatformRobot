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

        enum GameState
        {
            MainMenu,
            Playing
        }

        GameState CurrentGameState = GameState.MainMenu;

        //Robot
        Robot player;

        //Button
        Button button;

        //Map
        public string[] levels = { "../../../Content/Level/level1.txt",
                            "../../../Content/Level/level2.txt",
                            "../../../Content/Level/level3.txt"};
        public LevelManager levelManager;
        public Score score;
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
            button = new Button(this);
            levelManager = new LevelManager(this, levels);
            score = new Score(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            spriteBatch = new SpriteBatch(GraphicsDevice);
            IsMouseVisible = true;
            button.SetPosition(new Vector2(300, 250));
            levelManager.LoadLevelTextures();
            player.LoadTexture();
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            MouseState mouse = Mouse.GetState();

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    currentLevel = 0;
                    Robot._instance.points = 0;
                    Robot._instance.health = 3;
                    button.Update(mouse);
                    if (button.isClicked == true)
                    {
                        Initialize();
                        CurrentGameState = GameState.Playing;
                    }
                    break;

                case GameState.Playing:
                    km.Update();
                    if (currentLevel == 2)
                        CurrentGameState = GameState.MainMenu;
                    if (km.IsKeyPressed(Keys.R))
                    {
                        currentLevel = 0;
                        Initialize();
                    }
                    if (km.IsKeyPressed(Keys.Escape))
                        CurrentGameState = GameState.MainMenu;

                    player.Update(gameTime);

                    levelManager.UnloadTexture();

                    if (Robot._instance.IsGameOver())
                    {
                        currentLevel = 0;
                        Robot._instance.health = 3;
                        CurrentGameState = GameState.MainMenu;
                    }

                    if (levelManager.NextLevel())
                    {
                        currentLevel++;
                        Initialize();
                    }
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        public void IsDead() => Initialize();

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.MediumPurple);
            spriteBatch.Begin();
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    button.Draw(spriteBatch);
                    break;

                case GameState.Playing:
                    levelManager.Draw(spriteBatch);
                    score.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    break;
                default:
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
