#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
using MiniEngine;
#endregion

namespace JumpBlackAndRunWhite
{
    public enum GameState
    {
        Menu,
        Controls,
        Game,
        DeathScreen,
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        UIController uiController;
        Menu menu;
        ControlScreen controlScreen;
        DeathScreen deathScreen;

        Raumschiff raumschiff;
        Raumschiff2 raumschiff2;
        Space1 space1;
        Space2 space2;

        StartCountdown startCountdown;
        DifficultyManager difficultyManager;
        Player player;
        BlockCannon blockCannon;
        //PlatformMap platformMap;
        StarterPlatform starterPlatform;
        PlatformSpawner platformSpawner;
        LaserSpawner laserSpawner;

        public Game1() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 1000;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameManager.CurrentGraphicsDevice = this.GraphicsDevice;
            GameManager.CurrentContent = this.Content;

           GameManager.menuSong = Content.Load<Song>("ScreenerSong.wav");
            GameManager.buttonSound = Content.Load<SoundEffect>("BTNSimple.wav");
            GameManager.deathSound = Content.Load<SoundEffect>("Todessound.wav");
            GameManager.jumpSound = Content.Load<SoundEffect>("Jetpack.wav");
            MediaPlayer.Volume = 1f;

            InitMenu();
        }

        private void InitMenu()
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(GameManager.menuSong);
            }

            GameManager.gameState = GameState.Menu;
            GameManager.GameStateChanged = true;

            menu = new Menu();
            uiController = new UIController();

            MediaPlayer.Play(GameManager.menuSong);
        }

        private void InitControls()
        {
            GameManager.gameState = GameState.Controls;
            GameManager.GameStateChanged = true;

            controlScreen = new ControlScreen();
            uiController = new UIController();
        }

        private void InitGame()
        {
            //MediaPlayer.Stop();
            GameManager.gameState = GameState.Game;
            GameManager.GameStateChanged = true;

            uiController = new UIController();
            startCountdown = new StartCountdown();
            difficultyManager = new DifficultyManager();

            space1 = new Space1();
            space2 = new Space2();
            raumschiff = new Raumschiff();
            raumschiff2 = new Raumschiff2();
            player = new Player();
            //platformMap = new PlatformMap();
            blockCannon = new BlockCannon();
            laserSpawner = new LaserSpawner();
            starterPlatform = new StarterPlatform();
            platformSpawner = new PlatformSpawner();

        }

        private void InitDeathScreen()
        {
            MediaPlayer.Play(GameManager.menuSong);
            GameManager.gameState = GameState.DeathScreen;
            GameManager.GameStateChanged = true;

            deathScreen = new DeathScreen();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

                EventManager.Update(gameTime);
                CollisionManager.Update();

                if (GameManager.gameState == GameState.Menu && GameManager.GameStateChanged == false)
                {
                    InitMenu();
                    CheckMusic();
                }
                if (GameManager.gameState == GameState.Controls && GameManager.GameStateChanged == false)
                {
                    InitControls();
                    CheckMusic();
                }
                if (GameManager.gameState == GameState.Game && GameManager.GameStateChanged == false)
                {
                    InitGame();
                }
                if (GameManager.gameState == GameState.DeathScreen && GameManager.GameStateChanged == false)
                {
                    InitDeathScreen();
                    CheckMusic();
                }
                
                
            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

            if (GameManager.gameState == GameState.Menu)
            {
                GraphicsDevice.Clear(Color.White);
                menu.Draw(spriteBatch);
                EventManager.Render(spriteBatch);
            }
            if (GameManager.gameState == GameState.Controls)
            {
                GraphicsDevice.Clear(Color.White);
                controlScreen.Draw(spriteBatch);
                EventManager.Render(spriteBatch);
            }
            if (GameManager.gameState == GameState.Game)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                EventManager.Render(spriteBatch);
            }
            if (GameManager.gameState == GameState.DeathScreen)
            {
                GraphicsDevice.Clear(Color.Black);
                deathScreen.Draw(spriteBatch);
                EventManager.Render(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
        private void CheckMusic()
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(GameManager.menuSong);
            }
        }
    }
}
