using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniEngine;

namespace JumpBlackAndRunWhite
{
    class PlatformSpawner : GameObject
    {
        public float PlatformSpawnDelay = 1800;

        private Transform transform;

        private Random random = new Random();
        private float platformTimer;

        public PlatformSpawner()
        {
            transform = AddComponent<Transform>();

            EventManager.OnUpdate += OnUpdate;
        }

        private void OnUpdate(GameTime gameTime)
        {
            bool platformTimerElapsed = PlatformCheckTimerElapsed(gameTime);
            if (platformTimerElapsed)
            {
                SpawnPlatform();
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

        private void SpawnPlatform()
        {
            Platform platform = new Platform();
            platform.GetComponent<Transform>().Position = PlatformSpawnPosition();
            platform.GetComponent<Renderer>().ImageWidth = PlatformWidth();
            platform.GetComponent<BoxCollider>().Width = platform.GetComponent<Renderer>().ImageWidth;
            platform.GetComponent<SpriteRenderer>().SetImage(ChooseColor(platform), platform.GetComponent<SpriteRenderer>().ImageWidth, platform.GetComponent<SpriteRenderer>().ImageHeight);

        }


        private Vector2 PlatformSpawnPosition()
        {
            //int randomX = random.Next(0, 1000);
            int randomY = random.Next(375, 450);

            return new Vector2(1100, randomY);
        }

        private int PlatformWidth()
        {
            int randomWidth = 170;//random.Next(100, 200);

            return randomWidth;
        }


        private Texture2D ChooseColor(Platform platform)
        {
            int r = random.Next(0, 2);
            switch(r)
            {
                case 0:
                    platform.Tag = Tags.Blue;
                    return GameManager.CurrentContent.Load<Texture2D>("BluePlatform");
                case 1:
                    platform.Tag = Tags.Red;
                    return GameManager.CurrentContent.Load<Texture2D>("RedPlatform");
                default:
                    return GameManager.CurrentContent.Load<Texture2D>("None");
            }
        }

        private bool PlatformCheckTimerElapsed(GameTime gameTime)
        {
            platformTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (platformTimer > PlatformSpawnDelay)
            {
                platformTimer = 0;
                return true;
            }
            return false;
        }
    }
}
