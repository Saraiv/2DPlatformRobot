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
        string[] levels = { "../../../Content/Level/level1.txt" };
        int currentLevel;
        private Texture2D grassTexture;

        public void LoadLevel(ref int screenWidth, ref int screenHeight, ref char[,] map, int tileSize, ref List<Vector2> objectivePointsPos, GraphicsDeviceManager graphics, SpriteBatch spriteBatch, ContentManager content, ref Vector2 playerPos)
        {
            if (currentLevel >= levels.Length) return;

            string[] lines = File.ReadAllLines(levels[currentLevel]);
            map = new char[lines[0].Length, lines.Length];

            grassTexture = content.Load<Texture2D>("Sprites/Grass");

            spriteBatch.Begin();
            Rectangle position = new Rectangle(0, 0, Game1.tileSize, Game1.tileSize);
            for (int x = 0; x < lines[0].Length; x++)
                for (int y = 0; y < lines.Length; y++)
                {
                    position.X = x * Game1.tileSize;
                    position.Y = y * Game1.tileSize;

                    string currentLine = lines[y];
                    map[x, y] = currentLine[x];
                    if (currentLine[x] == 'i')
                        playerPos = new Vector2(x, y + Game1.tileSize * 2);
                    if (currentLine[x] == 'X')
                        spriteBatch.Draw(grassTexture, position, Color.White);
                }

            spriteBatch.End();
            screenHeight = lines.Length;
            screenWidth = lines[0].Length;

            graphics.PreferredBackBufferHeight = screenHeight * tileSize;
            graphics.PreferredBackBufferWidth = screenWidth * tileSize;
            graphics.ApplyChanges();
        }
    }
}
