using _2DPlatformerRobot.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _2DPlatformerRobot.Manager
{
    class LevelManager
    {
        Game1 game;

        private Texture2D wallTexture, lavaTexture, gearTexture;
        private List<Wall> walls;
        private char[,] map;
        private int screenWidth, screenHeight;
        public int tileSize = 64;

        public Robot player;

        public LevelManager(Game1 game, string[] levelFile)
        {
            this.game = game;
            walls = new List<Wall>();

            if (game.currentLevel >= levelFile.Length) return;

            string[] lines = File.ReadAllLines(levelFile[1]);
            map = new char[lines[0].Length, lines.Length];

            Rectangle position = new Rectangle(0, 0, tileSize, tileSize);
            for (int x = 0; x < lines[0].Length; x++)
                for (int y = 0; y < lines.Length; y++)
                {
                    position.X = x * tileSize;
                    position.Y = y * tileSize;

                    string currentLine = lines[y];
                    map[x, y] = currentLine[x];
                    if (currentLine[x] == 'X')
                        walls.Add(new Wall(wallTexture));
                    if (currentLine[x] == 'l') { }
                    if (currentLine[x] == 'c') { }
                    if (currentLine[x] == 'i')
                        Robot._instance.position = new Vector2(x, y);


                    screenHeight = lines.Length;
                    screenWidth = lines[0].Length;

                    game.graphics.PreferredBackBufferHeight = screenHeight * tileSize;
                    game.graphics.PreferredBackBufferWidth = screenWidth * tileSize;
                    game.graphics.ApplyChanges();


                }
        }

        public void LoadLevelTextures()
        {
            wallTexture = game.Content.Load<Texture2D>("Sprites/Wall");
            lavaTexture = game.Content.Load<Texture2D>("Sprites/Lava");
            gearTexture = game.Content.Load<Texture2D>("Sprites/gear");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var wall in walls)
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
                                spriteBatch.Draw(gearTexture, new Vector2(x, y) * tileSize, Color.White);
                                break;
                            default:
                                break;
                        }
                    }
                }
        }
    }
}
