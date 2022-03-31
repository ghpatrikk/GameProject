using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniEngine;

namespace JumpBlackAndRunWhite
{
    class BlockCannon : GameObject
    {
        Transform transform;
        Renderer renderer;
        SpriteRenderer spriteRenderer;
        Random random = new Random();

        private float blockTimer;

        private bool reachedLeft = false;


    public BlockCannon()
	{
        transform = AddComponent<Transform>();
        transform.Position = new Vector2(900, 0);
        
        renderer = AddComponent<Renderer>();
        renderer.Image = new Texture2D(GameManager.CurrentGraphicsDevice, 1, 1);
        renderer.Image.SetData(new[] { Color.Green });
        renderer.ImageWidth = 100;
        renderer.ImageHeight = 50;
        renderer.ImageColor = Color.Transparent;
        renderer.PositionZ = 0.9f;

        spriteRenderer = AddComponent<SpriteRenderer>();
        spriteRenderer.ImageWidth = 200;
        spriteRenderer.ImageHeight = 50;
        spriteRenderer.PositionX = -50;
        spriteRenderer.PositionY = 0;
        spriteRenderer.SetImage(GameManager.CurrentContent.Load<Texture2D>("BlockCannon"), spriteRenderer.ImageWidth, spriteRenderer.ImageHeight);
        spriteRenderer.ImageColor = Color.White;
        spriteRenderer.PositionZ = 0.9f;

        EventManager.OnUpdate += OnUpdate;
	}

    public override void Destroy()
    {
        EventManager.OnUpdate -= OnUpdate;
        base.Destroy();
    }

    void OnUpdate(GameTime gameTime)
    {
        MoveCannon();

        if (GameManager.GameStarted)
        {
            bool blockTimerElapsed = BlockCheckTimerElapsed(gameTime);
            if (blockTimerElapsed)
            {
                shootBlock();
            }
        }

        if (GameManager.gameState == GameState.DeathScreen)
        {
            Destroy();
        }
    }

    private bool BlockCheckTimerElapsed(GameTime gameTime)
    {
        blockTimer += gameTime.ElapsedGameTime.Milliseconds;
        if (blockTimer > GameManager.BlockSpawnDelay)
        {
            blockTimer = 0;
            return true;
        }
        return false;
    }

    private void shootBlock()
    {
        float X = transform.Position.X + 25;
        Vector2 shootBlockPosition = new Vector2(X, renderer.ImageHeight);
        Block block = new Block();
        Texture2D blockImage = ChooseColor(block);
        block.GetComponent<Transform>().Position = shootBlockPosition;
        block.GetComponent<Renderer>().SetImage(blockImage, block.GetComponent<Renderer>().ImageWidth, block.GetComponent<Renderer>().ImageHeight);
    }

    private void MoveCannon()
    {

        if (transform.Position.X <= GameManager.CurrentGraphicsDevice.Viewport.Width - 100 && transform.Position.X >= 0 && !reachedLeft)
        {
            transform.Translate(-2, 0);
        }
        if (transform.Position.X <= GameManager.CurrentGraphicsDevice.Viewport.X + 5)
        {
            reachedLeft = true;
        }
        if (transform.Position.X <= GameManager.CurrentGraphicsDevice.Viewport.Width - 100 && transform.Position.X >= 0 && reachedLeft)
        {
            transform.Translate(2, 0);
        }
        if (transform.Position.X >= GameManager.CurrentGraphicsDevice.Viewport.Width - 105)
        {
            reachedLeft = false;
        }
    }

    private Texture2D ChooseColor(Block block)
    {
        int r = random.Next(0, 2);
        switch (r)
        {
            case 0:
                block.Tag = Tags.Blue;
                return GameManager.CurrentContent.Load<Texture2D>("BlueBlock");
            case 1:
                block.Tag = Tags.Red;
                return GameManager.CurrentContent.Load<Texture2D>("RedBlock");
            default:
                return null;
        }
    }
  }
}
