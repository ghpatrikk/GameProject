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
    class Menu : GameObject
    {
        Texture2D background;
        

        private UILabel startButton = new UILabel();
        private UILabel controlButton = new UILabel();
        private UILabel exitButton = new UILabel();

        public Menu()
        {
            background = GameManager.CurrentContent.Load<Texture2D>("MenuBackground");

            InitStartButton();
            InitControlButton();
            InitExitButton();

            EventManager.OnUpdate += OnUpdate;
        }

        private void OnUpdate(GameTime gameTime)
        {
            startButton.TextRenderer.Text = " S - Start";
            controlButton.TextRenderer.Text = " C - Controls";
            exitButton.TextRenderer.Text = " ESC - Exit";
            CheckInput();
        }

        private void InitStartButton()
        {
            startButton.Position = new Vector2(250, 170);
            startButton.TextRenderer.FontColor = Color.Red;
            startButton.TextRenderer.FontSize = 2;
        }

        private void InitControlButton()
        {
            controlButton.Position = new Vector2(250, 250);
            controlButton.TextRenderer.FontColor = Color.Red;
            controlButton.TextRenderer.FontSize = 2;
        }

        private void InitExitButton()
        {
            exitButton.Position = new Vector2(250, 330);
            exitButton.TextRenderer.FontColor = Color.Red;
            exitButton.TextRenderer.FontSize = 2;
        }

        private void CheckInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.S))
            {
                GameManager.GameStateChanged = false;
                GameManager.gameState = GameState.Game;
                this.Destroy();
            }
            if (keyboardState.IsKeyDown(Keys.C))
            {
                GameManager.GameStateChanged = false;
                GameManager.gameState = GameState.Controls;
                this.Destroy();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 1000, 500), Color.White);
        }

        public override void Destroy()
        {
            GameManager.buttonSound.Play();
            exitButton.TextRenderer.Text = "";
            controlButton.TextRenderer.Text = "";
            startButton.TextRenderer.Text = "";
            GameManager.Points = 0;
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        } 
    }
}
