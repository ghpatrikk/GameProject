using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniEngine;

namespace JumpBlackAndRunWhite
{
    class Laser : GameObject
    {
        Transform transform;
        Renderer renderer;
        BoxCollider boxCollider;
        SpriteRenderer spriteRenderer;

        private float speed = 5f;

        public Laser()
        {
            this.Tag = Tags.Blue;

            transform = AddComponent<Transform>();

            renderer = AddComponent<Renderer>();
            renderer.Image = new Texture2D(GameManager.CurrentGraphicsDevice, 1, 1);
            renderer.Image.SetData(new[] { Color.White });
            renderer.ImageWidth = 10;
            renderer.ImageHeight = GameManager.CurrentGraphicsDevice.Viewport.Width;
            renderer.ImageColor = Color.Transparent;
            renderer.PositionZ = 1f;

            boxCollider = AddComponent<BoxCollider>();

            spriteRenderer = AddComponent<SpriteRenderer>();
            spriteRenderer.ImageWidth = 200;
            spriteRenderer.ImageHeight = 600;
            spriteRenderer.PositionX = -90;
            spriteRenderer.PositionY = 0;
            spriteRenderer.SetImage(GameManager.CurrentContent.Load<Texture2D>("BlueLaser"), spriteRenderer.ImageWidth, spriteRenderer.ImageHeight);
            spriteRenderer.ImageColor = Color.White;
            spriteRenderer.PositionZ = 1f;

            EventManager.OnUpdate += OnUpdate;
        }

        public override void Destroy()
        {
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }

        private void OnUpdate(GameTime gameTime)
        {
            transform.Translate(-speed, 0);

            if (GameManager.gameState == GameState.DeathScreen)
            {
                Destroy();
            }
        }
    }
}
