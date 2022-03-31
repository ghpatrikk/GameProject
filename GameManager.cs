using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace JumpBlackAndRunWhite
{
    

    public static class GameManager
    {
        public static ContentManager CurrentContent;
        public static GraphicsDevice CurrentGraphicsDevice;
        public static GameState gameState = new GameState();
        public static bool GameStateChanged = false;
        public static Song menuSong;
        public static SoundEffect buttonSound;
        public static SoundEffect deathSound;
        public static SoundEffect jumpSound;

        public static bool GameStarted;
        public static bool StarterPlatformDestroyed;
        public static float SpaceSpeed = 1f;
        public static float WorldSpeed = 2.5f;
        public static int Points = 0;
        public static float LaserSpawnerDelay;
        public static float BlockSpawnDelay;
    }
}