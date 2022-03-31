using System;
using MiniEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniEngine
{
    public class Animator : Component
    {

        private Transform transform;

        private Texture2D image;
        private int currentFrame;
        private int maxFrames;
        private Rectangle sourceRect = new Rectangle();
        private float timer;

        public int FrameDelay { get; set; }

        void Start()
        {
            this.transform = this.GameObject.GetComponent<Transform>();
            EventManager.OnUpdate += OnUpdate;
            EventManager.OnRender += OnRender;
        }

        public void Animate(Texture2D image, int spriteWidth, int spriteHeight, int maxFrames)
        {
            this.image = image;
            this.sourceRect.Width = spriteWidth;
            this.sourceRect.Height = spriteHeight;
            this.maxFrames = maxFrames;
        }

        void OnUpdate(GameTime gameTime)
        {
            CalculateFrame(gameTime);
            CalculateSource();
        }

        void OnRender(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.image, transform.Position, this.sourceRect, Color.White);
        }

        public override void Destroy()
        {
            EventManager.OnUpdate -= OnUpdate;
            EventManager.OnRender -= OnRender;
            base.Destroy();
        }

        private void CalculateFrame(GameTime gameTime)
        {
            this.timer += gameTime.ElapsedGameTime.Milliseconds;

            if (timer > this.FrameDelay)
            {
                this.timer = 0;

                if (this.currentFrame < this.maxFrames)
                {
                    this.currentFrame++;
                }
                else
                {
                    this.currentFrame = 0;
                }
            }
        }

        private void CalculateSource()
        {
            this.sourceRect.X = this.sourceRect.Width * this.currentFrame;
        }
    }
}
