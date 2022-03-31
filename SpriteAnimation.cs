using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniEngine;

namespace JumpBlackAndRunWhite
{
    public class SpriteAnimation : Component
    {
        private Transform transform;
        //public Vector2 Position { get; set; }
        public int FrameDelay { get; set; }
        public float Scale;
        public int PositionX;
        public int PositionY;
        public float PositionZ;

        private string name;
        private Texture2D atlas;
        private List<SpriteAnimationFrame> frames = new List<SpriteAnimationFrame>();

        private List<SpriteAnimationFrame> currentFrames = new List<SpriteAnimationFrame>();
        private string currentAnimationName;
        private int currentFrameCount;
        private float timer = 0;

        void Start()
        {
            this.transform = this.GameObject.GetComponent<Transform>();
            if (this.transform == null)
                throw new Exception("GameObject needs a Transform attached");

            EventManager.OnRender += OnRender;
            EventManager.OnUpdate += OnUpdate;
        }

        private void OnUpdate(GameTime gameTime)
        {
            this.CalculateFrame(gameTime);
        }

        private void OnRender(SpriteBatch spriteBatch)
        {
            if (this.currentFrames.Count != 0)
            {
                Rectangle source = this.CalculateSource();
                spriteBatch.Draw(this.atlas, new Vector2(transform.Position.X + this.PositionX, transform.Position.Y + this.PositionY), source, Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, PositionZ);
            }
        }

        public override void Destroy()
        {
            EventManager.OnRender -= OnRender;
            base.Destroy();
        }

        public void SetAnimation(string name, string atlasPath, Texture2D atlas, float scale, float positionZ, int frameDelay)
        {
            this.name = name;
            this.atlas = atlas;
            this.Scale = scale;
            this.PositionZ = positionZ;
            this.LoadFrames(atlasPath);
            this.FrameDelay = frameDelay;
        }


        public void PlayAnimation(string name)
        {
            if (name != this.currentAnimationName)
            {
                this.currentFrames = this.frames.FindAll((SpriteAnimationFrame animationFrame) => animationFrame.Name.Contains(name));
                this.currentFrameCount = 0;
                this.currentAnimationName = name;

                if (currentFrames.Count == 0)
                {
                    throw new Exception(string.Format("No AnimationFrame found for {0}", name));
                }
            }
        }

        private Rectangle CalculateSource()
        {
            SpriteAnimationFrame frame = this.currentFrames[currentFrameCount];
            return frame.Bounds;
        }

        private void CalculateFrame(GameTime gameTime)
        {
            this.timer += gameTime.ElapsedGameTime.Milliseconds;

            if (timer >= this.FrameDelay)
            {
                this.timer = 0;

                if (this.currentFrameCount < this.currentFrames.Count - 1)
                {
                    this.currentFrameCount++;
                }
                else
                {
                    this.currentFrameCount = 0;
                }
            }
        }

        private void LoadFrames(string atlasPath)
        {
            XmlReader xmlReader = XmlReader.Create(atlasPath);
            while (xmlReader.Read())
            {
                if (xmlReader.IsStartElement("sprite"))
                {
                    string name = xmlReader.GetAttribute("n");

                    if (name.Contains(this.name))
                    {
                        SpriteAnimationFrame animationFrame = new SpriteAnimationFrame();
                        animationFrame.Name = name;
                        animationFrame.Bounds.X = Convert.ToInt32(xmlReader.GetAttribute("x"));
                        animationFrame.Bounds.Y = Convert.ToInt32(xmlReader.GetAttribute("y"));
                        animationFrame.Bounds.Width = Convert.ToInt32(xmlReader.GetAttribute("w"));
                        animationFrame.Bounds.Height = Convert.ToInt32(xmlReader.GetAttribute("h"));

                        this.frames.Add(animationFrame);
                    }
                }
            }
        }
    }
}
