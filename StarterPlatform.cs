using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniEngine;

namespace JumpBlackAndRunWhite
{
    class StarterPlatform : GameObject
    {
        Transform transform;
        Renderer renderer;
        BoxCollider boxCollider;
        SpriteRenderer spriteRenderer;

        private int starterDelay = 10000;
        private float timer;

        public StarterPlatform()
        {
            GameManager.StarterPlatformDestroyed = false;

            transform = AddComponent<Transform>();
            transform.Position = new Vector2(500, 300);

            renderer = AddComponent<Renderer>();
            renderer.Image = new Texture2D(GameManager.CurrentGraphicsDevice, 1, 1);
            renderer.Image.SetData(new[] { Color.White });
            renderer.ImageWidth = 100;
            renderer.ImageHeight = 25;
            renderer.ImageColor = Color.Transparent;
            renderer.PositionZ = -0.5f;

            boxCollider = AddComponent<BoxCollider>();
            boxCollider.OnCollisionExit += OnCollisionExit;

            spriteRenderer = AddComponent<SpriteRenderer>();
            spriteRenderer.ImageWidth = 100;
            spriteRenderer.ImageHeight = 40;
            spriteRenderer.PositionX = 0;
            spriteRenderer.PositionY = 0;
            spriteRenderer.SetImage(GameManager.CurrentContent.Load<Texture2D>("StartPlatform"), spriteRenderer.ImageWidth, spriteRenderer.ImageHeight);
            spriteRenderer.ImageColor = Color.White;
            spriteRenderer.PositionZ = -0.3f;

            EventManager.OnUpdate += OnUpdate;
        }

        private void OnCollisionExit(BoxCollider other)
        {
            if (other.GameObject is Player)
            {
                Destroy();
            }
        }

        private void OnUpdate(GameTime gameTime)
        {
            //transform.Translate(-GameManager.WorldSpeed, 0);
            bool timerElapsed = CheckTimerElapsed(gameTime);
            if (timerElapsed)
            {
                Destroy();
            }
            if (GameManager.gameState != GameState.Game)
            {
                Destroy();
            }
        }

        public override void Destroy()
        {
            Player.hasJumped = true;
            GameManager.StarterPlatformDestroyed = true;
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }

        private bool CheckTimerElapsed(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer > starterDelay)
            {
                timer = 0;
                return true;
            }
            return false;
        }
    }
}
