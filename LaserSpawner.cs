using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniEngine;

namespace JumpBlackAndRunWhite
{
    class LaserSpawner : GameObject
    {

        private Transform transform;

        private Random random = new Random();
        private float laserTimer;

        public LaserSpawner()
        {
            transform = AddComponent<Transform>();

            EventManager.OnUpdate += OnUpdate;
        }

        private void OnUpdate(GameTime gameTime)
        {
            if (GameManager.GameStarted)
            {
                bool laserTimerElapsed = LaserCheckTimerElapsed(gameTime);
                if (laserTimerElapsed)
                {
                    SpawnLaser();
                }
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

        private void SpawnLaser()
        {
            Laser laser = new Laser();
            laser.GetComponent<Transform>().Position = new Vector2(GameManager.CurrentGraphicsDevice.Viewport.Width, 0);
            laser.GetComponent<SpriteRenderer>().SetImage(ChooseColor(laser), laser.GetComponent<SpriteRenderer>().ImageWidth, laser.GetComponent<SpriteRenderer>().ImageHeight);

        }


        private Texture2D ChooseColor(Laser laser)
        {
            int r = random.Next(0, 2);
            switch (r)
            {
                case 0:
                    laser.Tag = Tags.Blue;
                    return GameManager.CurrentContent.Load<Texture2D>("BlueLaser");
                case 1:
                    laser.Tag = Tags.Red;
                    return GameManager.CurrentContent.Load<Texture2D>("RedLaser");
                default:
                    return GameManager.CurrentContent.Load<Texture2D>("None");
            }
        }

        private bool LaserCheckTimerElapsed(GameTime gameTime)
        {
            laserTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (laserTimer > GameManager.LaserSpawnerDelay)
            {
                laserTimer = 0;
                return true;
            }
            return false;
        }
    }
}
