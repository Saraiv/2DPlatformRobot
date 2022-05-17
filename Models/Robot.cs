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
    class Robot : Sprite
    {
        private Game1 game;

        private Texture2D robotTexture;
        private Vector2 futurePos;
        public static Robot _instance;


        //Jump
        private bool isOnGround = true;
        private const int mSecsToMaxJump = 1300;
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

            position = Vector2.Zero;
        }

        public void LoadTexture()
        {
            robotTexture = game.Content.Load<Texture2D>("Sprites/robotRight");
        }

        public void SetPlayerPos(Vector2 startingPos)
        {
            this.position = startingPos;
            isOnGround = true;
        }

        private void Movement(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            if (game.km.IsKeyHeld(Keys.A) || game.km.IsKeyHeld(Keys.Left))
            {
                velocity.X = -speed - 1f;
            }

            if (game.km.IsKeyHeld(Keys.D) || game.km.IsKeyHeld(Keys.Right))
            {
                velocity.X = speed + 1f;
            }

            if (game.km.IsKeyHeld(Keys.Space) && timeOffGround < mSecsToMaxJump)
            {
                velocity.Y = speed;
                isOnGround = false;
            }

            if (isOnGround)
                timeOffGround = 0;
            else
                timeOffGround += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (!isOnGround && timeOffGround > 1300)
            {
                velocity.Y = -speed;
                timeOffGround -= gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            futurePos = position + velocity;
        }

        public Vector2 ConvertToDrawPos(Vector2 pos)
        {
            return new Vector2(game.GraphicsDevice.Viewport.Width / 2 + pos.X, game.GraphicsDevice.Viewport.Height - pos.Y);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(robotTexture, ConvertToDrawPos(position), Color.White);
        }

        public override void Update(GameTime gameTime, List<Sprite> textures)
        {
            Movement(gameTime);

            if (CanMove(textures)) position = futurePos;
            
        }

        public bool CanMove(List<Sprite> textures)
        {
            if (textures == null) return true;
            foreach(Sprite s in textures)
            {
                if(s.IsColliding(futurePos)) return false;
            }
            return true;
        }

        //public bool IsGrounded()
        //{

        //}
    }
}
