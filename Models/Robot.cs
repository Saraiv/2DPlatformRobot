using _2DPlatformerRobot.Manager;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using _2DPlatformerRobot.Collider;

namespace _2DPlatformerRobot.Models
{
    class Robot : ICollider
    {
        private Texture2D robotTexture;
        private KeyboardManager km;
        private SpriteBatch spriteBatch;
        private ContentManager content;
        private GraphicsDevice graphicsDevice;
        private int screenHeight;
        private bool isOnGround = true;
        private bool singleJump = true;
        private const int playerVelocity = 100;

        private const int SecsToMaxJump = 1;
        private float timeOffGround = 0;
        private bool maxJumped = false;

        private Vector2 position;

        public Robot(KeyboardManager km, SpriteBatch spriteBatch, ContentManager content, int screenHeight, GraphicsDevice graphicsDevice)
        {
            this.km = km;
            this.spriteBatch = spriteBatch;
            this.content = content;
            robotTexture = content.Load<Texture2D>("Sprites/robotRight");
            this.screenHeight = screenHeight;
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

            if (km.IsKeyHeld(Keys.Space)) //!maxJumped !singleJump
            {
                position = position + new Vector2(0, 2) * (float)gameTime.ElapsedGameTime.TotalSeconds * playerVelocity;
                isOnGround = false;
            }

            if(!isOnGround)
                position = position + new Vector2(0, -1) * (float)gameTime.ElapsedGameTime.TotalSeconds * playerVelocity;
        }

        Vector2 ConvertToDrawPos(Vector2 pos)
        {
            return new Vector2(graphicsDevice.Viewport.Width / 2 + pos.X, graphicsDevice.Viewport.Height - pos.Y);
        }

        public string Name() => "Player";

        public void CollisionWith(ICollider other)
        {
            if(other.Name() == "Wall")
            {
                
            }
        }

        public bool CollidesWith(ICollider other)
        {
            return other.CollidesWith(this);
        }

        public ICollider GetCollider()
        {
            return this;
        }

        public void Draw()
        {
            spriteBatch.Draw(robotTexture, ConvertToDrawPos(position), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            Movement(gameTime);
        }
    }
}
