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
    class ControlScreen : GameObject
    {
        private UILabel controls = new UILabel();
        private UILabel backButton = new UILabel();

        Texture2D background;

        public ControlScreen()
        {
            background = GameManager.CurrentContent.Load<Texture2D>("MenuBackground");

            InitControlScreen();
            InitBackButton();

            EventManager.OnUpdate += OnUpdate;
        }

        private void OnUpdate(GameTime gameTime)
        {
            controls.TextRenderer.Text = " W - Jump \n A - Left \n D - Right \n Space - Change Color";
            backButton.TextRenderer.Text = " M - Menu";
            CheckInput();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 1000, 500), Color.White);
        }

        private void InitControlScreen()
        {
            controls.Position = new Vector2(250, 150);
            controls.TextRenderer.FontColor = Color.Red;
            controls.TextRenderer.FontSize = 2;
        }

        private void InitBackButton()
        {
            backButton.Position = new Vector2(250, 400);
            backButton.TextRenderer.FontColor = Color.Red;
            backButton.TextRenderer.FontSize = 2;
        }

        private void CheckInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.M))
            {
                GameManager.GameStateChanged = false;
                GameManager.gameState = GameState.Menu;
                this.Destroy();
            }
        }

        public override void Destroy()
        {
            GameManager.buttonSound.Play();
            controls.TextRenderer.Text = "";
            backButton.TextRenderer.Text = "";
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }
    }
}
