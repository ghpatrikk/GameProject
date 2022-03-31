using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace JumpBlackAndRunWhite
{
    class DeathScreen : GameObject
    {
        private UILabel deathLabel = new UILabel();
        private UILabel restartButton = new UILabel();
        private UILabel menuButton = new UILabel();
        private UILabel exitButton = new UILabel();

        Texture2D background;

        public DeathScreen()
        {
            background = GameManager.CurrentContent.Load<Texture2D>("MenuBackground");

            InitDeathLabel();
            InitRestartButton();
            InitMenuButton();
            InitExitButton();

            EventManager.OnUpdate += OnUpdate;
        }

        private void OnUpdate(GameTime gameTime)
        {
            deathLabel.TextRenderer.Text = string.Format(" You are Dead! \n Points: {0}", GameManager.Points);
            restartButton.TextRenderer.Text = " R - Restart";
            menuButton.TextRenderer.Text = " M - Menu";
            exitButton.TextRenderer.Text = " ESC - Exit";
            CheckInput();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 1000, 500), Color.White);
        }

        private void InitDeathLabel()
        {
            deathLabel.Position = new Vector2(250, 150);
            deathLabel.TextRenderer.FontSize = 2;
            deathLabel.TextRenderer.FontColor = Color.Red;
        }

        private void InitRestartButton()
        {
            restartButton.Position = new Vector2(250, 300);
            restartButton.TextRenderer.FontColor = Color.Red;
            restartButton.TextRenderer.FontSize = 2;
        }

        private void InitMenuButton()
        {
            menuButton.Position = new Vector2(250, 340);
            menuButton.TextRenderer.FontColor = Color.Red;
            menuButton.TextRenderer.FontSize = 2;
        }

        private void InitExitButton()
        {
            exitButton.Position = new Vector2(250, 380); 
            exitButton.TextRenderer.FontColor = Color.Red;
            exitButton.TextRenderer.FontSize = 2;
        }

        private void CheckInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.R))
            {
                GameManager.GameStateChanged = false;
                GameManager.gameState = GameState.Game;
                this.Destroy();
                
            }
            else if (keyboardState.IsKeyDown(Keys.M))
            {
                GameManager.GameStateChanged = false;
                GameManager.gameState = GameState.Menu;
                this.Destroy();
            }
        }

        public override void Destroy()
        {
            GameManager.buttonSound.Play();
            deathLabel.TextRenderer.Text = "";
            restartButton.TextRenderer.Text = "";
            menuButton.TextRenderer.Text = "";
            exitButton.TextRenderer.Text = "";
            GameManager.Points = 0;
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }
    }
}
