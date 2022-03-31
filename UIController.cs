using Microsoft.Xna.Framework;
using MiniEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JumpBlackAndRunWhite
{
    class UIController : GameObject
    {
        private UILabel pointLabel = new UILabel();
        private UILabel countdownLabel = new UILabel();

        private float timer;
        private int countdown = 5;
        


        public UIController()
        {
            InitPointLabel();
            InitCountdownLabel();

            EventManager.OnLateUpdate += OnLateUpdate;
        }

        private void OnLateUpdate(GameTime gameTime)
        {
            if (GameManager.gameState == GameState.Game)
            {
                pointLabel.TextRenderer.Text = string.Format("Points: {0}", GameManager.Points);
                countdownLabel.TextRenderer.Text = string.Format("{0}", countdown);
                Countdown(gameTime);
            }
            if (GameManager.gameState != GameState.Game)
            {
                Destroy();
            }
        }

        private void InitPointLabel()
        {
            pointLabel.Position = new Vector2(870, 480);
            pointLabel.TextRenderer.FontColor = Color.White;
        }

        private void InitCountdownLabel()
        {
            countdownLabel.Position = new Vector2(475, 75);
            countdownLabel.TextRenderer.FontColor = Color.White;
            countdownLabel.TextRenderer.FontSize = 15;
        }

        private void Countdown(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer >= 1000)
            {
                timer = 0;
                countdown--;
            }
            if (countdown <= 0)
            {
                countdownLabel.Destroy();
            }
        }

        public override void Destroy()
        {
            pointLabel.TextRenderer.Text = "";

            EventManager.OnLateUpdate -= OnLateUpdate;
            base.Destroy();
        }
    }
}
