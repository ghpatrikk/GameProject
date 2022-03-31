using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace MiniEngine
{
    public class SpriteRenderer : Component
    {
        public Texture2D Image;
        public Color ImageColor = Color.White;
        public int ImageWidth = 10;
        public int ImageHeight = 10;
        public int PositionX;
        public int PositionY;
        public float PositionZ = 0f;
        public float Scale = 1f;
        private Transform transform;
        private void Start()
        {
            this.transform = base.GameObject.GetComponent<Transform>();
            if (this.transform == null)
            {
                throw new Exception("GameObject needs a Transform attached");
            }
            EventManager.OnRender += new EventManager.RenderEvent(this.OnRender);
        }
        public void SetImage(Texture2D image)
        {
            this.Image = image;
            this.ImageWidth = image.Width;
            this.ImageHeight = image.Height;
        }
        public void SetImage(Texture2D image, int width, int height)
        {
            this.Image = image;
            this.ImageWidth = width;
            this.ImageHeight = height;
        }
        private void OnRender(SpriteBatch spriteBatch)
        {
            if (this.Image != null)
            {
                spriteBatch.Draw(this.Image, new Rectangle((int)this.transform.Position.X + PositionX, (int)this.transform.Position.Y + PositionY, this.ImageWidth, this.ImageHeight), null, this.ImageColor, 0f, Vector2.Zero, SpriteEffects.None, this.PositionZ);
                //spriteBatch.Draw(this.Image, new Vector2((int)transform.Position.X, (int)transform.Position.Y), null, this.ImageColor, 0f, Vector2.Zero, this.Scale, SpriteEffects.None, this.PositionZ);
            }
        }
        public override void Destroy()
        {
            EventManager.OnRender -= new EventManager.RenderEvent(this.OnRender);
            base.Destroy();
        }
    }
}