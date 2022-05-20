using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using _2DPlatformerRobot.Manager;
using Microsoft.Xna.Framework.Audio;

namespace _2DPlatformerRobot.Models
{
    class Robot
    {
        private Game1 game;
        public enum RobotState
        {
            Jumping,
            Falling,
            Standing
        }
        private SoundEffect jump;
        private SoundEffect dead;
        private SoundEffect collectCoin;
        private Texture2D robotTexture;
        private Vector2 futurePos;
        public static Robot _instance;
        public Vector2 velocity;
        public Vector2 position;
        public float speed = 3f;
        public float gravity = 2f;
        public RobotState playerState;
        public List<Wall> walls;
        public List<Lava> lavas;
        public List<Coins> coins;
        public int points = 0;
        public int tileSize = 64;
        public int health = 3;

        //Jump
        public bool isOnGround = true;
        public int nJumps = 0;


        public Robot(Game1 game)
        {
            if (_instance != null) throw new Exception("Player called twice");
            _instance = this;
            this.game = game;
            playerState = RobotState.Standing;
            isOnGround = true;

            position = Vector2.Zero;
        }

        public void LoadTexture()
        {
            robotTexture = game.Content.Load<Texture2D>("Sprites/robotRight");
            jump = game.Content.Load<SoundEffect>("Audio/Jump");
            dead = game.Content.Load<SoundEffect>("Audio/Dead");
            collectCoin = game.Content.Load<SoundEffect>("Audio/CollectCoin");
        }

        public void SetPlayerPos(Vector2 startingPos)
        {
            position = startingPos;
            playerState = RobotState.Standing;
            isOnGround = true;
        }

        private void Movement(GameTime gameTime)
        {
            if (!isOnGround)
                velocity = (velocity + Vector2.UnitY * 2f);
            else nJumps = 0;

            velocity *= 0.4f;

            if (game.km.IsKeyHeld(Keys.A) || game.km.IsKeyHeld(Keys.Left))
            {
                velocity.X += -speed / 2f;
            }

            if (game.km.IsKeyHeld(Keys.D) || game.km.IsKeyHeld(Keys.Right))
            {
                velocity.X += speed / 2f;
            }

            if (game.km.IsKeyPressed(Keys.Space) && (isOnGround || nJumps < 2))
            {
                jump.Play();
                velocity += new Vector2(velocity.X, -30f);
                nJumps++;
                isOnGround = false;
            }

            futurePos = position + velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(robotTexture, position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            Movement(gameTime);

            if (CanMove(walls))
                position = futurePos;

            if (IsDead(lavas))
                game.IsDead();

            if (CoinPickUp(coins))
                points++;

        }

        public bool IsGameOver()
        {
            if (health == 0) return true;
            else return false;
        }

        public bool CanMove(List<Wall> walls)
        {
            if (walls == null) return true;
            foreach (var wall in walls)
            {
                if (wall.IsColliding(futurePos))
                {
                    isOnGround = true;
                    return false;
                }
            }
            return true;
        }

        public bool IsDead(List<Lava> lavas)
        {
            if (lavas == null) return false;
            foreach (var lava in lavas)
            {
                if (lava.IsColliding(futurePos))
                {
                    health--;
                    dead.Play();
                    return true;
                }
            }
            return false;
        }

        public bool CoinPickUp(List<Coins> coins)
        {
            if (coins == null) return false;
            foreach (var coin in coins)
            {
                if (coin.IsColliding(futurePos))
                {
                    game.levelManager.map[(int)coin.position.X / tileSize, (int)coin.position.Y / tileSize] = ' ';
                    collectCoin.Play();
                    return true;
                }
            }
            return false;
        }

        public Coins GetCoin(List<Coins> coins)
        {
            if (coins == null) return null;
            foreach (var coin in coins)
            {
                if (coin.IsColliding(futurePos))
                {
                    return coin;
                }
            }
            return null;
        }

        public bool IsGrounded()
        {
            if (isOnGround) return true;
            return false;
        }
    }
}
