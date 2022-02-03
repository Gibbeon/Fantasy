using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Screens.Transitions;
using System.Linq;
using System;

namespace Fantasy.Game.Screens
{  
    public class TitleScreen : GameScreen
    {
        private GuiSystem _guiSystem;
        private KeyboardListener  _keyboardListener; 
        public TitleScreen(Microsoft.Xna.Framework.Game game) : base (game)
        {
            
        }

        public override void Initialize() 
        {
            base.Initialize();
            LoadContent();            
        }

        public override void LoadContent()
        {                
            var guiRenderer = new GuiSpriteBatchRenderer(GraphicsDevice, () => Matrix.Identity);
            var viewportAdapter = new MonoGame.Extended.ViewportAdapters.DefaultViewportAdapter(GraphicsDevice);            
            var font = Content.Load<MonoGame.Extended.BitmapFonts.BitmapFont>("fonts/sm_arial");
            MonoGame.Extended.BitmapFonts.BitmapFont.UseKernings = false;
            
            Skin.CreateDefault(font);

            _guiSystem = new GuiSystem(viewportAdapter, guiRenderer) {
                ActiveScreen = new MonoGame.Extended.Gui.Screen
                {
                    Content = new MonoGame.Extended.Gui.Controls.Label("New Game")
                    {
                        VerticalAlignment = VerticalAlignment.Centre,
                        HorizontalAlignment = HorizontalAlignment.Centre
                    }
                }
            };

            _keyboardListener = new KeyboardListener( new KeyboardListenerSettings()
            {
                RepeatPress = false         
            });

            _keyboardListener.KeyTyped += (sender, args) => {
                switch(args.Key)
                {
                    case Microsoft.Xna.Framework.Input.Keys.Enter:
                        NewGame();
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.Escape:
                        Environment.Exit(0);
                        break;
                }
            };
        }

        protected void NewGame()
        {
            Game.Components.OfType<ScreenManager>().First().LoadScreen(
                new LocalAreaScreen(Game),
                new FadeTransition(GraphicsDevice, Color.Black)
            );
        }
        
        public override void Update(GameTime gameTime)
        {
            _keyboardListener.Update(gameTime);
            _guiSystem.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _guiSystem.Draw(gameTime);            
        }
    }
}
