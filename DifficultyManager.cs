using System;
using Microsoft.Xna.Framework;
using MiniEngine;

namespace JumpBlackAndRunWhite
{
    class DifficultyManager : GameObject
    {
        private bool difficulty_1 = false;
        private bool difficulty_2 = false;
        private bool difficulty_3 = false;
        private bool difficulty_endless = false;

        public DifficultyManager()
        {
            EventManager.OnUpdate += OnUpdate;
        }

        private void OnUpdate(GameTime gameTime)
        {
            CheckDifficulty();
            IncreasePlatformSpeed();
            IncreaseLaserSpawn();
            IncreaseBlockShotInterval();

            if (GameManager.gameState == GameState.DeathScreen)
            {
                Destroy();
            }
        }

        private void IncreasePlatformSpeed()
        {
            if (difficulty_1)
            {
                GameManager.WorldSpeed = 3f;
            }

            if (difficulty_2)
            {
                GameManager.WorldSpeed = 3.5f;
            }

            if (difficulty_3)
            {
                GameManager.WorldSpeed = 4.0f;
            }

            if (difficulty_endless)
            {
                GameManager.WorldSpeed += 0.0001f;
            }
        }

        private void IncreaseLaserSpawn()
        {
            GameManager.LaserSpawnerDelay = 8000;

            if (difficulty_1)
            {
                GameManager.LaserSpawnerDelay = 8000;
            }

            if (difficulty_2)
            {
                GameManager.LaserSpawnerDelay = 7000;
            }

            if (difficulty_3)
            {
                GameManager.LaserSpawnerDelay = 6000;
            }

            if (difficulty_endless)
            {
                GameManager.LaserSpawnerDelay -= 5;
            }
        }

        private void IncreaseBlockShotInterval()
        {
            GameManager.BlockSpawnDelay = 1700;

            if (difficulty_1)
            {
                GameManager.BlockSpawnDelay = 1600;
            }

            if (difficulty_2)
            {
                GameManager.BlockSpawnDelay = 1500;
            }

            if (difficulty_3)
            {
                GameManager.BlockSpawnDelay = 1400;
            }

            if (difficulty_endless)
            {
                GameManager.BlockSpawnDelay -= 5;
            }
        }

        private void CheckDifficulty()
        {
            bool higherThenDiff1 = false;
            bool higherThenDiff2 = false;
            bool higherThenDiff3 = false;
            if (GameManager.Points >= 20000 && higherThenDiff1 == false)
            {
                difficulty_1 = true;
            }

            if (GameManager.Points >= 60000 && higherThenDiff2 == false)
            {
                higherThenDiff1 = true;
                difficulty_1 = false;
                difficulty_2 = true;
            }

            if (GameManager.Points >= 100000 && higherThenDiff3 == false)
            {
                higherThenDiff2 = true;
                difficulty_2 = false;
                difficulty_3 = true;
            }

            if (GameManager.Points >= 180000)
            {
                higherThenDiff3 = true;
                difficulty_3 = false;
                difficulty_endless = true;
            }
        }
    }
}
