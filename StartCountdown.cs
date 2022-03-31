using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniEngine;

namespace JumpBlackAndRunWhite
{
    public class StartCountdown : GameObject
    {
        private float timer;
        private float startDelay = 5000;

        public StartCountdown()
        {
            GameManager.GameStarted = false;
            EventManager.OnUpdate += OnUpdate;
        }

        void OnUpdate(GameTime gameTime)
        {
            bool timerElapsed = CheckTimerElapsed(gameTime);
            if (timerElapsed)
            {
                GameManager.GameStarted = true;
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

        private bool CheckTimerElapsed(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer > startDelay)
            {
                timer = 0;
                return true;
            }
            return false;
        }
    }
}
