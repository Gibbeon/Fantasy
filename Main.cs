
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Gui;
using Fantasy.Game.Screens;

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

            _screenManager.LoadScreen(new TitleScreen(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        protected override void Update(GameTime gameTime)
        {
            // process input
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}
