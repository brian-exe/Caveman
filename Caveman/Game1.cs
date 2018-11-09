using Caveman.Models.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Caveman
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Rectangle Bounds { get; private set; }

        GameManager manager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Content.RootDirectory = "Content";
            this.manager = GameManager.Instance;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Bounds = GraphicsDevice.Viewport.Bounds;
            manager.LoadContent(Content);
            // TODO: use this.Content to load your game content here

        }


        protected override void UnloadContent()
        {
            manager.UnloadContent();
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            manager.Update(ref gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(254,195,41));

            // TODO: Add your drawing code here
            spriteBatch.Begin();
                manager.Draw(ref spriteBatch, ref gameTime);
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
