using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpBlackAndRunWhite
{
    class TextRenderer : Component
    {
        public string Text = "";
        public Color FontColor = Color.Black;
        public int FontSize = 1;
        public int PositionZ = 0;
        public Vector2 Origin = Vector2.Zero;

        private Transform transform;

        private SpriteFont spriteFont;

        void Start()
        {
            transform = this.GameObject.GetComponent<Transform>();
            if (transform == null)
                throw new Exception("Gameobject needs Tranform component");

            SetFont("Arial");

            EventManager.OnRender += OnRender;
        }

        public override void Destroy()
        {
            EventManager.OnRender -= OnRender;
            base.Destroy();
        }

        private void OnRender(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, Text, transform.Position, FontColor, 0, Origin, FontSize, SpriteEffects.None, PositionZ);
        }

        public void SetFont(string path)
        {
            spriteFont = GameManager.CurrentContent.Load<SpriteFont>(path);
        }
    }
}
