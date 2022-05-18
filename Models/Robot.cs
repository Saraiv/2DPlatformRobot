using _2DPlatformerRobot.Manager;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using _2DPlatformerRobot.Collider;
using System.Collections.Generic;
using System;

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

        //Jump
        private const float mSecsToMaxJump = 1000;
        private double timeOffGround = 0;
        public bool isOnGround = true;
        public int nJumps = 0;
        

        public Robot(Game1 game)
        {
            if(_instance != null) throw new Exception("Player called twice");
            _instance = this;
            this.game = game;
            playerState = RobotState.Standing;
            isOnGround = true;

            position = Vector2.Zero;
        }

        public void LoadTexture()
        {
            robotTexture = game.Content.Load<Texture2D>("Sprites/robotRight");
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

            velocity = velocity * 0.53f;

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
                velocity += new Vector2(velocity.X, -40f);
                nJumps++;
                isOnGround = false;
            }

            futurePos = position + velocity; // gravity
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

            //if(CoinPickUp(coins))

        }

        public bool CanMove(List<Wall> walls)
        {
            if (walls == null) return true;
            foreach(var wall in walls)
            {
                if (wall.IsColliding(futurePos))
                {
                    //playerState = RobotState.Standing;
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
                    return true;
            }
            return false;
        }

        public bool CoinPickUp(List<Coins> coins)
        {
            if (coins == null) return false;
            foreach(var coin in coins)
            {
                if (coin.IsColliding(futurePos))
                {
                    //Unload texture
                    return true;
                }
            }
            return false;
        }

        public bool IsGrounded()
        {
            //if (playerState == RobotState.Standing) return true;
            if (isOnGround) return true;
            return false;
        }
    }
}
