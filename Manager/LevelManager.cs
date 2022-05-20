using _2DPlatformerRobot.Models;
using _2DPlatformerRobot.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _2DPlatformerRobot.Manager
{
    public class LevelManager
    {
        Game1 game;
        public Score score;
        public Health health;
        public Texture2D wallTexture, lavaTexture, coinTexture;
        public List<Wall> walls;
        public List<Lava> lavas;
        public List<Coins> coins;
        public Coins selectedCoin;
        public char[,] map;
        public int screenWidth, screenHeight;
        public int tileSize = 64;

        public LevelManager(Game1 game, string[] levelFile)
        {
            this.game = game;

            if (game.currentLevel >= levelFile.Length) return;

            walls = new List<Wall>();
            lavas = new List<Lava>();
            coins = new List<Coins>();
            score = new Score(game);
            health = new Health(game);
            Robot._instance.points = 0;

            string[] lines = File.ReadAllLines(levelFile[game.currentLevel]);
            map = new char[lines[0].Length, lines.Length];

            Rectangle position = new Rectangle(0, 0, tileSize, tileSize);
            for (int x = 0; x < lines[0].Length; x++)
            {
                for (int y = 0; y < lines.Length; y++)
                {
                    position.X = x * tileSize;
                    position.Y = y * tileSize;

                    string currentLine = lines[y];
                    map[x, y] = currentLine[x];

                    char currentSymbol = map[x, y];

                    switch (currentSymbol)
                    {
                        case 'X':
                            walls.Add(new Wall(game, new Vector2(x * tileSize, y * tileSize)));
                            break;
                        case 'l':
                            lavas.Add(new Lava(game, new Vector2(x * tileSize, y * tileSize)));
                            break;
                        case 'c':
                            coins.Add(new Coins(game, new Vector2(x * tileSize, y * tileSize)));
                            break;
                        case 'i':
                            Robot._instance.SetPlayerPos(new Vector2(x * tileSize, y * tileSize));
                            break;
                        default:
                            break;
                    }
                }
            }

            Robot._instance.walls = walls;
            Robot._instance.lavas = lavas;
            Robot._instance.coins = coins;

            screenHeight = lines.Length;
            screenWidth = lines[0].Length;

            game.graphics.PreferredBackBufferHeight = screenHeight * tileSize;
            game.graphics.PreferredBackBufferWidth = screenWidth * tileSize;
            game.graphics.ApplyChanges();
        }

        public bool NextLevel()
        {
            if (Robot._instance.points == 5)
            {
                game.currentLevel++;
                return true;
            } 
            else return false;
        }

        public void LoadLevelTextures()
        {
            wallTexture = game.Content.Load<Texture2D>("Sprites/Wall");
            lavaTexture = game.Content.Load<Texture2D>("Sprites/Lava");
            coinTexture = game.Content.Load<Texture2D>("Sprites/Coin");
        }

        public void UnloadTexture()
        {
            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    char currentSymbol = map[x, y];
                    switch (currentSymbol)
                    {
                        case 'c':
                            if (Robot._instance.CoinPickUp(coins))
                            {
                                selectedCoin = Robot._instance.GetCoin(coins);
                                coins.Remove(selectedCoin);
                                break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    char currentSymbol = map[x, y];
                    switch (currentSymbol)
                    {
                        case 'X':
                            spriteBatch.Draw(wallTexture, new Vector2(x, y) * tileSize, Color.White);
                            break;
                        case 'l':
                            spriteBatch.Draw(lavaTexture, new Vector2(x, y) * tileSize, Color.White);
                            break;
                        case 'c':
                            spriteBatch.Draw(coinTexture, new Vector2(x, y) * tileSize, Color.White);
                            break;
                        default:
                            break;
                    }
                }
            }
            if(score != null)
                score.Draw(spriteBatch);
            if (health != null)
                health.Draw(spriteBatch);
        }
    }
}
