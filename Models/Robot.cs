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
            velocity = Vector2.Zero;

            if (game.km.IsKeyHeld(Keys.A) || game.km.IsKeyHeld(Keys.Left))
            {
                velocity.X = -speed;
            }

            if (game.km.IsKeyHeld(Keys.D) || game.km.IsKeyHeld(Keys.Right))
            {
                velocity.X = speed;
            }

            //if (game.km.IsKeyHeld(Keys.Space) && timeOffGround < mSecsToMaxJump)
            //{
            //    velocity.Y = -speed * 2f;
            //    timeOffGround += gameTime.ElapsedGameTime.TotalSeconds;
            //    playerState = RobotState.Jumping;
            //}

            //if (playerState == RobotState.Jumping)
            //{
            //    velocity.Y = speed / 2f;
            //    timeOffGround -= gameTime.ElapsedGameTime.TotalSeconds;
            //    if (timeOffGround < 0)
            //        playerState = RobotState.Standing;
            //}
            //else
            //    playerState = RobotState.Falling;

            //if(playerState == RobotState.Falling)
            //{
            //    velocity.Y = speed / 2f;
            //    timeOffGround -= gameTime.ElapsedGameTime.TotalSeconds;
            //    if (timeOffGround < 0)
            //        playerState = RobotState.Standing;
            //}

            if (game.km.IsKeyHeld(Keys.Space) && timeOffGround < mSecsToMaxJump)
            {
                velocity.Y = -speed;
                isOnGround = false;
            }

            if (isOnGround)
                timeOffGround = 0;
            else
                timeOffGround += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (game.km.IsKeyUp(Keys.Space) || !isOnGround && timeOffGround > 1000)
            {
                velocity.Y = speed;
                timeOffGround -= gameTime.ElapsedGameTime.TotalMilliseconds;
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
