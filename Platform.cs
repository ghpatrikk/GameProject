using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniEngine;

namespace JumpBlackAndRunWhite
{
    class Platform : GameObject
    {
        Transform transform;
        Renderer renderer;
        BoxCollider boxCollider;
        SpriteRenderer spriteRenderer;
        public Platform()
        {
            this.Tag = Tags.Blue;

            transform = AddComponent<Transform>();

            renderer = AddComponent<Renderer>();
            renderer.Image = new Texture2D(GameManager.CurrentGraphicsDevice, 1, 1);
            renderer.Image.SetData(new[] { Color.White });
            renderer.ImageWidth = 100;
            renderer.ImageHeight = 10;
            renderer.ImageColor = Color.Transparent;
            renderer.PositionZ = -0.3f;

            boxCollider = AddComponent<BoxCollider>();

            spriteRenderer = AddComponent<SpriteRenderer>();
            spriteRenderer.ImageWidth = 250;
            spriteRenderer.ImageHeight = 80;
            spriteRenderer.PositionX = -40;
            spriteRenderer.PositionY = -35;
            spriteRenderer.SetImage(GameManager.CurrentContent.Load<Texture2D>("BluePlatform"), spriteRenderer.ImageWidth, spriteRenderer.ImageHeight);
            spriteRenderer.ImageColor = Color.White;
            spriteRenderer.PositionZ = -0.3f;

            EventManager.OnUpdate += OnUpdate;
        }

        private void OnUpdate(GameTime gameTime)
        {
            transform.Translate(-GameManager.WorldSpeed, 0);

            if (GameManager.gameState == GameState.DeathScreen)
            {
                Destroy();
            }
        }

        public override void Destroy()
        {
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }
    }
}
