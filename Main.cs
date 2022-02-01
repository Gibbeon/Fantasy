using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Gui.Controls;
using Fantasy.Game.Screens;
using SpriteFontPlus;

namespace Fantasy
{  
    public class Main : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        public ScreenManager _screenManager;
        private GuiSystem _guiSystem;
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
            
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += WindowOnClientSizeChanged;           
        }

        private void WindowOnClientSizeChanged(object sender, EventArgs eventArgs)
        {
            _guiSystem.ClientSizeChanged();
        }

        protected override void LoadContent()
        {    
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

            _screenManager.LoadScreen(new LocalAreaScreen(this));
            var guiRenderer = new GuiSpriteBatchRenderer(GraphicsDevice, () => Matrix.Identity);

            var viewportAdapter = new MonoGame.Extended.ViewportAdapters.DefaultViewportAdapter(GraphicsDevice);
            
            var font = Content.Load<MonoGame.Extended.BitmapFonts.BitmapFont>("sm_arial");
            MonoGame.Extended.BitmapFonts.BitmapFont.UseKernings = false;
            Skin.CreateDefault(font);

            var test = new MonoGame.Extended.Gui.Screen
            {

                Content = new MonoGame.Extended.Gui.Controls.StackPanel
                {
                    Height = 32,
                    Spacing = 3,
                    Orientation = Orientation.Horizontal,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    HorizontalAlignment = HorizontalAlignment.Centre,
                    Items =
                    {
                        new Button
                        {
                            Name = "Button1",       
                            BackgroundColor = new Color(30, 30, 30, 128),                     
                            Content = new Label("1") { VerticalTextAlignment = VerticalAlignment.Top },
                            Width = 32
                        },
                        new Button
                        {
                            Name = "Button2",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("2") { VerticalTextAlignment = VerticalAlignment.Top },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button3",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("3")  { VerticalTextAlignment = VerticalAlignment.Top },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button4",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("4") { VerticalTextAlignment = VerticalAlignment.Top },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button5",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("5") { VerticalTextAlignment = VerticalAlignment.Top },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button6",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("6")  { VerticalTextAlignment = VerticalAlignment.Top },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button7",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("7")  { VerticalTextAlignment = VerticalAlignment.Top },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button8",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("8")  { VerticalTextAlignment = VerticalAlignment.Top },
                            Width = 32
                            
                        }
                    }
                }
            };

            _guiSystem = new GuiSystem(viewportAdapter, guiRenderer) {
                 ActiveScreen = test 
            };
        }

        protected override void Update(GameTime gameTime)
        {
            // process input
            base.Update(gameTime);
            
            _guiSystem.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            base.Draw(gameTime);
            
            _guiSystem.Draw(gameTime);
        }
    }

    public class DemoViewModel
    {
        public DemoViewModel(string name, object content)
        {
            Name = name;
            Content = content;
        }

        public string Name { get; }
        public object Content { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
