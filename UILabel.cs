using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpBlackAndRunWhite
{
    class UILabel : GameObject
    {
        private Transform transform;
        private TextRenderer textRenderer;
        private Renderer renderer;

        public Vector2 Position
        {
            get { return transform.Position; }
            set { transform.Position = value; }
        }

        public TextRenderer TextRenderer
        {
            get { return textRenderer; }
            set { textRenderer = value; }
        }

        public UILabel()
        {
            transform = AddComponent<Transform>();

            textRenderer = AddComponent<TextRenderer>();
            textRenderer.PositionZ = 1;

            renderer = AddComponent<Renderer>();
        }

        public void SetBackgroundImage(Texture2D image, int width, int height)
        {
            renderer.Image = image;
            renderer.ImageWidth = width;
            renderer.ImageHeight = height;
        }
    }
}
