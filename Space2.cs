using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniEngine;

namespace JumpBlackAndRunWhite
{
    class Space2 : GameObject
    {
        Transform transform;
        Renderer renderer;

        public Space2()
        {
            transform = AddComponent<Transform>();

            renderer = AddComponent<Renderer>();
            renderer.ImageWidth = 2000;
            renderer.ImageHeight = 500;
            renderer.SetImage(GameManager.CurrentContent.Load<Texture2D>("Space"));

            renderer.PositionZ = -1f;
            transform.Position = SpawnPosition();

            EventManager.OnUpdate += OnUpdate;
        }

        private void OnUpdate(GameTime gameTime)
        {
            transform.Translate(-GameManager.SpaceSpeed, 0);

            if (transform.Position.X + renderer.ImageWidth <= GameManager.CurrentGraphicsDevice.Viewport.X)
            {
                int x = GameManager.CurrentGraphicsDevice.Viewport.X + renderer.ImageWidth;
                int y = GameManager.CurrentGraphicsDevice.Viewport.Y;
                transform.Position = new Vector2(x, y);
            }


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

        private Vector2 SpawnPosition()
        {
            
            int x = GameManager.CurrentGraphicsDevice.Viewport.X + renderer.ImageWidth;
            int y = GameManager.CurrentGraphicsDevice.Viewport.Y;

            return new Vector2(x,y);
        }
    }
}
