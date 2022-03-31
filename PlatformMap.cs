using System;
using Microsoft.Xna.Framework;
using MiniEngine;

namespace JumpBlackAndRunWhite
{
    class PlatformMap : GameObject
    {

        private Transform transform;

        private Random random = new Random();

        public PlatformMap()
        {
            transform = AddComponent<Transform>();
            transform.Position = new Vector2(0, 0);

            EventManager.OnUpdate += OnUpdate;
            loadMap();
        }

        private void OnUpdate(GameTime gameTime)
        {
        }

        public override void Destroy()
        {
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }

        private void loadMap()
        {
            Spawn(new Vector2(0, 500));
            Spawn(new Vector2(300, 450));
            Spawn(new Vector2(100, 350));
            Spawn(new Vector2(200, 200));
            Spawn(new Vector2(500, 100));
            Spawn(new Vector2(200, 0));
            Spawn(new Vector2(0, -100));
            Spawn(new Vector2(150, -200));
            Spawn(new Vector2(500, -300));
            Spawn(new Vector2(100, -400));
            Spawn(new Vector2(400, -550));
            Spawn(new Vector2(0, -700));
            Spawn(new Vector2(500, -850));
            Spawn(new Vector2(100, -950));
            Spawn(new Vector2(300, -1100));
            Spawn(new Vector2(0, -1200));

        }

        private void Spawn(Vector2 platformPosition)
        {
            Color platformColor = ChooseColor();
            Platform platform = new Platform();
            platform.GetComponent<Transform>().Position = platformPosition;
            platform.GetComponent<Renderer>().ImageColor = platformColor;
        }


        private Color ChooseColor()
        {
            int r = random.Next(0, 2);
            switch(r)
            {
                case 0:
                    return Color.Black;
                case 1:
                    return Color.White;
                default:
                    return Color.Pink;
            }
        }
    }
}
