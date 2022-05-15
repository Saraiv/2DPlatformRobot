using _2DPlatformerRobot.Manager;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using _2DPlatformerRobot.Collider;
using System.Collections.Generic;

namespace _2DPlatformerRobot.Models
{
    //class Robot : ICollider
    class Robot : Sprite
    {
        private Texture2D robotTexture;
        private KeyboardManager km;
        private SpriteBatch spriteBatch;
        private ContentManager content;
        private GraphicsDevice graphicsDevice;
        private bool isOnGround = true;
        private const int playerVelocity = 100;


        //Jump
        private const int mSecsToMaxJump = 500;
        private double timeOffGround = 0;

        private Vector2 position;

        public Robot(Texture2D robotTexture, KeyboardManager km, SpriteBatch spriteBatch, ContentManager content, GraphicsDevice graphicsDevice) : base(robotTexture)
        {
            this.km = km;
            this.spriteBatch = spriteBatch;
            this.content = content;
            this.robotTexture = robotTexture;
            this.graphicsDevice = graphicsDevice;
        }

        public void SetPlayerPos(Vector2 startingPos)
        {
            this.position = startingPos;
            isOnGround = true;
        }

        private void Movement(GameTime gameTime)
        {
            if (km.IsKeyHeld(Keys.A) || km.IsKeyHeld(Keys.Left))
            {
                position = position + new Vector2(-2, 0) * (float)gameTime.ElapsedGameTime.TotalSeconds * playerVelocity;
                robotTexture = content.Load<Texture2D>("Sprites/robotLeft");
            }

            if (km.IsKeyHeld(Keys.D) || km.IsKeyHeld(Keys.Right))
            {
                position = position + new Vector2(2, 0) * (float)gameTime.ElapsedGameTime.TotalSeconds * playerVelocity;
                robotTexture = content.Load<Texture2D>("Sprites/robotRight");
            }

            if (km.IsKeyHeld(Keys.Space) && timeOffGround < mSecsToMaxJump)
            {
                position = position + new Vector2(0, 4) * (float)gameTime.ElapsedGameTime.TotalSeconds * playerVelocity;
                isOnGround = false;
            }

            if (isOnGround)
                timeOffGround = 0;
            else
                timeOffGround += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (!isOnGround)
                position += new Vector2(0, -1) * (float)gameTime.ElapsedGameTime.TotalSeconds * playerVelocity;
        }

        public Vector2 ConvertToDrawPos(Vector2 pos)
        {
            return new Vector2(graphicsDevice.Viewport.Width / 2 + pos.X, graphicsDevice.Viewport.Height - pos.Y);
        }


        public void Draw()
        {
            spriteBatch.Draw(robotTexture, ConvertToDrawPos(position), Color.White);
        }

        public void Update(GameTime gameTime, List<Sprite> textures)
        {
            Movement(gameTime);

            foreach(var texture in textures)
            {
                if(texture == this)
                {
                    continue;
                }

                if(this.velocity.X > 0 && this.IsTouchingLeft(texture) ||
                    this.velocity.X < 0 && this.IsTouchingRight(texture))
                {
                    this.velocity.X = 0;
                }

                if (this.velocity.Y > 0 && this.IsTouchingTop(texture) ||
                    this.velocity.Y < 0 && this.IsTouchingBottom(texture))
                {
                    this.velocity.Y = 0;
                }
            }

            position += velocity;
            velocity = Vector2.Zero;
        }
    }
}
