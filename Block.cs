using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniEngine;

namespace JumpBlackAndRunWhite
{
    class Block : GameObject
    {
        Transform transform;
        Renderer renderer;
        BoxCollider boxCollider;

        private float speed = 2f;

        public Block()
        {
            Tag = Tags.Blue;
            transform = AddComponent<Transform>();

            renderer = AddComponent<Renderer>();
            //renderer.Image = new Texture2D(GameManager.CurrentGraphicsDevice, 1, 1);
            //renderer.Image.SetData(new[] { Color.Blue });
            renderer.ImageWidth = 70;
            renderer.ImageHeight = 70;
            renderer.SetImage(GameManager.CurrentContent.Load<Texture2D>("BlueBlock"), renderer.ImageWidth, renderer.ImageHeight);
            renderer.ImageColor = Color.White;
            renderer.PositionZ = 0;

            boxCollider = AddComponent<BoxCollider>();

            EventManager.OnUpdate += OnUpdate;
        }

        private void OnUpdate(GameTime gameTime)
        {
            transform.Translate(0, speed);

            CheckBlockInScreen();

            if (GameManager.gameState == GameState.DeathScreen)
            {
                Destroy();
            }
        }

        private void CheckBlockInScreen()
        {
            if (transform.Position.Y >= GameManager.CurrentGraphicsDevice.Viewport.Height)
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
