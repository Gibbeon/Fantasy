using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using Fantasy.Screens;

namespace Fantasy
{  
    public class Main : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        public ScreenManager _screenManager;
        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            IsMouseVisible = true;
            Window.IsBorderless = true;            
        }

        protected override void LoadContent()
        {    
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

            _screenManager.LoadScreen(new WorldScreen(this));
        }

        protected override void Update(GameTime gameTime)
        {
            // process input
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            base.Draw(gameTime);
        }
    }
}
