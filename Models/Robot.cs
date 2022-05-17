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

        private Texture2D robotTexture;
        private Vector2 futurePos;
        public static Robot _instance;
        public Vector2 velocity;
        public Vector2 position;
        public float speed = 3f;
        public float gravity = 2f;

        public List<Wall> walls;


        //Jump
        public bool isOnGround = true;
        private const int mSecsToMaxJump = 1000;
        private double timeOffGround = 0;

        enum Direction{
            Left,
            Right
        }

        public Robot(Game1 game)
        {
            if(_instance != null) throw new Exception("Player cons called twice");
            _instance = this;
            this.game = game;
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

            if (game.km.IsKeyHeld(Keys.Space) && timeOffGround < mSecsToMaxJump)
            {
                velocity.Y = -speed;
                isOnGround = false;
            }

            if (game.km.IsKeyUp(Keys.Space))
            {
                velocity.Y = speed / 2f;
                timeOffGround -= gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (isOnGround)
                timeOffGround = 0;
            else
                timeOffGround += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (!isOnGround && timeOffGround > 1000)
            {
                velocity.Y = speed / 2f;
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
        }

        public bool CanMove(List<Wall> walls)
        {
            if (walls == null) return true;
            foreach(var wall in walls)
            {
                if (wall.IsColliding(futurePos))
                {
                    isOnGround = true;
                    return false;
                }
            }
            return true;
        }

        public bool IsGrounded()
        {
            if (isOnGround) return true;
            return false;
        }
    }
}
