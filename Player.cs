using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniEngine;

namespace JumpBlackAndRunWhite
{
    class Player : GameObject
    {

        private Transform transform;
        private Renderer renderer;
        private BoxCollider boxCollider;
        private SpriteAnimation spriteAnimation;
        private float playerSpeed = 3.5f;
        private float playerScale = 0.58f;
        private Vector2 velocity;
        public static bool hasJumped;
        private bool feetOnGround;
        private bool PlayerScrolling;
        private bool colorSwapable = true;

        // Directions
        private const string Left = "left";
        private const string Right = "right";

        // States
        private const string Idle = "idle";
        private const string Walk = "walk";
        private const string Jump = "jump";

        private string state = Idle;
        private string direction = Right;


        public Player()
        {
            this.Tag = Tags.Blue;
            transform = AddComponent<Transform>();
            transform.Position = new Vector2(525, 200);

            spriteAnimation = AddComponent<SpriteAnimation>();
            spriteAnimation.PositionX = -10;
            spriteAnimation.PositionY = -10;
            spriteAnimation.SetAnimation("siggi", GameManager.CurrentContent.RootDirectory + "/Blue_Siggi.xml", GameManager.CurrentContent.Load<Texture2D>("Blue_Siggi"), this.playerScale, 1, 100);
            
            renderer = AddComponent<Renderer>();
            renderer.Image = new Texture2D(GameManager.CurrentGraphicsDevice, 1, 1);
            renderer.Image.SetData(new[] { Color.White });
            renderer.ImageWidth = 60;
            renderer.ImageHeight = 90;
            renderer.ImageColor = Color.Transparent;
            renderer.PositionZ = 0.8f;
            
            hasJumped = true;

            boxCollider = AddComponent<BoxCollider>();
            boxCollider.OnCollision += OnCollision;
            boxCollider.OnCollisionEnter += OnCollisionEnter;
            boxCollider.OnCollisionExit += OnCollisionExit;


            EventManager.OnUpdate += OnUpdate;
        }


        public override void Destroy()
        {
            GameManager.deathSound.Play();
            GameManager.gameState = GameState.DeathScreen;
            GameManager.GameStateChanged = false;
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }

        private void OnUpdate(GameTime gameTime)
        {
            Input();
            colorInput();
            transform.Position += velocity;

            spriteAnimation.PlayAnimation(string.Format("{0}_{1}", this.state, this.direction));

            if (GameManager.GameStarted && GameManager.StarterPlatformDestroyed)
            {
                GameManager.Points += gameTime.ElapsedGameTime.Milliseconds;
            }

            if (PlayerScrolling)
            {
                transform.Translate(-GameManager.WorldSpeed, 0);
            }

            CheckPlayerInScreen();
            //InitPlayer();
        }


        void OnCollision(BoxCollider other)
        {
            if (other.GameObject is Platform)
            {

                if (this.Tag != other.GameObject.Tag)
                {
                    Destroy();
                }
            }
            if (other.GameObject is StarterPlatform)
            {
                PlayerScrolling = false;
            }
            if (other.GameObject is Block)
            {
                if (this.Tag != other.GameObject.Tag)
                {
                    Destroy();
                }
            }
            if (other.GameObject is Laser)
            {
                if (this.Tag != other.GameObject.Tag)
                {
                    Destroy();
                }
            }
        }

        private void OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject is Platform)
            {
                if (this.Tag != other.GameObject.Tag)
                {
                    Destroy();
                }

                checkFeetOnGround(other);
                if (feetOnGround)
                {
                    hasJumped = false;
                }
                PlayerScrolling = true;
            }
            if (other.GameObject is StarterPlatform)
            {
                PlayerScrolling = false;
                hasJumped = false;
            }
        }

        void OnCollisionExit(BoxCollider other)
        {
            if (other.GameObject is Platform)
            {
                hasJumped = true;
                feetOnGround = false;
                PlayerScrolling = false;
            }

            if (other.GameObject is StarterPlatform)
            {
                hasJumped = true;
                feetOnGround = false;
                PlayerScrolling = false;
            }
        }

        private void Move(Vector2 direction)
        {
            transform.Translate(direction * playerSpeed);
        }

        private void Input()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.A) && GameManager.GameStarted)
            {
                this.direction = Left;
                if (!hasJumped)
                {
                    this.state = Walk;
                    
                }
                else
                {
                    this.state = Jump;
                }

                this.Move(new Vector2(-1, 0));
                velocity.X = -3f;
            }
            else if (keyboardState.IsKeyDown(Keys.D) && transform.Position.X - renderer.ImageWidth <= GameManager.CurrentGraphicsDevice.Viewport.Width && GameManager.GameStarted)
            {
                this.direction = Right;
                if (!hasJumped)
                {
                    this.state = Walk;

                }
                else
                {
                    this.state = Jump;
                }

                this.Move(new Vector2(1, 0));
                velocity.X = 3f;
            }
            if(keyboardState.IsKeyDown(Keys.W) && hasJumped == false && GameManager.GameStarted)
            {
                this.state = Jump;

                GameManager.jumpSound.Play();
                transform.Translate(0, -10);
                velocity.Y = -5f;
                hasJumped = true;

            }
            if (hasJumped == true)
            {
                float i = 1f;
                velocity.Y += 0.15f * i;
            }

            if (hasJumped == false)
            {
                this.state = Walk;
                velocity.Y = 0f;
                velocity.X = 0f;
            }
            if (!GameManager.GameStarted)
            {
                this.state = Idle;
            }
            if (keyboardState.IsKeyUp(Keys.A) && keyboardState.IsKeyUp(Keys.D) && keyboardState.IsKeyUp(Keys.W) && !hasJumped)
            {
                this.state = Idle;
            }
            else
            {
                velocity.X = 0f;
            }
        }

        private void colorInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (colorSwapable)
                {
                    if (this.Tag == Tags.Red)
                    {
                        this.Tag = Tags.Blue;
                        SwapColor("siggi", "/Blue_Siggi.xml", GameManager.CurrentContent.Load<Texture2D>("Blue_Siggi"));
                        colorSwapable = false;
                    }
                    else if (this.Tag == Tags.Blue)
                    {
                        this.Tag = Tags.Red;
                        SwapColor("siggi", "/Red_Siggi.xml", GameManager.CurrentContent.Load<Texture2D>("Red_Siggi"));
                        colorSwapable = false;
                    }
                }           
            }
            else if (keyboardState.IsKeyUp(Keys.Space))
            {
                colorSwapable = true;
            }
        }


        private void SwapColor(string name, string atlasPath, Texture2D atlas)
        {
            spriteAnimation.SetAnimation(name, GameManager.CurrentContent.RootDirectory + atlasPath, atlas, this.playerScale, 1, 100);
        }

        private void checkFeetOnGround(BoxCollider other)
        {
            float fallTroughFix = 10;
            if (transform.Position.Y + renderer.ImageHeight <= other.Bounds.Y + fallTroughFix)
            {
                feetOnGround = true;
            }
        }

        private void CheckPlayerInScreen()
        {
            if (transform.Position.Y >= GameManager.CurrentGraphicsDevice.Viewport.Height)
            {
                Destroy();
            }
            if (transform.Position.X <= GameManager.CurrentGraphicsDevice.Viewport.X)
            {
                Destroy();
            }
        }
    }
}
